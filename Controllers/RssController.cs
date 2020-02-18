using System;
using Microsoft.AspNetCore.Mvc;
using rss_feed.Services;
using rss_feed.Services.Interfaces;

namespace rss_feed.Controllers {
    [ApiController]
    public class RssController : Controller {
        private readonly string[] _rssLink;
        private readonly IArticlesLoaderService _articlesLoaderService;
        
        public RssController(IConfigLoaderService configLoaderService, IArticlesLoaderService articlesLoaderService) {
            _rssLink = configLoaderService.GetConfiguration().RssLinks;
            _articlesLoaderService = articlesLoaderService;
        }

        [HttpGet]
        [Route("api/rss")]
        public JsonResult GetRss() {
            var articles = _articlesLoaderService.LoadArticles(_rssLink);
            Console.WriteLine("Sending articles");
            return Json(articles);
        }
    }
}