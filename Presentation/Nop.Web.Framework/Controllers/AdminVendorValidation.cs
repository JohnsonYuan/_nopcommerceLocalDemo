using System;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Domain.Customers;

namespace Nop.Web.Framework.Controllers
{
    /// <summary>
    /// Attribute to ensure that users with "Vendor" customer role has appropriate vendor account associated (and active)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminVendorValidation : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;

        public AdminVendorValidation(bool ignore = false)
        {
            this._ignore = ignore;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (_ignore)
                return;

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;

            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            if (!workContext.CurrentCustomer.IsVendor())
                return;

            //ensure that this user has active vendor record associated
            if (workContext.CurrentVendor == null)
                filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
