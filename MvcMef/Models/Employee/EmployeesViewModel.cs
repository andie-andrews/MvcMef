using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using MvcMef.Dependencies;
using MvcMef.Dependencies.Models;

namespace MvcMef.Models.Employee
{
    public interface IEmployeesViewModel
    {
        ExportFactory<IEmployeeService> Factory { get; set; }
        List<IEmployee> Employees { get; }
    }

    [Export(typeof(IViewModel))]
    [Export(typeof(IEmployeesViewModel))]
    public class EmployeesViewModel: IViewModel, IEmployeesViewModel
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