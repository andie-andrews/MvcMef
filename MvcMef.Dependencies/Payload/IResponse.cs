namespace MvcMef.Dependencies.Payload
{
    public interface IResponse
    {
        dynamic Data { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
        string ErrorMessage { get; set; }
        string ResponseMessage { get; set; }

        bool IsSuccessful { get; }
    }
}