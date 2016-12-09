using System.Web.Mvc;
using Nop.Core;
using Nop.Web.Models.Common;
using Nop.Core.Domain.Tax;
using System;

namespace Nop.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly IWebHelper _webHelper;
        private readonly TaxSettings _taxSettings;

        #region Constructors

        public CommonController(IStoreContext storeContext,
            IWebHelper webHelper,
            IWorkContext workContext,
            TaxSettings taxSettings)
        {
            this._storeContext = storeContext;
            this._webHelper = webHelper;
            this._workContext = workContext;
            this._taxSettings = taxSettings;
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region Methods

        //tax type
        [ChildActionOnly]
        public ActionResult TaxTypeSelector()
        {
            if (!_taxSettings.AllowCustomersToSelectTaxDisplayType)
                return Content("");

            var model = new TaxTypeSelectorModel
            {
                CurrentTaxType = _workContext.TaxDisplayType
            };

            return PartialView(model);
        }
        //available even when navigation is not allowed
        public ActionResult SetTaxType(int cusomterTaxType, string returnUrl = "")
        {
            var taxDisplayType = (TaxDisplayType)Enum.ToObject(typeof(TaxDisplayType), cusomterTaxType);
            _workContext.TaxDisplayType = taxDisplayType;

            //home page
            if (String.IsNullOrEmpty(returnUrl))
                returnUrl = Url.RouteUrl("HomePage");

            //prevent open redirection attach
            if (!Url.IsLocalUrl(returnUrl))
                returnUrl = Url.RouteUrl("Home");

            return Redirect(returnUrl);
        }

        //page not found
        public ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;

            return View();
        }

        //favicon
        public ActionResult Favicon()
        {
            //try loading a store specific favicon
            var faviconFileName = string.Format("favicon-{0}.ico", _storeContext.CurrentStore.Id);
            var localFaviconPath = System.IO.Path.Combine(Request.PhysicalApplicationPath, faviconFileName);
            if (!System.IO.File.Exists(localFaviconPath))
            {
                //try loading a generic favicon
                faviconFileName = "favicon.ico";
                localFaviconPath = System.IO.Path.Combine(Request.PhysicalApplicationPath, faviconFileName);
                if (!System.IO.File.Exists(localFaviconPath))
                {
                    return Content("");
                }
            }
            var model = new FaviconModel
            {
                FaviconUrl = _webHelper.GetStoreLocation() + faviconFileName
            };
            return PartialView(model);
        }

        #endregion
    }
}