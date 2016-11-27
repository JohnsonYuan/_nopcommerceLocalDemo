using System.Collections.Generic;

namespace Nop.Web.Framework.Localization
{
    public interface ILocalizedModel
    {
    }

    public interface ILoccalizedModel<TLocalizedModel> : ILocalizedModel
    {
        IList<TLocalizedModel> Locales { get; set; }
    }
}
