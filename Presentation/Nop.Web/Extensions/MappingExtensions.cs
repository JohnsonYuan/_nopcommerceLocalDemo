using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Core.Domain.Catalog;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Models.Catalog;

namespace Nop.Web.Extensions
{
    public static class MappingExtensions
    {
        //category
        public static CategoryModel ToModel(this Category entity)
        {
            if (entity == null)
                return null;

            var model = new CategoryModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle),
                SeName = entity.GetSeName(),
            };
            return model;
        }
    }
}