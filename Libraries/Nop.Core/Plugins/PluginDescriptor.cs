using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Plugins
{
    public class PluginDescriptor : IComparable<PluginDescriptor>
    {
        /// <summary>
        /// Plugin file name
        /// </summary>
        public virtual string PluginFileName { get; set; }

        /// <summary>
        /// Plugin Type
        /// </summary>
        public virtual Type PluinType { get; set; }

        /// <summary>
        /// The assembly that has been shadow copied that is active in the application
        /// </summary>
        public virtual Assembly ReferencedAssembly { get; set; }

        /// <summary>
        /// The original assembly file that a shadow copy was made from it
        /// </summary>
        public virtual FileInfo OriginalAssemblyFile { get; internal set; }

        /// <summary>
        /// Gets or sets the plugin group
        /// </summary>
        public virtual string Group { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public virtual string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the system name
        /// </summary>
        public virtual string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the supported versions of nopCommerce
        /// </summary>
        public virtual IList<string> SupportedVersions { get; set; }

        /// <summary>
        /// Gets or sets the author
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the list of store identifiers in which this plugin is available. If empty, then this plugin is available in all stores
        /// </summary>
        public virtual IList<int> LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether plugin is installed
        /// </summary>
        public virtual bool Installed { get; set; }

        // TODO EngineContext implementation
        public virtual T Instance<T>() where T : class, IPlugin
        {

        }

        public IPlugin Instance()
        {
            return Instance<IPlugin>();
        }

        public int CompareTo(PluginDescriptor other)
        {
            if (this.DisplayOrder != other.DisplayOrder)
                return this.DisplayOrder.CompareTo(other.DisplayOrder);
            return this.FriendlyName.CompareTo(other.FriendlyName);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PluginDescriptor;
            return other != null &&
                SystemName != null &&
                SystemName.Equals(other.SystemName);
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }
    }
}
