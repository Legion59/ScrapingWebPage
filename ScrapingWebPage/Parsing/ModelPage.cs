using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Parsing
{
    //Here we collect all need data from the page whith cars models by the brand and region
    public static partial class HtmlParsesExtensions
    {
        //This method take all content from the page, URL link to main page and forming List of needs data
        public static IEnumerable<CarPageModel> ParseModelPage(this HtmlParser htmlParser, string htmlContent, string urlMain)
        {
            //Here we take from the page only cells that contains
            //Car name, car model code, production dates, configurations code
            //Also we need save link to page with model configurations
            return htmlParser.ParseDocument(htmlContent)
                        .All
                        .Where(m => m.LocalName == "div" && m.ClassList.Contains("List") && m.Children.HasClass("Header"))
                        .SelectMany(group => GetModelInformation(group, htmlParser, group.QuerySelector("div.name").TextContent, urlMain))
                        .ToList();
        }

        public static List<CarPageModel> GetModelInformation(IElement infoHtml, HtmlParser htmlParser,  string name, string urlMain)
        {
            //Here we forming List with information about every model on the page
            return htmlParser.ParseDocument(infoHtml.InnerHtml)
                                   .All
                                   .Where(m => m.ClassList.Contains("List") && m.Children.HasClass("id"))
                                   .Select(info => new CarPageModel
                                   {
                                       CarId = info.QuerySelector("div.id").TextContent,
                                       Name = name,
                                       DateRange = info.QuerySelector("div.dateRange").TextContent,
                                       ModelCode = info.QuerySelector("div.modelCode").TextContent,
                                       ConfigurationLink = urlMain + "/toyota/?function=getComplectations&market=EU&model=" + info.QuerySelector("div.id").TextContent
                                   })
                                   .ToList();
        }
    }
}