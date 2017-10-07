using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;

namespace NewsFeedsLoaderLib
{
    public class RssLoaderRruter : IRssLoader
    {
        private readonly string _url;
        public RssLoaderRruter(string url)
        {
            _url = url;
        }

        RssItem LoadItem(XmlNode itemNode)
        {
            RssItem item = new RssItem
            {
                Title = itemNode.ChildNodes[0].FirstChild.Value,
                Description = new string(itemNode.ChildNodes[1].FirstChild.Value.TakeWhile(c => c != '<').ToArray()),
                Link = itemNode.ChildNodes[2].FirstChild.Value,
                Guid = itemNode.ChildNodes[3].FirstChild.Value,
                Category = itemNode.ChildNodes[4].FirstChild.Value,
                PublishDate = DateTime.Parse(itemNode.ChildNodes[5].FirstChild.Value)
            };
            return item;

        }
        public List<RssItem> GetRssNews()
        {
            WebClient wclient = new WebClient();
            wclient.DownloadString(_url);

            XmlDocument document = new XmlDocument();
            document.Load(_url);

            var nodes = document.SelectNodes("//*");

            List<RssItem> result = new List<RssItem>();

            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].Name == "item")
                        result.Add(LoadItem(nodes[i]));
                }
            }

            return result;
        }
    }
}
