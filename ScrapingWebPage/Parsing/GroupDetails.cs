using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Parsing
{
    //This class save information about groups of details to specific configuration
    //Method to parse page whith details subgroups same
    public static partial class HtmlParsesExtensions
    {
        public static IEnumerable<GroupDetailsModel> ParseGropDetailsPage(this HtmlParser htmlClient, string htmlContent, string urlMain)
        {
            //We save only name of group ang link to the subgroup page
            return htmlClient.ParseDocument(htmlContent)
                                    .QuerySelectorAll("div.List")
                                    .Skip(1)
                                    .Select(x => new GroupDetailsModel
                                    {
                                        Name = x.QuerySelector("div.name").TextContent,
                                        SubgroupLink = urlMain + x.QuerySelector("a").GetAttribute("href")
                                    })
                                    .ToList();
        }
    }
}