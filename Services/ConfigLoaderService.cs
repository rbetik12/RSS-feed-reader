using System.Globalization;
using System.IO;
using System.Xml;
using rss_feed.Models;
using rss_feed.Services.Interfaces;

namespace rss_feed.Services {
    public class ConfigLoaderService : IConfigLoaderService {
        public Config GetConfiguration() {
            string configPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Properties" +
                                Path.DirectorySeparatorChar + "config.xml";
            XmlDocument configDocument = new XmlDocument();
            configDocument.Load(configPath);
            var root = configDocument.GetElementsByTagName("Config")[0];
            var link = root["RssLink"].InnerText;
            var updateRate = root["RefreshFrequency"].InnerText;
            return new Config(link, updateRate);
        }
    }
}