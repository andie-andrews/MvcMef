using MvcMef.Dependencies.Models;
namespace MvcMef.Dto
{
    public class LookupReference : ILookupReference, IIdentity
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
