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
    public class EmployeeMapping : IMapping<Employee, Dto.Employee>
    {
        public bool CanMap(Type sourceType, Type targetType)
        {
            return this.CanMapFromSource(sourceType) && this.CanMapToTarget(targetType);
        }

        public bool CanMapToTarget( Type targetType)
        {
            return targetType == typeof(Dto.Employee);
        }

        public bool CanMapFromSource(Type sourceType)
        {
            return sourceType == typeof(Employee);
        }

        public IEnumerable<Dto.Employee> Map(IEnumerable<Employee> source)
        {
            return source.Select(this.Map);

        }

        public Dto.Employee Map(Employee source)
        {
            return new Dto.Employee()
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Wages = source.Wages,
                IsHourly = source.IsHourly,
                StartDate = source.StartDate,
                PayFrequencyId = source.Frequency != null ? source.Frequency.Id : 0

            };
        }

        public Employee  Map(Dto.Employee source)
        {
            return new Employee()
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Wages = source.Wages,
                IsHourly = source.IsHourly,
                StartDate = source.StartDate.GetValueOrDefault(),
                FrequencyId = source.PayFrequencyId

            };
        }

        public IEnumerable<Employee> Map(IEnumerable<Dto.Employee> source)
        {
            return source.Select(this.Map);
        }
    }
}
