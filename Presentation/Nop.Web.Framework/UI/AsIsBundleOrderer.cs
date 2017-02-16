using System;
using System.Collections.Generic;
using System.Web.Optimization;

namespace Nop.Web.Framework.UI
{
    public partial class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
