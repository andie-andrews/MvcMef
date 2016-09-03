using MvcMef.Dependencies;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;

namespace MvcMef.Web
{
    public class MefControllerFactory : DefaultControllerFactory
    {
        private  CompositionContainer _container;

        public MefControllerFactory(CompositionContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController result = null;


            if (!MefBootstrap.IsIntialized)
            {
                MefBootstrap.Intialize();
                ControllerBuilder.Current.SetControllerFactory(new MefControllerFactory(MefBootstrap.Container));
            }

            var export = _container.GetExports(typeof(IMefController), null, controllerType.FullName).SingleOrDefault(x => x.Value.GetType() == controllerType);

            if (null != export)
            {
                result = export.Value as IController;
            }

            if (null != result)
            {
                _container.SatisfyImportsOnce(result);
            }

            return result;
        }
    }
}
