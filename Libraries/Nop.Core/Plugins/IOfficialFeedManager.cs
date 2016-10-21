using System.Collections.Generic;

namespace Nop.Core.Plugins
{
    /// <summary>
    /// Official feed manager (official plugins from www.nopCommerce.com site)
    /// </summary>
    public interface IOfficialFeedManager
    {
        /// <summary>
        /// Get categories
        /// </summary>
        /// <returns>Result</returns>
        IList<OfficialFeedCategory> GetCategories();

        /// <summary>
        /// Get versions
        /// </summary>
        /// <returns>Result</returns>
        IList<OfficialFeedVersion> GetVersions();

        IPagedList<OfficialFeedPlugin> GetAllPlugins(int categoryId = 0,
            int versionId = 0, int price = 0,
            string searchTerm = "",
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
