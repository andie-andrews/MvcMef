using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using MvcMef.Dependencies;
using MvcMef.Dependencies.Models;
namespace MvcMef.Models.Employee
{
    [Export(typeof(IViewModel))]
    public class EmployeeViewModel : IViewModel
    {
        public IEmployee Employee { get; private set; }
        protected virtual IEmployeeService employeeService { get { return EmployeeServiceFactory.CreateExport().Value; } }
        protected virtual IReferenceService referenceService { get { return ReferenceServiceFactory.CreateExport().Value; } }
      
        [Import]
        protected virtual ExportFactory<IEmployeeService> EmployeeServiceFactory { get; set; }

        [Import]
        protected virtual ExportFactory<IReferenceService> ReferenceServiceFactory { get; set; }

        public List<ILookupReference> Fequencies { get; private set; }

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