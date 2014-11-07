using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveSDKDemo.Startup))]
namespace LiveSDKDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
