using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rrhhGestion.Startup))]
namespace rrhhGestion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
