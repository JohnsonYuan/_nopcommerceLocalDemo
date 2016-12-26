using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.News;
using Nop.Services.Localization;
using Nop.Web.Framework.Security;
using Nop.Web.Models.News;
using Nop.Services.Seo;
using Nop.Services.News;
using Nop.Services.Media;
using Nop.Services.Helpers;
using Nop.Services.Messages;
using Nop.Core.Caching;
using Nop.Services.Logging;
using Nop.Services.Stores;
using Nop.Services.Security;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework.Security.Captcha;
using Nop.Services.Customers;
using Nop.Services.Common;
using Nop.Web.Infrastructure.Cache;

namespace Nop.Web.Controllers
{
    [NopHttpsRequirement(SslRequirement.No)]
    public class NewsController : BasePublicController
    {
        #region Fields

        private readonly INewsService _newsService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IWebHelper _webHelper;
        private readonly ICacheManager _cacheManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPermissionService _permissionService;

        private readonly MediaSettings _mediaSettings;
        private readonly NewsSettings _newsSettings;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly CaptchaSettings _captchaSettings;


        #endregion

        #region Constructors

        public NewsController(INewsService newsService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IPictureService pictureService,
            ILocalizationService localizationService,
            IDateTimeHelper dateTimeHelper,
            IWorkflowMessageService workflowMessageService,
            IWebHelper webHelper,
            ICacheManager cacheManager,
            ICustomerActivityService customerActivityService,
            IStoreMappingService storeMappingService,
            IPermissionService permissionService,
            MediaSettings mediaSettings,
            NewsSettings newsSettings,
            LocalizationSettings localizationSettings,
            CustomerSettings customerSettings,
            CaptchaSettings captchaSettings)
        {
            this._newsService = newsService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._dateTimeHelper = dateTimeHelper;
            this._workflowMessageService = workflowMessageService;
            this._webHelper = webHelper;
            this._cacheManager = cacheManager;

            this._mediaSettings = mediaSettings;
            this._newsSettings = newsSettings;
            this._localizationSettings = localizationSettings;
            this._customerSettings = customerSettings;
            this._captchaSettings = captchaSettings;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareNewsItemModel(NewsItemModel model, NewsItem newsItem, bool prepareComments)
        {
            if (newsItem == null)
                throw new ArgumentNullException("newsItem");

            if (model == null)
                throw new ArgumentNullException("model");

            model.Id = newsItem.Id;
            model.MetaTitle = newsItem.MetaTitle;
            model.MetaDescription = newsItem.MetaDescription;
            model.MetaKeywords = newsItem.MetaKeywords;
            model.SeName = newsItem.GetSeName(newsItem.LanguageId, ensureTwoPublishedLanguages: false);
            model.Title = newsItem.Title;
            model.Short = newsItem.Short;
            model.Full = newsItem.Full;
            model.AllowComments = newsItem.AllowComments;
            model.CreatedOn = _dateTimeHelper.ConvertToUserTime(newsItem.StartDateUtc ?? newsItem.CreatedOnUtc, DateTimeKind.Utc);
            model.NumberOfComments = newsItem.CommentCount;
            model.AddNewComment.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnNewsCommentPage;
            if (prepareComments)
            {
                var newsComments = newsItem.NewsComments.OrderBy(pr => pr.CreatedOnUtc);
                foreach (var nc in newsComments)
                {
                    var commentModel = new NewsCommentModel
                    {
                        Id = nc.Id,
                        CustomerId = nc.CustomerId,
                        CustomerName = nc.Customer.FormatUserName(),
                        CommentTitle = nc.CommentTitle,
                        CommentText = nc.CommentText,
                        CreatedOn = _dateTimeHelper.ConvertToUserTime(nc.CreatedOnUtc, DateTimeKind.Utc),
                        AllowViewingProfiles = _customerSettings.AllowViewingProfiles && nc.Customer != null && !nc.Customer.IsGuest(),
                    };
                    if (_customerSettings.AllowCustomersToUploadAvatars)
                    {
                        commentModel.CustomerAvatarUrl = _pictureService.GetPictureUrl(
                            nc.Customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId),
                            _mediaSettings.AvatarPictureSize,
                            _customerSettings.DefaultAvatarEnabled,
                            defaultPictureType: PictureType.Avatar);
                    }
                    model.Comments.Add(commentModel);
                }
            }
        }

        #endregion

        #region Methods

        public ActionResult HomePageNews()
        {
            if (!_newsSettings.Enabled || !_newsSettings.ShowNewsOnMainPage)
                return Content("");

            var cacheKey = string.Format(ModelCacheEventConsumer.HOMEPAGE_NEWSMODEL_KEY, _workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id);
            var cachedModel = _cacheManager.Get(cacheKey, () =>
            {
                var newsItems = _newsService.GetAllNews(_workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id, 0, _newsSettings.MainPageNewsCount);
                return new HomePageNewsItemsModel
                {
                    WorkingLanguageId = _workContext.WorkingLanguage.Id,
                    NewsItems = newsItems
                        .Select(x =>
                                    {
                                        var newsModel = new NewsItemModel();
                                        PrepareNewsItemModel(newsModel, x, false);
                                        return newsModel;
                                    })
                        .ToList()
                };
            });

            //"Comments" property of "NewsItemModel" object depends on the current customer.
            //Furthermore, we just don't need it for home page news. So let's reset it.
            //But first we need to clone the cached model (the updated one should not be cached)
            var model = (HomePageNewsItemsModel)cachedModel.Clone();
            foreach (var newsItemModel in model.NewsItems)
                newsItemModel.Comments.Clear();
            return PartialView(model);
        }

        public ActionResult List(NewsPagingFilteringModel command)
        {
            if (!_newsSettings.Enabled)
                return RedirectToRoute("HomePage");

            var model = new NewsItemListModel();
            model.WorkingLanguageId = _workContext.WorkingLanguage.Id;

            if (command.PageSize <= 0) command.PageSize = _newsSettings.NewsArchivePageSize;
            if (command.PageNumber <= 0) command.PageNumber = 1;

            var newsItems = _newsService.GetAllNews(_workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id,
                command.PageNumber - 1, command.PageSize);
            model.PagingFilteringContext.LoadPagedList(newsItems);

            model.NewsItems = newsItems
                .Select(x =>
                {
                    var newsModel = new NewsItemModel();
                    PrepareNewsItemModel(newsModel, x, false);
                    return newsModel;
                })
                .ToList();

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult RssHeaderLink()
        {
            if (!_newsSettings.Enabled || !_newsSettings.ShowHeaderRssUrl)
                return Content("");

            string link = string.Format("<link href=\"{0}\" rel=\"alternate\" type=\"{1}\" title=\"{2}: News\" />",
                Url.RouteUrl("NewsRSS", new { languageId = _workContext.WorkingLanguage.Id }, _webHelper.IsCurrentConnectionSecured() ? "https" : "http"), MimeTypes.ApplicationRssXml, _storeContext.CurrentStore.GetLocalized(x => x.Name));

            return Content(link);
        }

        #endregion
    }
}