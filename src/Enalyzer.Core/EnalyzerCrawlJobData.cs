using CluedIn.Core.Crawling;

namespace CluedIn.Crawling.Enalyzer.Core
{
    public class EnalyzerCrawlJobData : CrawlJobData
    {
        public string ApiKey { get; set; }
        public string AccessKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
