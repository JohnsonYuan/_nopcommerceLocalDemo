using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Seo;

namespace Nop.Services.Seo
{
    /// <summary>
    /// Provides information about URL records
    /// </summary>
    public partial class UrlRecordService : IUrlRecordService
    {
        public void DeleteUrlRecord(UrlRecord urlRecord)
        {
            throw new NotImplementedException();
        }

        public void DeleteUrlRecords(IList<UrlRecord> urlRecords)
        {
            throw new NotImplementedException();
        }

        public string GetActiveSlug(int entityId, string entityName, int languageId)
        {
            throw new NotImplementedException();
        }

        public IPagedList<UrlRecord> GetAllUrlRecords(string slug = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public UrlRecord GetBySlug(string slug)
        {
            throw new NotImplementedException();
        }

        public UrlRecordService.UrlRecordForCaching GetBySlugCached(string slug)
        {
            throw new NotImplementedException();
        }

        public UrlRecord GetUrlRecordById(int urlRecordId)
        {
            throw new NotImplementedException();
        }

        public IList<UrlRecord> GetUrlRecordsByIds(int[] urlRecordIds)
        {
            throw new NotImplementedException();
        }

        public void InsertUrlRecord(UrlRecord urlRecord)
        {
            throw new NotImplementedException();
        }

        public void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity, ISlugSupported
        {
            throw new NotImplementedException();
        }

        public void UpdateUrlRecord(UrlRecord urlRecord)
        {
            throw new NotImplementedException();
        }
    }
}
