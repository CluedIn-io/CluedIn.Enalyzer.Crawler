using System.IO;
using System.Reflection;
using CluedIn.Crawling.Enalyzer.Core;
using CrawlerIntegrationTesting.Clues;
using CrawlerIntegrationTesting.Log;
using Xunit.Abstractions;
using DebugCrawlerHost = CrawlerIntegrationTesting.CrawlerHost.DebugCrawlerHost<CluedIn.Crawling.Enalyzer.Core.EnalyzerCrawlJobData>;

namespace CluedIn.Crawling.Enalyzer.Integration.Test
{
    public class EnalyzerTestFixture
    {
        public ClueStorage ClueStorage { get; }
        private readonly DebugCrawlerHost debugCrawlerHost;

        public TestLogger Log { get; }
        public EnalyzerTestFixture()
        {
            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Substring(8)).DirectoryName;
            debugCrawlerHost = new DebugCrawlerHost(executingFolder, EnalyzerConstants.ProviderName);

            ClueStorage = new ClueStorage();

            Log = debugCrawlerHost.AppContext.Container.Resolve<TestLogger>();

            debugCrawlerHost.ProcessClue += ClueStorage.AddClue;

            debugCrawlerHost.Execute(EnalyzerConfiguration.Create(), EnalyzerConstants.ProviderId);
        }

        public void PrintClues(ITestOutputHelper output)
        {
            foreach(var clue in ClueStorage.Clues)
            {
                output.WriteLine(clue.OriginEntityCode.ToString());
            }
        }

        public void PrintLogs(ITestOutputHelper output)
        {
            output.WriteLine(Log.PrintLogs());
        }

        public void Dispose()
        {
        }

    }
}


