﻿using System;
using System.Web.Mvc;
using Nop.Core.Data;
using Nop.Core.Domain.Security;
using Nop.Core.Infrastructure;

namespace Nop.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AdminAntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;

        /// <summary>
        /// Anti-forgery security attribute
        /// </summary>
        /// <param name="ignore">Pass false in order to ignore this security validation</param>
        public AdminAntiForgeryAttribute(bool ignore)
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

            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase))
                return;

            if (DataSettingsHelper.DatabaseIsInstalled())
                return;

            var securitySettings = EngineContext.Current.Resolve<SecuritySettings>();
            if (!securitySettings.EnableXsrfProtectionForAdminArea)
                return;

            var validator = new ValidateAntiForgeryTokenAttribute();
            validator.OnAuthorization(filterContext);
        }
    }
}
