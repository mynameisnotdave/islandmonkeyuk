namespace islandmonkeyuk.Misc;

public class VirusScanAnalysisResponse {

    public class Data {
        public string type { get; set; }
        public string id { get; set; }
        public Links links { get; set; }
    }

    public class Links {
        public string self { get; set; }
    }

    public class Root {
        public Data data { get; set; }
    }
}
