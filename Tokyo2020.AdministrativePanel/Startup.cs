using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tokyo2020.AdministrativePanel.Startup))]
namespace Tokyo2020.AdministrativePanel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
