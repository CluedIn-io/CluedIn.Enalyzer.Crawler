using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.Enalyzer.Core;
using CluedIn.Crawling.Enalyzer.Infrastructure.Factories;

namespace CluedIn.Crawling.Enalyzer
{
    public class EnalyzerCrawler : ICrawlerDataGenerator
    {
        private readonly IEnalyzerClientFactory clientFactory;
        public EnalyzerCrawler(IEnalyzerClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is EnalyzerCrawlJobData enalyzercrawlJobData))
            {
                yield break;
            }

            var client = clientFactory.CreateNew(enalyzercrawlJobData);

            //retrieve data from provider and yield objects
            
        }       
    }
}
