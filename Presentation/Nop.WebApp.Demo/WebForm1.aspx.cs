using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nop.WebApp.Demo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie MyCookie = new HttpCookie("LastVisit");
            MyCookie.Value = DateTime.Now.ToString();
            Response.Cookies.Set(MyCookie);

            HttpCookie MyCookie2 = new HttpCookie("LastVisit2");
            MyCookie2.Value = "HELLO WORLD";
            MyCookie2.HttpOnly = true;
            Response.Cookies.Set(MyCookie2);
            if (Request.Cookies.Get("sdfsdf") == null)
            {
                Response.Write("null");
            }
            else
            {
                Response.Write("null");
            }
            Response.Write("<br/>");
        }
    }
}