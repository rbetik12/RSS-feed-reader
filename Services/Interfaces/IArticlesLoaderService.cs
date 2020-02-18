using System.Collections.Generic;
using rss_feed.Models;

namespace rss_feed.Services.Interfaces {
    public interface IArticlesLoaderService {
        public List<Item> LoadArticles(string url);
    }
}