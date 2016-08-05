using MvcMef.Dependencies;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcMef.Web
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ControllerExportAttribute : ExportAttribute
    {
        public ControllerExportAttribute(Type concreteType)
            : base(concreteType.FullName, typeof(IMefController))
        {
        }
    }
}
