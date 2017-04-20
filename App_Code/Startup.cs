using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Merge.Startup))]
namespace Merge
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
