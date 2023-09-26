using System.Net;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Headers;
using System.Web;

namespace islandmonkeyuk.Misc
{
    public class VirusScan
    {
        private readonly IConfiguration _config;
        private string virusScanApiKey = string.Empty;
        private string hash = string.Empty;
        private VirusScanUploadResponse.Root deserializedUploadResponse;
        private VirusScanAnalysisResponse.Root deserializedAnalysisResponse;

        public VirusScan(IConfiguration config)
        {
            _config = config;
        }
        private async Task RunVirusScanner(string path)
        {
            var options = new RestClientOptions("https://www.virustotal.com/api/v3/files");
            var client = new RestClient(options);
            var request = new RestRequest("")
            {
                AlwaysMultipartFormData = true
            };
            var virusScanConfig = _config.GetSection("VirusScan").Get<VirusScanSettings>();
            virusScanApiKey = virusScanConfig.VirusScanApiKey;
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", virusScanApiKey);
            request.FormBoundary = "---011000010111000001101001";
            request.AddFile("file", path);
            var response = await client.PostAsync(request);
            deserializedUploadResponse = JsonConvert.DeserializeObject<VirusScanUploadResponse.Root>(response.ToString());
        }
        /* Yuck, we have a problem on the VirusTotal website trying to get what the API response would
         look like. */
        private async Task<bool> GetAnalysis()
        {
            var hash = deserializedUploadResponse.data.id;
            var isMalicious = true;
            var options = new RestClientOptions("https://www.virustotal.com/api/v3/files/" + HttpUtility.UrlEncode(hash));
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", virusScanApiKey);
            var response = await client.GetAsync(request);
            return isMalicious; 
        }
    }
}
