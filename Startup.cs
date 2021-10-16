using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrackerSystem.Startup))]
namespace TrackerSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
