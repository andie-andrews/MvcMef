namespace MvcMef.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Dependencies;


    [Export(typeof(IMappingProvider))]
    [Export(typeof(MappingProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MappingProvider : IMappingProvider
    {
        /// <summary>
        /// Gets or sets the available mappings.
        /// </summary>
        [ImportMany(typeof(IMap))]
        public IEnumerable<IMap> AvailableMappings { get; private set; }

        public IMap ProvideProcessor(Type source, Type target)
        {
            if (AvailableMappings == null || !AvailableMappings.Any())
            {
                throw new Exception("No mappings available");
            }

            return AvailableMappings.First(processor => processor.CanMap(source, target));
        }

        public IMap ProvideProcessorBySource(Type source)
        {
            if (AvailableMappings == null || !AvailableMappings.Any())
            {
                throw new Exception("No mappings available");
            }

            return AvailableMappings.First(processor => processor.CanMapToTarget(source));
        }

    }
}
