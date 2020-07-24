using CluedIn.Core;
using CluedIn.Crawling.Enalyzer.Core;

using ComponentHost;

namespace CluedIn.Crawling.Enalyzer
{
    [Component(EnalyzerConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class EnalyzerCrawlerComponent : CrawlerComponentBase
    {
        public EnalyzerCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

