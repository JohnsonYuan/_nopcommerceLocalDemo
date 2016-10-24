using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Compilation;

//[assembly: PreApplicationStartMethod(typeof(Nop.Core.Plugins.Demo), "Initialize")]
namespace Nop.Core.Plugins
{
    public class Demo
    {
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        public static void Initialize()
        {
            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.DynamicDirectory + "\\demo1.txt"))
            {
                writer.WriteLine(AppDomain.CurrentDomain.GetAssemblies().Count());

                foreach (var item in AppDomain.CurrentDomain.GetAssemblies().OrderBy(x=>x.FullName))
                {
                    writer.WriteLine(item.FullName);
                }
            }
            FileInfo shadowCopiedPlug2 = new FileInfo(@"E:\Source\Repos\nopCommerce_3.80_Beta_Source\Libraries\Nop.Core\bin\Debug\Autofac.dll");
            var an2 = AssemblyName.GetAssemblyName(shadowCopiedPlug2.FullName);
            var shadowCopiedAssembly = Assembly.Load(an2);
           
             //var shadowCopiedAssembly = Assembly.Load();
            //BuildManager.AddReferencedAssembly(shadowCopiedAssembly);
            AppDomain.CurrentDomain.Load(an2);
            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.DynamicDirectory + "\\demo2.txt"))
            {
                writer.WriteLine(AppDomain.CurrentDomain.GetAssemblies().Count());
                foreach (var item in AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName))
                {
                    writer.WriteLine(item.FullName);
                }
            }
        }
    }

}