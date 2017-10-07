using System.Collections.Generic;

namespace NewsFeedsLoaderLib
{
    public interface IRssLoader
    {
        List<RssItem> GetRssNews();
    }
}
