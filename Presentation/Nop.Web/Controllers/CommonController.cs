using System.Web.Mvc;
using Nop.Core;
using Nop.Web.Models.Common;

namespace Nop.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly IStoreContext _storeContext;
        private readonly IWebHelper _webHelper;

        #region Constructors

        public CommonController(IStoreContext storeContext,
            IWebHelper webHelper)
        {
            this._storeContext = storeContext;
            this._webHelper = webHelper;
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region Methods

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