using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterApi.Common.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsValid { get; set; } = true;
        public List<ErrorMessage> ErrorMessages { get; set; } = new List<ErrorMessage>();
    }
}
