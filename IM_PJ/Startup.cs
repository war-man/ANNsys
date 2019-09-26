using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IM_PJ.Startup))]
namespace IM_PJ
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureCron();
            ConfigureAuth(app);
        }
    }
}
