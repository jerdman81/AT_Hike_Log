using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HikeLog.WebMVC.Startup))]
namespace HikeLog.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
