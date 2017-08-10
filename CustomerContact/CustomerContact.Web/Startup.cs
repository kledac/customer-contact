using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerContact.Web.Startup))]
namespace CustomerContact.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
