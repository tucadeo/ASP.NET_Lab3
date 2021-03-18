using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using ASP.NET.Lab3.Models;

namespace ASP.NET.Lab3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            const string url = "https://www.techradar.com/rss";

            var xml = XElement.Load(url);

            var items = xml.Descendants("item")
                .Select(n => new RssItem
                {
                    Title = n.Element("title")?.Value,
                    Description = n.Element("description")?.Value,
                    Image = n.Descendants("image")
                        .Select(x => x.Element("url")?.Value)
                        .First(),
                    Link = n.Element("link")?.Value,
                })
                .Take(4)
                .AsQueryable();

            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}