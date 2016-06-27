using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;

namespace Netchex.Web
{
    public class MefControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _container;

        public MefControllerFactory(CompositionContainer container)
        {
            this._container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController result = null;

            var export = this._container.GetExports(typeof(IController), null, controllerType.FullName).SingleOrDefault(x => x.Value.GetType() == controllerType);

            if (null != export)
            {
                result = export.Value as IController;
            }

            if (null != result)
            {
                this._container.SatisfyImportsOnce(result);
            }

            return result;
        }
    }
}
