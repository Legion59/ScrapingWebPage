using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Parsing
{
    //This class collect information about model configurations
    public static partial class HtmlParsesExtensions
    {
        public static IEnumerable<CarConfigurationModel> ParseConfigurationPage(this HtmlParser htmlParser, string htmlContent, string urlMain)
        {
            //Here we split table on the lines and write information from the cells
            //Configuration name, dates and list with specifications
            //Also we need save link to page with details groups
            return htmlParser.ParseDocument(htmlContent)
                                        .QuerySelectorAll("table")
                                        .FirstOrDefault()
                                        .QuerySelectorAll("tr")
                                        .Skip(1)
                                        .Select(group => GetCarConfigurationSpecs(group, urlMain))
                                        .ToList();
        }

        //Here we form information into Model
        public static CarConfigurationModel GetCarConfigurationSpecs(IElement element, string urlMain)
        {
            CarConfigurationModel carConfiguration= new CarConfigurationModel();

            //Save configuration name
            carConfiguration.ConfigurationName = element.QuerySelector("a").TextContent;

            //Save production dates
            carConfiguration.DateRange = element.QuerySelector("div.dateRange").TextContent;

            //Save link to details group page
            carConfiguration.GroupDetailsLink = urlMain + element.QuerySelector("a").GetAttribute("href");

            //Forming a list with specifications about configuration
            for (int i = 1; i <= element.QuerySelectorAll("td").Length - 2; i++)
            {
                if (element.QuerySelector($@"div.\30 {i}") != null)
                {
                    carConfiguration.Specs
                        .Add(new CarConfigurationSpecModel
                        {
                            SpecValue = element.QuerySelector($@"div.\30 {i}").TextContent
                        });
                }
            }
            return carConfiguration;
        }
    }
}
