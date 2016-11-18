using System;
using System.Web.Mvc;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core;
using Nop.Services.Customers;

namespace Nop.Web.Framework
{
    /// <remarks>
    /// 我的注释
    /// 更新用户最近一次操作时间（超过当前时间1分钟才更新）
    /// </remarks>
    public class CustomerLastActivityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
                return;

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;

            //only GET requests
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var customer = workContext.CurrentCustomer;

            //update last activity date
            if (customer.LastActivityDateUtc.AddMinutes(1.0) < DateTime.UtcNow)
            {
                var customerService = EngineContext.Current.Resolve<ICustomerService>();
                customer.LastActivityDateUtc = DateTime.UtcNow;
                customerService.UpdateCustomer(customer);
            }
        }
    }
}
