using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Security;
using System.Web.SessionState;

namespace Nop.WebApp.Demo
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        { 
        }
        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();

                //store a value indicating whether profiler was started
                HttpContext.Current.Items["nop.MiniProfilerStarted"] = true;
            }
        }
        protected void Application_EndRequest()
        {
            //miniprofiler
            var miniProfilerStarted = HttpContext.Current.Items.Contains("nop.MiniProfilerStarted") &&
                 Convert.ToBoolean(HttpContext.Current.Items["nop.MiniProfilerStarted"]);
            if (miniProfilerStarted)
            {
                MiniProfiler.Stop();
            }
        }
    }
}