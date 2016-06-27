using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netchex.Dependencies.Models;

namespace Netchex.Dependencies
{
    public interface IEmployeeService
    {
        IEmployee GetItem(int? id);
        List<IEmployee> GetItems();
        IEmployee Update(string employee);
    }
}
