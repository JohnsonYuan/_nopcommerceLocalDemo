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
        }
    }
}