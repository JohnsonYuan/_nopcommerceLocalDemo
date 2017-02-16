using System.Collections.Generic;

namespace Nop.Web.Models.Common
{
    public partial class LanguageSelectorModel
    {
        public LanguageSelectorModel()
        {
            AvailableLanguages = new List<LanguageModel>();
        }
        public IList<LanguageModel> AvailableLanguages { get; set; }
        public int CurrentLanguageId { get; set; }
        public bool UseImages { get; set; }
    }
}