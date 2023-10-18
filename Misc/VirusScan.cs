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
        var options = new RestClientOptions("https://www.virustotal.com/api/v3/files");
        var client = new RestClient(options);
        var request = new RestRequest("")
        {
            AlwaysMultipartFormData = true
        };
        var virusScanConfig = config.GetSection("VirusScan").Get<VirusScanSettings>();
        virusScanApiKey = virusScanConfig.VirusScanApiKey;
        request.AddHeader("accept", "application/json");
        request.AddHeader("x-apikey", virusScanApiKey);
        request.FormBoundary = "---011000010111000001101001";
        request.AddFile("file", path);
        var response = await client.PostAsync(request);
        deserializedUploadResponse = JsonConvert.DeserializeObject<VirusScanUploadResponse.Root>(response.ToString());
    }
    private async Task<bool> GetAnalysis()
    {
        var hash = deserializedUploadResponse.data.id;
        var isMalicious = true;        
        var options = new RestClientOptions($"https://www.virustotal.com/api/v3/analyses/{HttpUtility.UrlEncode(hash)}");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("x-apikey", virusScanApiKey);
        var response = await client.GetAsync(request);
        var deserializedAnalysisResponse = JsonConvert.DeserializeObject<VirusScanUploadResponse.Root>(response.ToString());
        return isMalicious;

        
    }
}