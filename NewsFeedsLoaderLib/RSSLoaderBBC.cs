using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace NewsFeedsLoaderLib
{
    public class RssLoaderBbc : IRssLoader
    {
        private readonly string _url;
        public RssLoaderBbc(string url)
        {
            _url = url;
        }
        RssItem LoadItem(XmlNode itemNode)
        {
            try
            {
                RssItem item = new RssItem
                {
                    Title = itemNode.ChildNodes[0].FirstChild.Value,
                    Description = itemNode.ChildNodes[1].FirstChild.Value,
                    Link = itemNode.ChildNodes[2].FirstChild.Value,
                    Guid = itemNode.ChildNodes[3].FirstChild.Value,
                    PublishDate = DateTime.Parse(itemNode.ChildNodes[4].FirstChild.Value),
                    MediaWidth = int.Parse(itemNode.ChildNodes[5].Attributes[0].Value),
                    MediaHeight = int.Parse(itemNode.ChildNodes[5].Attributes[1].Value),
                    MediaUrl = itemNode.ChildNodes[5].Attributes[2].Value
                };

                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RssItem> GetRssNews()
        {
            WebClient wclient = new WebClient();
            wclient.DownloadString(_url);

            XmlDocument document = new XmlDocument();
            document.Load(_url);

            var nodes=document.SelectNodes("//*");

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
