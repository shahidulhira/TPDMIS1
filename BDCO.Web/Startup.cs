using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BDCO.Web.Startup))]
namespace BDCO.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
