namespace islandmonkeyuk.Misc;

using Newtonsoft.Json;

public class VirusScanAnalysisResponse {

    public class Attributes
    {
        public int date { get; set; }
        public string status { get; set; }
        public Stats stats { get; set; }
        public Results results { get; set; }
    }

    public class Data
    {
        public Attributes attributes { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public Links links { get; set; }
    }

    public class FileInfo
    {
        public string sha256 { get; set; }
        public string sha1 { get; set; }
        public string md5 { get; set; }
        public int size { get; set; }
    }

    public class Links
    {
        public string item { get; set; }
        public string self { get; set; }
    }

    public class Meta
    {
        public FileInfo file_info { get; set; }
    }

    public class Results
    {
    }

    public class Root
    {
        public Meta meta { get; set; }
        public Data data { get; set; }
    }

    public class Stats
    {
        public int harmless { get; set; }

        [JsonProperty("type-unsupported")]
        public int typeunsupported { get; set; }
        public int suspicious { get; set; }

        [JsonProperty("confirmed-timeout")]
        public int confirmedtimeout { get; set; }
        public int timeout { get; set; }
        public int failure { get; set; }
        public int malicious { get; set; }
        public int undetected { get; set; }
    }

}
