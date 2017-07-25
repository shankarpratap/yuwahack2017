using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YuwaWebApp.Startup))]
namespace YuwaWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
