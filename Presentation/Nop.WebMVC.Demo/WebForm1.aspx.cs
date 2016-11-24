using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Nop.WebMVC.Demo.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
            //var document = new XmlDocument();
            //document.LoadXml("");

            //Response.Charset = "utf-8";
            Response.ContentType = "text/xml";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "web.demo"));
            Response.BinaryWrite(Encoding.ASCII.GetBytes("Hello johnson"));
            Response.WriteFile("D:\\web.config");
            Response.WriteFile("D:\\ILogger.cs");
  
            // 
            Response.End();
        }
    }
}