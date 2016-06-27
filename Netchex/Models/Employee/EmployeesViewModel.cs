using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Netchex.Dependencies;
using Netchex.Dependencies.Models;

namespace Netchex.Models.Employee
{
    [Export(typeof(IViewModel))]
    public class EmployeesViewModel: IViewModel
    {
        [Import]
        public ExportFactory<IEmployeeService> Factory { get; set; }

        public EmployeesViewModel()
        {

        }
        public List<IEmployee> Employees {
            get {
                return GetEmployess();
            }
        }

        private List<IEmployee> GetEmployess()
        {
            var employeeService = Factory.CreateExport().Value;
            return employeeService.GetItems();
        }

       
       
    }

}