using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Parsing
{
    public static partial class HtmlParsesExtensions
    {
        public static IEnumerable<LinksModel> ParseRegionPage(this HtmlParser htmlParser, string htmlContent, string urlMain)
        {
            return htmlParser.ParseDocument(htmlContent)
                                    .QuerySelectorAll("div.name")
                                    .Select(x => new LinksModel
                                    {
                                        Link = urlMain + x.QuerySelector("a").GetAttribute("href")
                                    })
                                    .ToList();
        }
    }
}
