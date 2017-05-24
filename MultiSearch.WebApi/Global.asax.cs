using log4net;
using MultiSearch.SearchingCore.Engines;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace MultiSearch.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var repositoryAssembly = typeof(EngineLoader).Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace.Contains("MultiSearch") || type.Namespace.Contains("SearchFight")
                where type.GetInterfaces().Any()
                select new { Service = type.GetInterfaces().Single(), Implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);
            }
            container.RegisterSingleton(LogManager.GetLogger(typeof(object)));


            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            GlobalConfiguration.Configuration.EnsureInitialized();
            


            
        }
    }
}
