using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMef.Dependencies.Payload
{
  
    public class Request : IRequest
    {
        public dynamic Data { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
 
    }
}
