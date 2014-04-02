using System.Web.Http;
using Owin;

namespace AADLab.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

            appBuilder.UseWebApi(config);
        }
    }
}