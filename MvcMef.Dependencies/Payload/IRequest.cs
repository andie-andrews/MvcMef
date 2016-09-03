namespace MvcMef.Dependencies.Payload
{
    public interface IRequest
    {
        dynamic Data { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
    }
}