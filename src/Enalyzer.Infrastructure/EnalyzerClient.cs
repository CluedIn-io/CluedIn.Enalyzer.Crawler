using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CluedIn.Core;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Enalyzer.Core;
using CluedIn.Crawling.Enalyzer.Core.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CluedIn.Crawling.Enalyzer.Infrastructure
{
    // TODO - This class should act as a client to retrieve the data to be crawled.
    // It should provide the appropriate methods to get the data
    // according to the type of data source (e.g. for AD, GetUsers, GetRoles, etc.)
    // It can receive a IRestClient as a dependency to talk to a RestAPI endpoint.
    // This class should not contain crawling logic (i.e. in which order things are retrieved)
    public class EnalyzerClient
    {
        private const string BaseUri = "http://sample.com";

        private readonly ILogger<EnalyzerClient> log;

        private readonly IRestClient client;

        public EnalyzerClient(ILogger<EnalyzerClient> log, EnalyzerCrawlJobData enalyzerCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
        {
            if (enalyzerCrawlJobData == null)
            {
                throw new ArgumentNullException(nameof(enalyzerCrawlJobData));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.client = client ?? throw new ArgumentNullException(nameof(client));

            // TODO use info from enalyzerCrawlJobData to instantiate the connection
            client.BaseUrl = new Uri(BaseUri);
            client.AddDefaultParameter("api_key", enalyzerCrawlJobData.ApiKey, ParameterType.QueryString);
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var request = new RestRequest(url, Method.GET);

            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var diagnosticMessage = $"Request to {client.BaseUrl}{url} failed, response {response.ErrorMessage} ({response.StatusCode})";
                log.LogError(diagnosticMessage);
                throw new InvalidOperationException($"Communication to jsonplaceholder unavailable. {diagnosticMessage}");
            }

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation("", "");
        }

        public IEnumerable<Project> Get(string accessKey, string apiSecret)
        {
            //Gets current unix time
            var url = CreateUrl(accessKey, apiSecret);
            using (HttpClient httpClient = new HttpClient())
            {

                var response = httpClient.GetAsync(url).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    log.LogError("401 Unauthorized. Check credentials");
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    log.LogError(response.StatusCode.ToString() + " Failed to get data");
                }
                var results = JsonConvert.DeserializeObject<Projects>(responseContent);
                foreach (var item in results.projects)
                {
                    yield return item;
                }
            }
        }

        private static string CreateUrl(string accessKey, string apiSecret)
        {
            var expires = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var api = string.Format("https://api.enalyzer.com/projects?AccessKey={0}&Expires={1}", accessKey, expires);
            var apiBytes = System.Text.Encoding.UTF8.GetBytes(apiSecret);
            apiBytes.AddRange(System.Text.Encoding.UTF8.GetBytes(api));
            var signature = System.Convert.ToBase64String(System.Security.Cryptography.HMACMD5.Create().ComputeHash(apiBytes));
            var url = string.Format(api, accessKey, expires);
            url = string.Format("{0}&Signature={1}", url, signature);
            url = HttpUtility.UrlEncode(url);
            return url;
        }
    }
}
