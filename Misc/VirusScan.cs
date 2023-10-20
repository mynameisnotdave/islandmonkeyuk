using System.Web;
using Newtonsoft.Json;
using RestSharp;

namespace islandmonkeyuk.Misc;

public class VirusScan
{
    private readonly IConfiguration config;
    //private VirusScanAnalysisResponse.Root deserializedAnalysisResponse;
    private VirusScanUploadResponse.Root deserializedUploadResponse;
    private string hash = string.Empty;
    private string virusScanApiKey = string.Empty;

    private async Task RunVirusScanner(string path)
    {
        RestClientOptions options = new RestClientOptions("https://www.virustotal.com/api/v3/files");
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("")
        {
            AlwaysMultipartFormData = true
        };
        VirusScanSettings? virusScanConfig = config.GetSection("VirusScan").Get<VirusScanSettings>();
        virusScanApiKey = virusScanConfig.VirusScanApiKey;
        request.AddHeader("accept", "application/json");
        request.AddHeader("x-apikey", virusScanApiKey);
        request.FormBoundary = "---011000010111000001101001";
        request.AddFile("file", path);
        RestResponse response = await client.PostAsync(request);
        deserializedUploadResponse = JsonConvert.DeserializeObject<VirusScanUploadResponse.Root>(response.ToString());
    }
    private async Task<bool> GetAnalysis()
    {
        string hash = deserializedUploadResponse.data.id;
        bool isMalicious = true;
        RestClientOptions options = new RestClientOptions($"https://www.virustotal.com/api/v3/analyses/{HttpUtility.UrlEncode(hash)}");
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("x-apikey", virusScanApiKey);
        RestResponse? response = await client.GetAsync(request);
        VirusScanAnalysisResponse.Root? deserializedAnalysisResponse = JsonConvert.DeserializeObject<VirusScanAnalysisResponse.Root>(response.ToString());
        if (!deserializedAnalysisResponse.data.attributes.status.Equals("completed"))
        {
            response = await client.GetAsync(request);// do the request again if it is not completed
        }
        else
        {
            if (deserializedAnalysisResponse.data.attributes.stats.malicious == 0)
            {
                return !isMalicious;
            }
        }
        return isMalicious;
    }
}