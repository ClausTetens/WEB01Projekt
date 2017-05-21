using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Voresjazzklub.Startup))]
namespace Voresjazzklub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
