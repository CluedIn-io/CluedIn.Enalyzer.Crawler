using CluedIn.Crawling.Enalyzer.Core;

namespace CluedIn.Crawling.Enalyzer.Infrastructure.Factories
{
    public interface IEnalyzerClientFactory
    {
        EnalyzerClient CreateNew(EnalyzerCrawlJobData enalyzerCrawlJobData);
    }
}
