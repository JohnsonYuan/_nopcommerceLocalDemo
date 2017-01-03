using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Services.Authentication;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Framework.Security.Honeypot;
using Nop.Web.Models.Common;
using Nop.Web.Models.Customer;

namespace Nop.Web.Controllers
{
    public class CustomerController : BasePublicController
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly TaxSettings _taxSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ITaxService _taxService;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly AddressSettings _addressSettings;
        private readonly ForumSettings _forumSettings;
        private readonly OrderSettings _orderSettings;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly IDownloadService _downloadService;
        private readonly IWebHelper _webHelper;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IReturnRequestService _returnRequestService;
        private readonly IEventPublisher _eventPublisher;

        private readonly MediaSettings _mediaSettings;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly StoreInformationSettings _storeInformationSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly VendorSettings _vendorSettings;

        #endregion

        #region Ctor

        public CustomerController(IAuthenticationService authenticationService,
            IDateTimeHelper dateTimeHelper,
            DateTimeSettings dateTimeSettings,
            TaxSettings taxSettings,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            ICustomerService customerService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService,
            ITaxService taxService,
            RewardPointsSettings rewardPointsSettings,
            CustomerSettings customerSettings,
            AddressSettings addressSettings,
            ForumSettings forumSettings,
            OrderSettings orderSettings,
            IAddressService addressService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IOrderService orderService,
            IPictureService pictureService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IShoppingCartService shoppingCartService,
            IOpenAuthenticationService openAuthenticationService,
            IDownloadService downloadService,
            IWebHelper webHelper,
            ICustomerActivityService customerActivityService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeFormatter addressAttributeFormatter,
            IReturnRequestService returnRequestService,
            IEventPublisher eventPublisher,
            MediaSettings mediaSettings,
            IWorkflowMessageService workflowMessageService,
            LocalizationSettings localizationSettings,
            CaptchaSettings captchaSettings,
            SecuritySettings securitySettings,
            ExternalAuthenticationSettings externalAuthenticationSettings,
            StoreInformationSettings storeInformationSettings,
            CatalogSettings catalogSettings,
            VendorSettings vendorSettings)
        {
            this._authenticationService = authenticationService;
            this._dateTimeHelper = dateTimeHelper;
            this._dateTimeSettings = dateTimeSettings;
            this._taxSettings = taxSettings;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeMappingService = storeMappingService;
            this._customerService = customerService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
            this._taxService = taxService;
            this._rewardPointsSettings = rewardPointsSettings;
            this._customerSettings = customerSettings;
            this._addressSettings = addressSettings;
            this._forumSettings = forumSettings;
            this._orderSettings = orderSettings;
            this._addressService = addressService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._orderService = orderService;
            this._pictureService = pictureService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._shoppingCartService = shoppingCartService;
            this._openAuthenticationService = openAuthenticationService;
            this._downloadService = downloadService;
            this._webHelper = webHelper;
            this._customerActivityService = customerActivityService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._returnRequestService = returnRequestService;
            this._eventPublisher = eventPublisher;
            this._mediaSettings = mediaSettings;
            this._workflowMessageService = workflowMessageService;
            this._localizationSettings = localizationSettings;
            this._captchaSettings = captchaSettings;
            this._securitySettings = securitySettings;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
            this._storeInformationSettings = storeInformationSettings;
            this._catalogSettings = catalogSettings;
            this._vendorSettings = vendorSettings;
        }

        #endregion

        #region Login / logout

        [NopHttpsRequirement(SslRequirement.Yes)]
        //available even when a store is closed
        [StoreClosed(true)]
        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        public ActionResult Login(bool? checkoutAsGuest)
        {
            var model = new LoginModel();
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.CheckoutAsGuest = checkoutAsGuest.GetValueOrDefault();
            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage;
            return View(model);
        }

        [HttpPost]
        [CaptchaValidator]
        //available even when a store is closed
        [StoreClosed(true)]
        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        public ActionResult Login(LoginModel model, string returnUrl, bool captchaValid)
        {
            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage && !captchaValid)
            {
                ModelState.AddModelError("", _captchaSettings.GetWrongCaptchaMessage(_localizationService));
            }

            if (ModelState.IsValid)
            {
                if (_customerSettings.UsernamesEnabled && model.Username != null)
                {
                    model.Username = model.Username.Trim();
                }
                var loginResult = _customerRegistrationService.ValidateCustomer(_customerSettings.UsernamesEnabled ? model.Username : model.Email, model.Password);
                switch (loginResult)
                {
                    case CustomerLoginResults.Successful:
                        {
                            var customer = _customerSettings.UsernamesEnabled ? _customerService.GetCustomerByUsername(model.Username) : _customerService.GetCustomerByEmail(model.Email);

                            //migrate shopping cart
                            _shoppingCartService.MigrateShoppingCart(_workContext.CurrentCustomer, customer, true);

                            //sign in new customer
                            _authenticationService.SignIn(customer, model.RememberMe);

                            //raise event       
                            _eventPublisher.Publish(new CustomerLoggedinEvent(customer));

                            //activity log
                            _customerActivityService.InsertActivity("PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"), customer);

                            if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                return RedirectToRoute("HomePage");

                            return Redirect(returnUrl);
                        }
                        break;
                    case CustomerLoginResults.CustomerNotExist:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.CustomerNotExist"));
                        break;
                    case CustomerLoginResults.Deleted:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.Deleted"));
                        break;
                    case CustomerLoginResults.NotActive:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotActive"));
                        break;
                    case CustomerLoginResults.NotRegistered:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered"));
                        break;
                    case CustomerLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials"));
                        break;
                }
            }

            //If we got this far, something failed, redisplay form
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage;
            return View(model);
        }

        #endregion
    }
}