using Nop.WebMVC.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.WebMVC.Demo.Controllers
{
    public class TopicController : Controller
    {
        // GET: Topic
        public ActionResult Index()
        {
            var topic = new TopicModel
            {
                Id = 122,
                Title = "Hello world",
                Body = "this my first article"
            };
            return View(topic);
        }

        [ValidateAntiForgeryTokenWrapperAttribute(HttpVerbs.Post)]
        public ActionResult Antifogery()
        {
            return View();
        }
    }
}