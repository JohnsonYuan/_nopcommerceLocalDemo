using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework.Mvc;
using System.Web.Helpers.AntiXsrf;

namespace Nop.WebMVC.Demo.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index(int? id)
        {
            return new NullJsonResult();
            return Json(null, JsonRequestBehavior.AllowGet);
            return Json(null);
            return Content(HttpContext.Request.FilePath + " " + " " + HttpContext.Request.AppRelativeCurrentExecutionFilePath + " " + HttpContext.Request.AppRelativeCurrentExecutionFilePath  + " Hello world, [int] id = " + id);
        }   
        // GET: Home
        public ActionResult Index2(int? id)
        { 
            return Json(new { }, JsonRequestBehavior.AllowGet);
            return Json(null, JsonRequestBehavior.AllowGet);
            return Content(HttpContext.Request.FilePath + " " + " " + HttpContext.Request.AppRelativeCurrentExecutionFilePath + " " + HttpContext.Request.AppRelativeCurrentExecutionFilePath  + " Hello world, [int] id = " + id);
        }
        public ActionResult Index3(int? id)
        {

            AntiForgeryTokenSerializer serializer = new AntiForgeryTokenSerializer(new MachineKey45CryptoSystem());
            string cookieValue = "k5v9fu6On-EhZ_YOzY1voQROMQFuwZgb77zr00OB3Z20or_AD1ZP6mB17oVQ4xV7ld2U_pvPDHoc8zOimJG4t7XVQQA1";
            var cookieToken = serializer.Deserialize(cookieValue);
            var formToken2 = serializer.Deserialize("ZgFqPlKwjNHB1imWLFWOhfxo-MXpLk4hr2J1yhmPt12dPsEgyREn3VO1IMjqUcZ4gkHV6dORMDMkORxsGhaB_yGU_iJ8-ozRyGnNHtwRHLYEtpTi0");
            var formToken = serializer.Deserialize("ykbb3V33vI9ogyiq6UOo9a2g6iJGt8jz3Y5b5B-1qlTA1rBsV1N7mVpa6Uk2jPtHsWVG3R0rQgxfMr756HcjnTXg2dXY3-fCZmjdI51YK3gyTZWA0");
            //Console.WriteLine(formToken.SecurityToken);
            //Console.WriteLine(formToken2.SecurityToken);
            //Console.WriteLine(cookieToken.SecurityToken);
            ViewBag.form1 = formToken.SecurityToken;
            ViewBag.form2 = formToken2.SecurityToken;
            ViewBag.cookie = cookieToken.SecurityToken;
            return View();
        }
    }
}