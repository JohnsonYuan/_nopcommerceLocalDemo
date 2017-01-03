using System;
using System.Web.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;

namespace Nop.Admin.Controllers
{
    // 我的注释
    [NopHttpsRequirement(SslRequirement.Yes)] // SslRequirement.Yes 且 当前store.SslEnabled为true， 跳转到https
    [AdminValidateIpAddress] // 当前用户ip是否在SecuritySettings.AdminAreaAllowedIpAddresses 里，不在则跳转到admin/security/accessdenied
    [AdminAuthorize]        // 当前action， contorller是否有AdminAuthorizeAttribute属性， 如果有继续验证是否有权限StandardPermissionProvider.AccessAdminPanel
    [AdminAntiForgery]      // post时候验证AntiForgery
    [AdminVendorValidation] // 如果当前 workContext.CurrentCustomer.IsVendor()， 再检查workContext.CurrentCustomer == null? （vendor是否激活）
    public abstract partial class BaseAdminController : BaseController
    {

    }
}