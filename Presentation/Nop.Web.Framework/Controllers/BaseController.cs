using System;
using System.Web.Mvc;

namespace Nop.Web.Framework.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    [StoreIpAddress]
    [CustomerLastActivity]
    [StoreLastVisitedPage]
    public abstract class BaseController : Controller
    {
    }
}
