using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HungVdn1670.Startup))]
namespace HungVdn1670
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
