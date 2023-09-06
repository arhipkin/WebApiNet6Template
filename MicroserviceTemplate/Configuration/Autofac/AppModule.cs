using Autofac;
using MicroserviceTemplate.Controllers;
using Microsoft.Extensions.Options;

namespace MicroserviceTemplate.Configuration.Autofac
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(ConfigureAppOptions<>)).As(typeof(IConfigureOptions<>)).SingleInstance();
            //builder.RegisterType<TestService>().As<ITestService>().InstancePerRequest();
        }
    }
}
