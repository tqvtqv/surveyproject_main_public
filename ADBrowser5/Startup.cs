using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ADBrowser5.Startup))]
namespace ADBrowser5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
