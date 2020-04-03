using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zadatak.Startup))]
namespace Zadatak
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
