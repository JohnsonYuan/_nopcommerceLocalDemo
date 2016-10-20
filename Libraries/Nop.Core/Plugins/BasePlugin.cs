using System;

namespace Nop.Core.Plugins
{
    /// <summary>
    /// Base plugin
    /// </summary>
    public abstract class BasePlugin : IPlugin
    {
        /// <summary>
        /// Gets or sets the plugin descriptor
        /// </summary>
        public virtual PluginDescriptor PluginDescriptor { get; set; }

        /// <summary>
        /// Install plugin
        /// </summary>
        public void Install()
        {

        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public void Uninstall()
        {
        }
    }
}
