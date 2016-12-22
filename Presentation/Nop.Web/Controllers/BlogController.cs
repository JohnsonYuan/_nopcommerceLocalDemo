using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Blogs;
using Nop.Services.Localization;

namespace Nop.Web.Controllers
{
    public class BlogController : BasePublicController
    {
        #region Fields

        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly BlogSettings _blogSettings;

        #endregion

        #region Constructors

        public BlogController(IWebHelper webHelper,
            IWorkContext workContext,
            IStoreContext storeContext,
            BlogSettings blogSettings)
        {
            this._webHelper = webHelper;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._blogSettings = blogSettings;
        }

        #endregion

        #region Utilities

        [ChildActionOnly]
        public ActionResult RssHeaderLink()
        {
            if (!_blogSettings.Enabled || !_blogSettings.ShowHeaderRssUrl)
                return Content("");

            string link = string.Format("<link href=\"{0}\" rel=\"alternate\" type=\"{1}\" title=\"{2}: Blog\" />",
                Url.RouteUrl("BlogRSS", new { languageId = _workContext.WorkingLanguage.Id }, _webHelper.IsCurrentConnectionSecured() ? "https" : "http"), MimeTypes.ApplicationRssXml, _storeContext.CurrentStore.GetLocalized(x => x.Name));

            return Content(link);
        }
        #endregion
    }
}