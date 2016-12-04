using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class InstallController : Controller
    {
        // GET: Install
        public ActionResult Index()
        {
            return Content("Hello");
        }
        // GET: Install
        [NonAction]
        public ActionResult Index2()
        {
            return Content("Hello");
        }
    }
}