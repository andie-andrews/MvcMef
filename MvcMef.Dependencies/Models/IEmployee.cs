using System;
namespace MvcMef.Dependencies.Models
{
    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        double Wages { get; set; }
        bool IsHourly { get; set; }
        DateTime? StartDate { get; set; }
        int? PayFrequencyId { get; set; }

    }
}
