using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PolicyGAPWeb.Startup))]
namespace PolicyGAPWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
