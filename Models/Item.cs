using System;

namespace rss_feed.Models {
    public class Item : IComparable<Item> {
        public DateTime PubDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public Item(DateTime date, string title, string description, string link) {
            this.Description = description;
            this.Title = title;
            PubDate = date;
            this.Link = link;
        }

        public int CompareTo(Item other) {
            if (other == null) {
                return 1;
            }

            return PubDate.CompareTo(other.PubDate);
        }
    }
}