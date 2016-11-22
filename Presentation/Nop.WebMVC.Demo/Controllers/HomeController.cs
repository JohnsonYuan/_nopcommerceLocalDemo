using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.WebMVC.Demo.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index(int? id)
        { 
            return Content(HttpContext.Request.FilePath + " " + " " + HttpContext.Request.AppRelativeCurrentExecutionFilePath + " " + HttpContext.Request.AppRelativeCurrentExecutionFilePath  + " Hello world, [int] id = " + id);
        }
    }
}