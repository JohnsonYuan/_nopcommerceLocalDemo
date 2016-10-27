using Nop.Core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Localization
{
    public class Language : BaseEntity, IStoreMappingSupported
    {
        private ICollection<LocaleStringResource> _localeStringResources;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language culture
        /// </summary>
        public string LanguageCulture { get; set; }

        /// <summary>
        /// Gets or sets the unique SEO code
        /// </summary>
        public string UniqueSeoCode { get; set; }

        /// <summary>
        /// Gets or sets the flag image file name
        /// </summary>
        public string FlagImageFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the language support "Right-to-left"
        /// </summary>
        public bool Rtl { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the default currency for this language;
        /// 0 is set when we use the default display order
        /// </summary>
        public int DefaultCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the language is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the locale string resources
        /// </summary>
        public ICollection<LocaleStringResource> LocaleStringResources
        {
            get
            {
                return _localeStringResources ?? (_localeStringResources = new List<LocaleStringResource>());
            }
            protected set
            {
                _localeStringResources = value;
            }
        }
    }
}
