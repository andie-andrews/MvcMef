using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netchex.Dependencies.Models;
namespace Netchex.Dto
{
    public class Employee : IEmployee, IIdentity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Wages { get; set; }
        public bool IsHourly { get; set; }
        public DateTime? StartDate { get; set; }
        public int? PayFrequencyId { get; set; }
    }
}
