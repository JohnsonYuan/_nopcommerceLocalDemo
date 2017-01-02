using System.Web.Mvc;

namespace Nop.Admin.Areas.DemoArea
{
    public class DemoAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DemoArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DemoArea_default",
                "DemoArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                , new[] { "Nop.Admin.Areas.DemoArea.Controllers" }
            );
        }
    }
}