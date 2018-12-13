using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mission_Site.Startup))]
namespace Mission_Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
