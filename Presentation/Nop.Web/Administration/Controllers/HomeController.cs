using System;
using System.Collections.Generic;
using System.Linq; 
using System.Web.Mvc;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}