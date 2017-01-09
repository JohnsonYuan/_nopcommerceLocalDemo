using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework.Mvc;

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
            return View();
        }
        }
}