using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using Netchex.Dependencies;
using Netchex.Dto;

namespace Netchex.Data.Mapping
{

    [Export(typeof(IMap))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class FrequencyMapping : IMapping<Frequency, Dto.LookupReference>
    {
        public bool CanMap(Type sourceType, Type targetType)
        {
            return this.CanMapFromSource(sourceType) && this.CanMapToTarget(targetType);
        }

        public bool CanMapToTarget(Type targetType)
        {
            return targetType == typeof(Dto.LookupReference);
        }

        public bool CanMapFromSource(Type sourceType)
        {
            return sourceType == typeof(Frequency);
        }

        public IEnumerable<Dto.LookupReference> Map(IEnumerable<Frequency> source)
        {
            return source.Select(this.Map);

        }

        public Dto.LookupReference Map(Frequency source)
        {
            return new Dto.LookupReference()
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description
            };
        }

        public Frequency Map(LookupReference source)
        {
            return new Frequency()
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description
            };
        }

        public IEnumerable<Frequency> Map(IEnumerable<LookupReference> source)
        {
            return source.Select(this.Map);
        }
    }
}
