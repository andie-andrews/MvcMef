using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netchex.Dependencies.Models
{
    public interface ILookupReference
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
