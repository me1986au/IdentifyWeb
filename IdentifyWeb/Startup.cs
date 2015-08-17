using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentifyWeb.Startup))]
namespace IdentifyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
