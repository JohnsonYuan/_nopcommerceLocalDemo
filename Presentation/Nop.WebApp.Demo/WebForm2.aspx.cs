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
            for (int i = 0; i < 10; i++)
            {
                MyCookie.Values.Add("hello" + i, i + " " + DateTime.Now.ToString());
            }
           // MyCookie.Value = DateTime.Now.ToString();
            Response.Cookies.Add(MyCookie);
        }

        protected void set_Click(object sender, EventArgs e)
        {
            HttpCookie MyCookie = new HttpCookie("LastVisit");
            MyCookie.Values.Clear();
            Response.Cookies.Set(MyCookie);
        }
    }
}