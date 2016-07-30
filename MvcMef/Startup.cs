using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Mvc;
using MvcMef.Web;
using MvcMef.Dependencies;

[assembly: OwinStartupAttribute(typeof(MvcMef.Startup))]
namespace MvcMef
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
