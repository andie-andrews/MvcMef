using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMef.Dependencies
{
    public interface IMappingProvider
    {
        /// <summary>
        /// Gets or sets the available mappings.
        /// </summary>
        IEnumerable<IMap> AvailableMappings { get; }

        IMap ProvideProcessor(Type source, Type target);
        IMap ProvideProcessorBySource(Type source);
    }
}