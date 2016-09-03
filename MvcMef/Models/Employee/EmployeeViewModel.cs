using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using MvcMef.Dependencies;
using MvcMef.Dependencies.Models;
namespace MvcMef.Models.Employee
{
    public interface IEmployeeViewModel
    {
        IEmployee Employee { get; set; }
        List<ILookupReference> Fequencies { get; }
        void IntializeEmployee(int? id);
        void Update(IEmployee employee);
        void Update(string employee);
    }

    [Export(typeof(IViewModel))]
    [Export(typeof(IEmployeeViewModel))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class EmployeeViewModel : IViewModel, IEmployeeViewModel
    {
        public IEmployee Employee { get;  set; }
 
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
            this.Employee = EmployeeServiceFactory.CreateExport().Value.GetItem(id);
        }

        public void Update(IEmployee employee)
        {

            this.Employee = EmployeeServiceFactory.CreateExport().Value.Update(employee);
        }
        public void Update(string employee)
        {

            this.Fequencies = referenceService.GetFrequencyReferenceList();
            this.Employee = EmployeeServiceFactory.CreateExport().Value.Update(employee);
        }
    }
}