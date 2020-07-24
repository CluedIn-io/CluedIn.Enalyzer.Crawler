using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Enalyzer.Infrastructure.Factories;
using Moq;

namespace CluedIn.Provider.Enalyzer.Unit.Test.EnalyzerProvider
{
    public abstract class EnalyzerProviderTest
    {
        protected readonly ProviderBase Sut;

        protected Mock<IEnalyzerClientFactory> NameClientFactory;
        protected Mock<IWindsorContainer> Container;

        protected EnalyzerProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            NameClientFactory = new Mock<IEnalyzerClientFactory>();
            var applicationContext = new ApplicationContext(Container.Object);
            Sut = new Enalyzer.EnalyzerProvider(applicationContext, NameClientFactory.Object);
        }
    }
}
