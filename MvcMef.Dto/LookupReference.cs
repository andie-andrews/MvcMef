using System.ComponentModel.Composition;
using MvcMef.Dependencies.Models;
namespace MvcMef.Dto
{
    [Export(typeof(ILookupReference))]
    public class LookupReference : ILookupReference, IIdentity
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
