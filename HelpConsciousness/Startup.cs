using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpConsciousness.Startup))]
namespace HelpConsciousness
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
