using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.Enalyzer;
using CluedIn.Crawling.Enalyzer.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Enalyzer.Unit.Test
{
    public class EnalyzerCrawlerBehaviour
    {
        private readonly ICrawlerDataGenerator _sut;

        public EnalyzerCrawlerBehaviour()
        {
            var nameClientFactory = new Mock<IEnalyzerClientFactory>();

            _sut = new EnalyzerCrawler(nameClientFactory.Object);
        }

        [Fact]
        public void GetDataReturnsData()
        {
            var jobData = new CrawlJobData();

            _sut.GetData(jobData)
                .ShouldNotBeNull();
        }
    }
}
