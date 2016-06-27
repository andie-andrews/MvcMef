using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netchex.Dependencies.Models;

namespace Netchex.Dependencies
{
    public interface IReferenceService
    {
        List<ILookupReference> GetFrequencyReferenceList();
    }
}
