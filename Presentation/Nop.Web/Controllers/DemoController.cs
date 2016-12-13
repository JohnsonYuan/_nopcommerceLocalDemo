using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class DemoController : BasePublicController
    {
        // GET: Demo
        public ActionResult Index()
        {
            return Content(ControllerContext.Controller.ToString());
        }
    }
}