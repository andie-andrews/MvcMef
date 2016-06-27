using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Netchex.Dependencies;

namespace Netchex.Data
{
    [Export(typeof(IApplicationContext))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public partial class NetchexEntities : DbContext, IApplicationContext
    {
        
    }
}
