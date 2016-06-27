namespace Netchex.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Netchex.Dependencies;

    [Export(typeof(MappingProvider))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class MappingProvider
    {
        /// <summary>
        /// Gets or sets the available mappings.
        /// </summary>
        [ImportMany(typeof(IMap))]
        public IEnumerable<IMap> AvailableMappings { get; private set; }

        public IMap ProvideProcessor(Type source, Type target)
        {
            if (this.AvailableMappings == null || !this.AvailableMappings.Any())
            {
                throw new Exception("No mappings available");
            }

            return this.AvailableMappings.First(processor => processor.CanMap(source, target));
        }

        public IMap ProvideProcessorBySource(Type source)
        {
            if (this.AvailableMappings == null || !this.AvailableMappings.Any())
            {
                throw new Exception("No mappings available");
            }

            return this.AvailableMappings.First(processor => processor.CanMapToTarget(source));
        }

    }
}
