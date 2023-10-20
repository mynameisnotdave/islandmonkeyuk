using System.Web;
using Newtonsoft.Json;
using RestSharp;

namespace islandmonkeyuk.Misc;

public class VirusScan
{
    //private VirusScanAnalysisResponse.Root deserializedAnalysisResponse;
    private VirusScanUploadResponse.Root deserializedUploadResponse;
    private string hash = string.Empty;
    private string virusScanApiKey = string.Empty;

    public async Task RunVirusScanner(string path)
    {
        RestClientOptions options = new RestClientOptions("https://www.virustotal.com/api/v3/files");
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("")
        {
            AlwaysMultipartFormData = true
        };
        var builder = new ConfigurationBuilder().AddUserSecrets<VirusScanSettings>();
        IConfiguration config = builder.Build();
        virusScanApiKey = config["VirusScan:ApiKey"];
        request.AddHeader("accept", "application/json");
        request.AddHeader("x-apikey", virusScanApiKey);
        request.FormBoundary = "---011000010111000001101001";
        request.AddFile("file", path);
        RestResponse response = await client.PostAsync(request);
        deserializedUploadResponse = JsonConvert.DeserializeObject<VirusScanUploadResponse.Root>(response.ToString());
    }
    public async Task<bool> IsMalicious()
    {
        hash = deserializedUploadResponse.data.id;
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
            if (deserializedAnalysisResponse.data.attributes.stats.malicious != 0)
            {
                return true;
            }
        }
        return false;
    }
}