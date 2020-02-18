using System.Xml.Serialization;

namespace rss_feed.Models {
    public class Config {
        [XmlElement(DataType = "string")]
        public string RssLink { get; set; }
        [XmlElement(DataType = "string")]
        public string RefreshFrequency { get; set; }

        public Config(string link, string refreshFrequency) {
            RssLink = link;
            RefreshFrequency = refreshFrequency;
        }

        public Config() {
        }

        public override string ToString() {
            return RssLink + " " + RefreshFrequency;
        }
    }
}