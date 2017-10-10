using NewsFeedsLoaderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace NewsFeeds.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ShowFeeds(string category, string order, bool? isDesc)
        {
            List<RssItem> items = new List<RssItem>();
            IRssLoader loader;

            if (string.IsNullOrEmpty(category) || category == "BBC News UK")
            {
                loader = new RssLoaderBbc("http://feeds.bbci.co.uk/news/uk/rss.xml");
                items.AddRange(loader.GetRssNews());
            }

            if (string.IsNullOrEmpty(category) || category == "BBC News Technology")
            {
                loader = new RssLoaderBbc("http://feeds.bbci.co.uk/news/technology/rss.xml");
                items.AddRange(loader.GetRssNews());
            }

            if (string.IsNullOrEmpty(category) || category == "Reuters UK")
            {
                loader = new RssLoaderRruter("http://feeds.reuters.com/reuters/UKdomesticNews?format=xml");
                items.AddRange(loader.GetRssNews());
            }

            if (string.IsNullOrEmpty(category) || category == "Reuters Technology")
            {
                loader = new RssLoaderRruter("http://feeds.reuters.com/reuters/technologyNews?format=xml");
                items.AddRange(loader.GetRssNews());
            }

            if (order == "Title")
                items = items.OrderBy(i => i.Title).ToList();
            else if (order == "PublishDate")
                items = items.OrderBy(i => i.PublishDate).ToList();

            if (isDesc != null && isDesc.Value)
                items.Reverse();

            ViewBag.Category = category;
            ViewBag.order = order;
            ViewBag.isDesc = isDesc;

            return View(items);
        }

        public ActionResult DownloadFile(string url)
        {
            HttpClient client = new HttpClient {BaseAddress = new Uri(url)};

            var stream = client.GetStreamAsync("").Result;

            return File(stream, "text/html", "news.html");
        }
    }
}