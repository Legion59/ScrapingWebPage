using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ScrapingWebPage.Database;
using ScrapingWebPage.Models;
using ScrapingWebPage.Parsing;

namespace ScrapingWebPage
{
    public class Program
    {
        static async Task Main()
        {
            var htmlClient = new HtmlClient(new HttpClient());
            var htmlParser = new HtmlParser();
            await using var carsDbContext = new CarsDbContext();


            var urlMain = "https://www.ilcats.ru";
            var urlModel = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";


            //Because of I don't want save a lot of information, I choose to safe only One car model
            //Same things to configurations, groups of details, subgroups ext.
            //I save only a few things

            //Save information about models cars
            //In this case about only Two cars
            var allModels = htmlParser.ParseModelPage(await htmlClient.GetHtmlContent(urlModel), urlMain).Take(2).ToList();


            //Save information about car configurations
            List<CarConfigurationModel> allConfigurations = new List<CarConfigurationModel>();

            foreach (var model in allModels)
            {
                var configurations = htmlParser.ParseConfigurationPage(await htmlClient.GetHtmlContent(model.ConfigurationLink), urlMain).Take(3).ToList();

                //Connect car configurations with car model
                configurations.ForEach(configuration => configuration.CarModel = model);

                //Connect car model with configurations
                model.CarConfigurations = configurations;

                allConfigurations.AddRange(configurations);
            }


            //Save information about groups of details
            var allGroupDetails = new List<GroupDetailsModel>();

            foreach (var configuration in allConfigurations)
            {
                var groupDetails = htmlParser.ParseGropDetailsPage(await htmlClient.GetHtmlContent(configuration.GroupDetailsLink), urlMain).Take(2).ToList();

                //Connect configurations whith groups of details
                configuration.GroupDetails = groupDetails;

                //Connect group of details with car configuration
                groupDetails.ForEach(group => group.CarConfiguration = configuration);

                allGroupDetails.AddRange(groupDetails);
            }


            //Save information about subgroups of details
            var allSubgroupsDetails = new List<SubgroupDetailsModel>();

            foreach (var groups in allGroupDetails)
            {
                var subgroupDetails = htmlParser.ParseSubgroupPage(await htmlClient.GetHtmlContent(groups.SubgroupLink), urlMain).Take(3).ToList();

                //Connect groups of details whith the subgroups 
                groups.Subgroups = subgroupDetails;

                subgroupDetails.ForEach(subgroup => subgroup.GroupDetail = groups);

                allSubgroupsDetails.AddRange(subgroupDetails);
            }


            //Save information about details
            var allDetails = new List<DetailModel>();

            foreach (var subgroup in allSubgroupsDetails)
            {
                var details = htmlParser.ParseDetailsPage(await htmlClient.GetHtmlContent(subgroup.DetailLink)).ToList();

                //Connect subgroups of details whith the details
                subgroup.Details = details;

                details.ForEach(detail => detail.SubGroup = subgroup);

                allDetails.AddRange(details);
            }

            //Add information to the Data base
            await carsDbContext.AddRangeAsync(allModels);
            await carsDbContext.AddRangeAsync(allConfigurations);
            await carsDbContext.AddRangeAsync(allGroupDetails);
            await carsDbContext.AddRangeAsync(allSubgroupsDetails);
            await carsDbContext.AddRangeAsync(allDetails);

            //Save changes in Data base
            await carsDbContext.SaveChangesAsync();
        }
    }
}