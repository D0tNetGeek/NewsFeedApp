using System;

namespace NewsFeedsLoaderLib
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
        public string Guid { get; set; }
        public DateTime PublishDate { get; set; }
        public int MediaWidth { get; set; }
        public int MediaHeight { get; set; }
        public string MediaUrl { get; set; }
    }
}
