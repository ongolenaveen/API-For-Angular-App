using Autofac;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using SwordTestApi.Contracts;
using SwordTestApi.Services;

namespace SwordTestApi.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<RiskDatasource>().As<IRiskDataSource>();
            builder.RegisterType<RiskRepository>().As<IRiskRepository>();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}