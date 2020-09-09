using Castle.MicroKernel.Registration;

using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Core.Server;
// 
using CluedIn.Crawling.Enalyzer.Core;
using CluedIn.Crawling.Enalyzer.Infrastructure.Installers;
// 
using CluedIn.Server;
using ComponentHost;

namespace CluedIn.Provider.Enalyzer
{
    [Component(EnalyzerConstants.ProviderName, "Providers", ComponentType.Service, ServerComponents.ProviderWebApi, Components.Server, Components.DataStores, Isolation = ComponentIsolation.NotIsolated)]
    public sealed class EnalyzerProviderComponent : ServiceApplicationComponent<IBusServer>
    {
        public EnalyzerProviderComponent(ComponentInfo componentInfo)
            : base(componentInfo)
        {
            // Dev. Note: Potential for compiler warning here ... CA2214: Do not call overridable methods in constructors
            //   this class has been sealed to prevent the CA2214 waring being raised by the compiler
            Container.Register(Component.For<EnalyzerProviderComponent>().Instance(this));  
        }

        public override void Start()
        {
            Container.Install(new InstallComponents());

            Container.Register(Types.FromAssembly(System.Reflection.Assembly.GetExecutingAssembly()).BasedOn<IProvider>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromAssembly(System.Reflection.Assembly.GetExecutingAssembly()).BasedOn<IEntityActionBuilder>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());



            State = ServiceState.Started;
        }

        public override void Stop()
        {
            if (State == ServiceState.Stopped)
                return;

            State = ServiceState.Stopped;
        }
    }
}
