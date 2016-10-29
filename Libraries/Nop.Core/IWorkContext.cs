using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Tax;

namespace Nop.Core
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        Customer CurrentCustomer { get; set; }
        /// <summary>
        /// Gets or sets the original customer (in case the current one is impersonated)
        /// </summary>
        Customer OriginalCustomerIfImpersonated { get; }
        /// <summary>
        /// Gets or sets the current vender (logged-in manager)
        /// </summary>
        Customer CurrentVender { get; }

        /// <summary>
        /// Gets or sets the current working language
        /// </summary>
        Language WorkingLanguage { get; }
        /// <summary>
        /// Gets or sets the current working currency
        /// </summary>
        Currency WorkingCurrency { get; }
        /// <summary>
        /// Gets or sets the current tax display type
        /// </summary>
        TaxDisplayType TaxDisplayType { get; }

        /// <summary>
        /// Gets or sets value indicating whether we're in admin area
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
