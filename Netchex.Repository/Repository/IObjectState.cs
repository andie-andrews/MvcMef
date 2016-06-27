namespace Netchex.Repository.Repository
{
    namespace Data
    {
        public interface IObjectState
        {
            ObjectState State { get; set; }
        }

        public enum ObjectState
        {
            Unchanged,
            Added,
            Modified,
            Deleted
        }

    }
}
