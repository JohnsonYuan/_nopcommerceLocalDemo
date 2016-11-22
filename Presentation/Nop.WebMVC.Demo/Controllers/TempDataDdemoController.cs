using System.Web;
using System.Web.Mvc;

namespace Nop.WebMVC.Demo.Controllers
{
    public class TempDataDdemoController : Controller
    {
        public virtual string GetStoreHost(bool useSsl)
        {
            var result = "";
            var httpHost = Request.ServerVariables["HTTP_HOST"];
            if (!string.IsNullOrEmpty(httpHost))
            {
                result = "http://" + httpHost;
                if (!result.EndsWith("/"))
                    result += "/";
            }
            return result;
        }
        public virtual string GetStoreLocation(bool useSsl)
        {
            //return HostingEnvironment.ApplicationVirtualPath;

            string result = GetStoreHost(useSsl);
            if (result.EndsWith("/"))
                result = result.Substring(0, result.Length - 1);
            if (Request != null)
                result = result + Request.ApplicationPath;
            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        // GET: TempDataDdemo
        public ActionResult Index()
        {
            var response = System.Web.HttpContext.Current.Response;
            response.Status = "301 Moved Permanently";
            response.RedirectLocation = GetStoreLocation(false) + "WebForm1.aspx";
            response.End();
            return View();
        }

        // GET: TempDataDdemo
        public ActionResult Index2()
        {
            Response.Redirect("~/Webform1.aspx");
            return View();
        }

        public ActionResult Foo()
        {
            // store something into the tempdata that will be available during a single redirect
            TempData["foo"] = "bar";

            // you should always redirect if you store something into TempData to
            // a controller action that will consume this data
            return RedirectToAction("bar");
        }

        public ActionResult Bar()
        {
            var foo = TempData["foo"];
            return View();
        }
    }
}