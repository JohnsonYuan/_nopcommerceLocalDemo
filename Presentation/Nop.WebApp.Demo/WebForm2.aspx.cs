using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nop.WebApp.Demo
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var builder = new SqlConnectionStringBuilder(@"Data Source=|DataDirectory|\DatabaseFileName.sdf");

            Response.Write(builder.DataSource + "<br/><br/>");

            var cookies = Request.Cookies.Get("LastVisit");
            if (cookies == null)
            {
                Response.Write("Cookie is null<br/>");
            }
            else
            {
                Response.Write( cookies.Value + "<br/><br/><br/>");
            }
        }

        protected void check_Click(object sender, EventArgs e)
        {
            HttpCookie MyCookie = new HttpCookie("LastVisit");
            MyCookie.Value = DateTime.Now.Second.ToString();
            //for (int i = 0; i < 10; i++)
            //{
            //    MyCookie.Values.Add("hello" + i, i + " " + DateTime.Now.ToString());
            //}
           // MyCookie.Value = DateTime.Now.ToString();
            Response.Cookies.Add(MyCookie);


        }

        protected void check_Click2(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("languageCode");

            cookie.Value = "en";
            cookie.HttpOnly = true;
            cookie.Expires = DateTime.Now.AddHours(1);

            // MyCookie.Value = DateTime.Now.ToString();

            Response.Cookies.Remove("languageCode");
            Response.Cookies.Set(cookie);
        }

        protected void set_Click(object sender, EventArgs e)
        {
            HttpCookie MyCookie = new HttpCookie("LastVisit");
            MyCookie.Values.Clear();
            Response.Cookies.Set(MyCookie);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Clear();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
        }
    }
}