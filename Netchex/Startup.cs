using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Mvc;
using Netchex.Web;
using Netchex.Dependencies;

[assembly: OwinStartupAttribute(typeof(Netchex.Startup))]
namespace Netchex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            MefBootstrap.Register();

            ControllerBuilder.Current.SetControllerFactory(new MefControllerFactory(MefBootstrap.Container));
        }
    }
}
