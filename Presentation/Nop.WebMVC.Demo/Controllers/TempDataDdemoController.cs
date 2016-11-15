using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.WebMVC.Demo.Controllers
{
    public class TempDataDdemoController : Controller
    {
        // GET: TempDataDdemo
        public ActionResult Index()
        {
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