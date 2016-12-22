using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.News;
using Nop.Services.Localization;

namespace Nop.Web.Controllers
{
    public class NewsController : ProductController
    {
        #region Fields

        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly NewsSettings _newsSettings;

        #endregion

        #region Constructors

        public NewsController(IWebHelper webHelper,
            IWorkContext workContext,
            IStoreContext storeContext,
            NewsSettings newsSettings)
        {
            this._webHelper = webHelper;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._newsSettings = newsSettings;
        }

        #endregion

        #region Utilities

        [ChildActionOnly]
        public ActionResult RssHeaderLink()
        {
            if (!_newsSettings.Enabled || !_newsSettings.ShowHeaderRssUrl)
                return Content("");

            string link = string.Format("<link href=\"{0}\" rel=\"alternate\" type=\"{1}\" title=\"{2}: News\" />",
                Url.RouteUrl("NewsRSS", new { languageId = _workContext.WorkingLanguage.Id }, _webHelper.IsCurrentConnectionSecured() ? "https" : "http"), MimeTypes.ApplicationRssXml, _storeContext.CurrentStore.GetLocalized(x => x.Name));

            return Content(link);
        }

        #endregion
    }
}