using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Areas.DemoArea.Controllers
{
    public class DefaultController : Controller
    {
        // GET: DemoArea/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}