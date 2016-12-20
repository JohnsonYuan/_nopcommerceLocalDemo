using Nop.Core;
using Nop.WebMVC.Demo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Compilation;

[assembly: PreApplicationStartMethod(typeof(PluginManager), "Initialize")]
namespace Nop.WebMVC.Demo
{
    public class PluginManager
    {
        #region Const

        private const string InstalledPluginsFilePath = "~/App_Data/InstalledPlugins.txt";
        private const string PluginsPath = "~/Plugins";
        private const string ShallowCopyPath = "~/Plugins/bin";

        #endregion

        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        private static DirectoryInfo _shadowCopyFolder;

        /// <summary>
        /// Initialize
        /// </summary>
        public static void Initialize()
        {
            // prevent app from starting altogether
            var pluginFolder = new DirectoryInfo(CommonHelper.MapPath(PluginsPath));
            _shadowCopyFolder = new DirectoryInfo(CommonHelper.MapPath(ShallowCopyPath));

            Directory.CreateDirectory(pluginFolder.FullName);
            Directory.CreateDirectory(_shadowCopyFolder.FullName);

            //clear out plugins)
            foreach (var f in _shadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                f.Delete();
            }

            //shadow copy files
            foreach (var plug in pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                var di = Directory.CreateDirectory(Path.Combine(_shadowCopyFolder.FullName, plug.Directory.Name));
                // NOTE: You cannot rename the plugin DLL to a different name, it will fail because the assembly name is part if it's manifest
                // (a reference to how assemblies are loaded: http://msdn.microsoft.com/en-us/library/yx7xezcf )
                File.Copy(plug.FullName, Path.Combine(di.FullName, plug.Name), true);
            }

            foreach (var a in
                _shadowCopyFolder
                .GetFiles("*.dll", SearchOption.AllDirectories))
            {
                PerformFileDeploy(a);
                // BuildManager.AddReferencedAssembly(a);
            }

            foreach (var a in
                _shadowCopyFolder
                .GetFiles("*.dll", SearchOption.AllDirectories)
                .Select(x => AssemblyName.GetAssemblyName(x.FullName))
                .Select(x => Assembly.Load(x.FullName)))
            {
                BuildManager.AddReferencedAssembly(a);
            }
        }

        /// <summary>
        /// Perform file deploy
        /// 如果不是 Full Trust 复制该dll到~/Plugins/bin/
        /// 如果是 Full Trust 复制该dll到AppDomain.CurrentDomain.DynamicDirectory
        /// </summary>
        /// <param name="plug">Plugin file info</param>
        /// <returns>Assembly</returns>
        private static Assembly PerformFileDeploy(FileInfo plug)
        {
            if (plug.Directory.Parent == null)
                throw new InvalidOperationException("The plugin directory for the " + plug.Name +
                                                    " file exists in a folder outside of the allowed nopCommerce folder hierarchy");

            FileInfo shadowCopiedPlug;
            if (CommonHelper.GetTrustLevel() != AspNetHostingPermissionLevel.Unrestricted)
            {
                //all plugins will need to be copied to ~/Plugins/bin/
                //this is absolutely required because all of this relies on probingPaths being set statically in the web.config

                //were running in med trust, so copy to custom bin folder
                var shadowCopyPlugFolder = Directory.CreateDirectory(_shadowCopyFolder.FullName);
                shadowCopiedPlug = InitializeMediumTrust(plug, shadowCopyPlugFolder);


                //we can now register the plugin definition
                var shadowCopiedAssembly = Assembly.Load(AssemblyName.GetAssemblyName(shadowCopiedPlug.FullName));

                //add the reference to the build manager
                Debug.WriteLine("Adding to BuildManager: '{0}'", shadowCopiedAssembly.FullName);
                BuildManager.AddReferencedAssembly(shadowCopiedAssembly);
            }
            else
            {
                var directory = AppDomain.CurrentDomain.DynamicDirectory;
                Debug.WriteLine(plug.FullName + " to " + directory);
                shadowCopiedPlug = InitializeFullTrust(plug, new DirectoryInfo(directory));
            }

            return null;
            //return shadowCopiedAssembly;
        }

        /// <summary>
        /// Used to initialize plugins when running in Full Trust
        /// </summary>
        /// <param name="plug"></param>
        /// <param name="shadowCopyPlugFolder"></param>
        /// <returns></returns>
        private static FileInfo InitializeFullTrust(FileInfo plug, DirectoryInfo shadowCopyPlugFolder)
        {
            var shadowCopiedPlug = new FileInfo(Path.Combine(shadowCopyPlugFolder.FullName, plug.Name));
            try
            {
                File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
            }
            catch (IOException)
            {
                Debug.WriteLine(shadowCopiedPlug.FullName + " is locked, attempting to rename");
                //this occurs when the files are locked,
                //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
                //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
                try
                {
                    var oldFile = shadowCopiedPlug.FullName + Guid.NewGuid().ToString("N") + ".old";
                    File.Move(shadowCopiedPlug.FullName, oldFile);
                }
                catch (IOException exc)
                {
                    throw new IOException(shadowCopiedPlug.FullName + " rename failed, cannot initialize plugin", exc);
                }
                //ok, we've made it this far, now retry the shadow copy
                File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
            }
            return shadowCopiedPlug;
        }

        /// <summary>
        /// Used to initialize plugins when running in Medium Trust
        /// </summary>
        /// <param name="plug"></param>
        /// <param name="shadowCopyPlugFolder"></param>
        /// <returns></returns>
        private static FileInfo InitializeMediumTrust(FileInfo plug, DirectoryInfo shadowCopyPlugFolder)
        {
            var shouldCopy = true;
            var shadowCopiedPlug = new FileInfo(Path.Combine(shadowCopyPlugFolder.FullName, plug.Name));

            //check if a shadow copied file already exists and if it does, check if it's updated, if not don't copy
            if (shadowCopiedPlug.Exists)
            {
                //it's better to use LastWriteTimeUTC, but not all file systems have this property
                //maybe it is better to compare file hash?
                var areFilesIdentical = shadowCopiedPlug.CreationTimeUtc.Ticks >= plug.CreationTimeUtc.Ticks;
                if (areFilesIdentical)
                {
                    Debug.WriteLine("Not copying; files appear identical: '{0}'", shadowCopiedPlug.Name);
                    shouldCopy = false;
                }
                else
                {
                    //delete an existing file

                    //More info: http://www.nopcommerce.com/boards/t/11511/access-error-nopplugindiscountrulesbillingcountrydll.aspx?p=4#60838
                    Debug.WriteLine("New plugin found; Deleting the old file: '{0}'", shadowCopiedPlug.Name);
                    File.Delete(shadowCopiedPlug.FullName);
                }
            }

            if (shouldCopy)
            {
                try
                {
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
                }
                catch (IOException)
                {
                    Debug.WriteLine(shadowCopiedPlug.FullName + " is locked, attempting to rename");
                    //this occurs when the files are locked,
                    //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
                    //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
                    try
                    {
                        var oldFile = shadowCopiedPlug.FullName + Guid.NewGuid().ToString("N") + ".old";
                        File.Move(shadowCopiedPlug.FullName, oldFile);
                    }
                    catch (IOException exc)
                    {
                        throw new IOException(shadowCopiedPlug.FullName + " rename failed, cannot initialize plugin", exc);
                    }
                    //ok, we've made it this far, now retry the shadow copy
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
                }
            }

            return shadowCopiedPlug;
        }
    }
}