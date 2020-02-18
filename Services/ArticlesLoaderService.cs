using System;
using System.Collections.Generic;
using System.Xml;
using rss_feed.Models;
using rss_feed.Services.Interfaces;

namespace rss_feed.Services {
    public class ArticlesLoaderService : IArticlesLoaderService {
        public List<Item> LoadArticles(string url) {
            var articles = new List<Item>();
            var rss = new XmlDocument();
            try {
                rss.Load(url);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }

            var nodes = rss.GetElementsByTagName("item");
            foreach (XmlElement node in nodes) {
                var title = node.GetElementsByTagName("title")[0].InnerText;
                var link = node.GetElementsByTagName("link")[0].InnerText;
                var description = node.GetElementsByTagName("description")[0].InnerText;
                var pubDate = node.GetElementsByTagName("pubDate")[0].InnerText;
                articles.Add(new Item(DateTime.Parse(pubDate), title, RemoveHTMLTags(description), link));
            }

            return articles;
        }

        private string RemoveHTMLTags(string source) {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var let in source) {
                switch (let) {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (inside) continue;
                array[arrayIndex] = @let;
                arrayIndex++;
            }

            return new string(array, 0, arrayIndex);
        }
    }
}