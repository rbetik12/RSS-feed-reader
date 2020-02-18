using System.Xml.Serialization;

namespace rss_feed.Models {
    public class Config {
        public string RefreshFrequency { get; set; }
        public string[] RssLinks { get; set; }
        public Config(string[] link, string refreshFrequency) {
            RssLinks = link;
            RefreshFrequency = refreshFrequency;
        }

        public Config() {
        }

        public override string ToString() {
            string links = "";
            foreach (var link in RssLinks) {
                links += link + '\n';
            }
            return links + '\n' + RefreshFrequency;
        }
    }
}