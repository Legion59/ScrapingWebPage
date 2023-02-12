using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Parsing
{
    //Here we makin the same things that on the Group details page
    public static partial class HtmlParsesExtensions
    {
        public static IEnumerable<SubgroupDetailsModel> ParseSubgroupPage(this HtmlParser htmlParser, string htmlContent, string urlMain)
        {
            return htmlParser.ParseDocument(htmlContent)
                                .QuerySelectorAll("div.name")
                                .Select(details => new SubgroupDetailsModel
                                {
                                    Name = details.TextContent,
                                    DetailLink = urlMain + details.QuerySelector("a").GetAttribute("href")
                                })
                                .ToList();
        }
    }
}
