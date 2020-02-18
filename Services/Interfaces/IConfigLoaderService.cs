using rss_feed.Models;

namespace rss_feed.Services.Interfaces {
    public interface IConfigLoaderService {
        public Config GetConfiguration();
    }
}