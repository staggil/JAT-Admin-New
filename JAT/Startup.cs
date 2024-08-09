using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JAT.Startup))]
namespace JAT
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
