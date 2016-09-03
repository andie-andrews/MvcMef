using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMef.Dependencies.Payload
{
    public class Response : IResponse
    {
        public dynamic Data { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

        public string ErrorMessage { get; set; }

        public string ResponseMessage { get; set; }
        public bool IsSuccessful => string.IsNullOrWhiteSpace(ErrorMessage);
    }
}
