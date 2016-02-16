using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(AvalancheAllerts.Web.Startup))]

namespace AvalancheAllerts.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
