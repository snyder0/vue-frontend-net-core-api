using System.Collections.Generic;
using System.Linq;

namespace StarterApi.Common.Responses
{
    public class Response
    {
        public object Data { get; set; }
        public bool IsValid => !ErrorMessages.Any();
        public List<ErrorMessage> ErrorMessages { get; set; } = new List<ErrorMessage>();
    }

    public class Response<T> : Response
    {
        public new T Data { get; set; }
    }
}
