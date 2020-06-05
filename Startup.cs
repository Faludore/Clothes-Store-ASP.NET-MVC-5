using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreIdent.Startup))]
namespace StoreIdent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
