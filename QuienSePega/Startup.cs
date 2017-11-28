using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuienSePega.Startup))]
namespace QuienSePega
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
