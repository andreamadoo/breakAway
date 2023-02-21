using System.Configuration;
using System.Web.Mvc;
using BreakAway.Entities;
using BreakAway.Services;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BreakAway.Installers
{
    public class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<IController>()
                                      .LifestyleTransient());

            container.Register(Classes.FromAssemblyContaining<IFilter>()
                                          .BasedOn<IFilter>()
                                          .WithServiceAllInterfaces()
                                          .LifestyleSingleton());


            container.Register(Component.For<IRepository>().ImplementedBy<SqlRepository>().LifeStyle.Transient);

            container.Register(Component.For<IFilterService>().ImplementedBy<FilterService>().LifeStyle.Singleton);



            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            container.Register(Component.For<IBreakAwayContext>().UsingFactoryMethod(() => new BreakAwayContext(connectionString)).LifeStyle.Transient);
        }
    }
}