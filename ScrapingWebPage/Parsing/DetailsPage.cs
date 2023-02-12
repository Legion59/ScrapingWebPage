using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Parsing
{
    //Here we collect information about details 
    public static partial class HtmlParsesExtensions
    {
        public static IEnumerable<DetailModel> ParseDetailsPage(this HtmlParser htmlClient, string htmlContent)
        {
            List<DetailModel> allDetails = new List<DetailModel>();

            HashSet<string> uniqClasses = htmlClient.ParseDocument(htmlContent)
                                        .QuerySelectorAll("tbody")
                                        .FirstOrDefault()
                                        .QuerySelectorAll("tr")
                                        .Skip(1)
                                        .Select(x => x.ClassName)
                                        .ToHashSet<string>();

            foreach (var uniqClass in uniqClasses)
            {
                var detailInfoEnum = htmlClient.ParseDocument(htmlContent)
                                                    .All
                                                    .Where(x => x.LocalName == "tr" && x.ClassName == uniqClass);

                foreach (var item in detailInfoEnum.Skip(1))
                {
                    DetailModel detailsModel = new DetailModel();

                    if (item.QuerySelector("div.replaceNumber") != null)
                    {
                        detailsModel.Code = item.QuerySelector("div.replaceNumber").QuerySelector("a").TextContent;
                    }
                    else
                    {
                        detailsModel.Code = item.QuerySelector("div.number").TextContent;
                    }

                    detailsModel.TreeCode = (detailInfoEnum.FirstOrDefault().TextContent).Split('\U000000A0').FirstOrDefault();
                    detailsModel.TreeName = (detailInfoEnum.FirstOrDefault().TextContent).Remove(0, detailsModel.TreeCode.Length).Trim();

                    detailsModel.Count = Convert.ToInt32(item.QuerySelector("div.count").TextContent);
                    detailsModel.DateRange = item.QuerySelector("div.dateRange").TextContent;
                    detailsModel.Info = item.QuerySelector("div.usage").TextContent;

                    allDetails.Add(detailsModel);
                }
            }

            return allDetails;
        }
    }
}
