using System.Collections.Generic;
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

            var linksNode = root["RssLinks"];
            List<string> links = new List<string>();
            foreach (XmlNode node in linksNode.ChildNodes) {
                links.Add(node.InnerText);
            }
            var updateRate = root["RefreshFrequency"].InnerText;
            return new Config(links.ToArray(), updateRate);
        }
    }
}