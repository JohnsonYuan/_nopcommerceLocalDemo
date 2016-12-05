using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Cms;
using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class WidgetController : Controller
    {
        #region Fields

        private readonly IWidgetService _widgetService;
        private readonly IStoreContext _storeContext;
        private readonly IThemeContext _themeContext;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public WidgetController(IWidgetService widgetService,
            IStoreContext storeContext,
            IThemeContext themeContext,
            ICacheManager cacheManager)
        {
            this._widgetService = widgetService;
            this._storeContext = storeContext;
            this._themeContext = themeContext;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        // GET: Widget
        public ActionResult WidgetsByZone(string widgetZone, object additionalData = null)
        {

            return View();
        }

        #endregion
    }
}