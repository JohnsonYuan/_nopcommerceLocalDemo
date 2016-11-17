using System;
using System.Linq;
using System.Web;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Tax;
using Nop.Services.Customers;
using Nop.Services.Vendors;
using Nop.Services.Authentication;
using Nop.Services.Localization;
using Nop.Services.Directory;
using Nop.Services.Common;
using Nop.Services.Helpers;
using Nop.Services.Stores;
using Nop.Core.Domain.Vendors;
using Nop.Web.Framework.Localization;

namespace Nop.Web.Framework
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string CustomerCookieName = "Nop.customer";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly IVendorService _vendorService;
        private readonly IStoreContext _storeContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILanguageService _languageService;
        private readonly ICurrencyService _currencyService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly TaxSettings _taxSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IUserAgentHelper _userAgentHelper;
        private readonly IStoreMappingService _storeMappingService;

        private Customer _cachedCustomer;
        private Customer _originalCustomerIfImpersonated;
        private Vendor _cachedVendor;
        private Language _cachedLanguage;
        private Currency _cachedCurrency;
        private TaxDisplayType? _cachedTaxDisplayType;

        #endregion

        #region Ctor

        public WebWorkContext(HttpContextBase httpContext,
            ICustomerService customerService,
            IVendorService vendorService,
            IStoreContext storeContext,
            IAuthenticationService authenticationService,
            ILanguageService languageService,
            ICurrencyService currencyService,
            IGenericAttributeService genericAttributeService,
            TaxSettings taxSettings,
            CurrencySettings currencySettings,
            LocalizationSettings localizationSettings,
            IUserAgentHelper userAgentHelper,
            IStoreMappingService storeMappingService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._vendorService = vendorService;
            this._storeContext = storeContext;
            this._authenticationService = authenticationService;
            this._languageService = languageService;
            this._currencyService = currencyService;
            this._genericAttributeService = genericAttributeService;
            this._taxSettings = taxSettings;
            this._currencySettings = currencySettings;
            this._localizationSettings = localizationSettings;
            this._userAgentHelper = userAgentHelper;
            this._storeMappingService = storeMappingService;
        }

        #endregion

        #region Utilities

        protected virtual HttpCookie GetCustomerCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[CustomerCookieName];
        }

        protected virtual void SetCustomerCookie(Guid customerGuid)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(CustomerCookieName);
                cookie.HttpOnly = true;
                cookie.Value = customerGuid.ToString();
                if (customerGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(CustomerCookieName);
                _httpContext.Response.Cookies.Add(cookie);
                _httpContext.Response.Cookies.Set(cookie);
            }
        }

        protected virtual Language GetLanguageFromUrl()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            string virtualPath = _httpContext.Request.AppRelativeCurrentExecutionFilePath;
            string applicationPath = _httpContext.Request.ApplicationPath;
            if (!virtualPath.IsLocalizedUrl(applicationPath, false))
                return null;

            var seoCode = virtualPath.GetLanguageSeoCodeFromUrl(applicationPath, false);
            if (String.IsNullOrEmpty(seoCode))
                return null;

            var language = _languageService
                .GetAllLanguages()
                .FirstOrDefault(l => seoCode.Equals(l.UniqueSeoCode, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published && _storeMappingService.Authorize(language))
            {
                return language;
            }

            return null;
        }

        protected virtual Language GetLanguageFromBrowserSettings()
        {
            if (_httpContext == null ||
                _httpContext.Request == null ||
                _httpContext.Request.UserLanguages == null)
                return null;

            var userLanguage = _httpContext.Request.UserLanguages.FirstOrDefault();
            if (String.IsNullOrEmpty(userLanguage))
                return null;

            var language = _languageService
                .GetAllLanguages()
                .FirstOrDefault(l => userLanguage.Equals(l.LanguageCulture, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published && _storeMappingService.Authorize(language))
            {
                return language;
            }

            return null;
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public virtual Customer CurrentCustomer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                SetCustomerCookie(value.CustomerGuid);
                _cachedCustomer = value;
            }
        }

        /// <summary>
        /// Gets or sets the original customer (in case the current one is impersonated)
        /// </summary>
        public Customer OriginalCustomerIfImpersonated
        {
            get
            {
                return _originalCustomerIfImpersonated;
            }
        }

        /// <summary>
        /// Gets or sets the current vendor (logged-in manager)
        /// </summary>
        public Customer CurrentVender
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAdmin
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public TaxDisplayType TaxDisplayType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Currency WorkingCurrency
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Language WorkingLanguage
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
