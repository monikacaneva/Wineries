using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DIANS_2.Startup))]
namespace DIANS_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
