using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMef.Dependencies.Models;

namespace MvcMef.Dependencies
{
    public interface IEmployeeService
    {
        IEmployee GetItem(int? id);
        List<IEmployee> GetItems();
        IEmployee Update(string employee);
    }
}
