using Netchex.Dependencies.Models;
namespace Netchex.Dto
{
    public class LookupReference : ILookupReference, IIdentity
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
