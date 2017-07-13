using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(yuwabackendappService.Startup))]

namespace yuwabackendappService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}