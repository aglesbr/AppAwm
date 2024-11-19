using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppDocManager.Services
{
    public static class ServiceAwm
    {

        public static Task<HttpResponseMessage> Get(string httpPath)
        {
            string originPath = GetUri();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{originPath}/{httpPath}");
            var response = Task.Run(async () => await client.SendAsync(request));

            return response;
        }

        public static Task<HttpResponseMessage> Put(string httpPath, MultipartFormDataContent content = null)
        {
            string originPath = GetUri();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"{originPath}/{httpPath}");
            
            if (content != null)
                request.Content = content;

            var response = Task.Run(async () => await client.SendAsync(request));

            return response;
        }

        public static Task<HttpResponseMessage> Post(string httpPath, MultipartFormDataContent content)
        {
            string originPath = GetUri();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{originPath}/{httpPath}");
            request.Content = content;
            var response = Task.Run(async () => await client.SendAsync(request));

            return response;
        }

        public static string GetUri()
        {
            string settingValue = ConfigurationManager.AppSettings["apiUrl"];
            return settingValue;
        }
    }
}
