using System.Net.Http;
using System.Threading.Tasks;

namespace ScrapingWebPage
{
    public class HtmlClient
    {
        private HttpClient _httpClient;

        public HtmlClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Get HTML content from the web page by URL and return as string
        public Task<string> GetHtmlContent(string url) => _httpClient.GetStringAsync(url);
    }
}