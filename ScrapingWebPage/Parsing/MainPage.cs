using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Parsing
{
    //This class parse main page to get all brands whith links to all brands model
    public static partial class HtmlParsesExtensions
    {
        public static IEnumerable<LinksModel> ParseMainPage(this HtmlParser htmlParser, string htmlContent)
        {
            return htmlParser.ParseDocument(htmlContent )
                                    .GetElementsByClassName("CatalogGroup")
                                    .Take(2)
                                    .Select(x => x.InnerHtml)
                                    .ToList()
                                    .SelectMany(group => GetBrandsList(group, htmlParser))
                                    .ToList();
        }

        public static IEnumerable<LinksModel> GetBrandsList(string elements, HtmlParser parser)
        {
            return parser.ParseDocument(elements)
                              .QuerySelectorAll("a")
                              .Select(links => new LinksModel
                              {
                                  Link = links.GetAttribute("href")
                              });
        }
    }
}
