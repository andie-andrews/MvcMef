using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using Netchex.Dependencies;
using Netchex.Dependencies.Models;
namespace Netchex.Models.Employee
{
    [Export(typeof(IViewModel))]
    public class EmployeeViewModel : IViewModel
    {
        public  IEmployee Employee;
        protected IEmployeeService employeeService { get { return EmployeeServiceFactory.CreateExport().Value; } }
        protected IReferenceService referenceService { get { return ReferenceServiceFactory.CreateExport().Value; } }
      
        [Import]
        public ExportFactory<IEmployeeService> EmployeeServiceFactory { get; set; }

        [Import]
        public ExportFactory<IReferenceService> ReferenceServiceFactory { get; set; }

        public List<ILookupReference> Fequencies;

        public EmployeeViewModel()
        {

        }
        public void IntializeEmployee(int? id)
        {
            this.Fequencies = referenceService.GetFrequencyReferenceList();
            this.Employee = employeeService.GetItem(id);
        }

        public void Update(string employee)
        {

            this.Fequencies = referenceService.GetFrequencyReferenceList();
            this.Employee = employeeService.Update(employee);
        }
    }
}