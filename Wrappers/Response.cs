using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPGC_API.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Data = data;
        }
        public T Data { get; set; }

    }
}
