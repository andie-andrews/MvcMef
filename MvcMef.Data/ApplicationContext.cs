using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using MvcMef.Dependencies;

namespace MvcMef.Data
{
    [Export(typeof(IApplicationContext))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public partial class MvcMefEntities : DbContext, IApplicationContext
    {
        
    }
}
