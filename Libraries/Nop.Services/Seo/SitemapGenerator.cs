using System;
using System.Web.Mvc;
using System.Xml;
using Nop.Core;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Security;
using Nop.Services.Catalog;
using Nop.Services.Topics;
using System.IO;

namespace Nop.Services.Seo
{
    /// <summary>
    /// Represents a sitemap generator
    /// </summary>
    public partial class SitemapGenerator : ISitemapGenerator
    {
        #region Fields

        private readonly IStoreContext _storeContext;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ITopicService _topicService;
        private readonly CommonSettings _commonSettings;
        private readonly BlogSettings _blogSettings;
        private readonly NewsSettings _newsSettings;
        private readonly ForumSettings _forumSettings;
        private readonly SecuritySettings _securitySettings;

        private const string DateFormat = @"yyyy-MM-dd";
        private XmlTextWriter _writer;

        #endregion


        #region Ctor
        public SitemapGenerator(IStoreContext storeContext,
            ICategoryService categoryService,
            IProductService productService,
            IManufacturerService manufacturerService,
            ITopicService topicService,
            CommonSettings commonSettings,
            BlogSettings blogSettings,
            NewsSettings newsSettings,
            ForumSettings forumSettings,
            SecuritySettings securitySettings)
        {
            this._storeContext = storeContext;
            this._categoryService = categoryService;
            this._productService = productService;
            this._manufacturerService = manufacturerService;
            this._topicService = topicService;
            this._commonSettings = commonSettings;
            this._blogSettings = blogSettings;
            this._newsSettings = newsSettings;
            this._forumSettings = forumSettings;
            this._securitySettings = securitySettings;
        }

        #endregion

        #region Utilities

        protected virtual string GetHttpProtocol()
        {
            return _securitySettings.ForceSslForAllPages ? "https" : "http";
        }



        #endregion

        #region Methods

        /// <summary>
        /// This will build an xml sitemap for better index with search engines.
        /// See http://en.wikipedia.org/wiki/Sitemaps for more information.
        /// </summary>
        /// <param name="urlHelper">URL helper</param>
        /// <returns>Sitemap.xml as string</returns>
        public string Generate(UrlHelper urlHelper)
        {
            throw new NotImplementedException();
        }

        public void Generate(UrlHelper urlHelper, Stream stream)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
