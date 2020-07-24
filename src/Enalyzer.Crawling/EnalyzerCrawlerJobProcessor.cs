using CluedIn.Crawling.Enalyzer.Core;

namespace CluedIn.Crawling.Enalyzer
{
    public class EnalyzerCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<EnalyzerCrawlJobData>
    {
        public EnalyzerCrawlerJobProcessor(EnalyzerCrawlerComponent component) : base(component)
        {
        }
    }
}
