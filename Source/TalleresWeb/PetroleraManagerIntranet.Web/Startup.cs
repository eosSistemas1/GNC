using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetroleraManagerIntranet.Web.Startup))]
namespace PetroleraManagerIntranet.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            //ConfigureAuth(app);
        }
    }
}
