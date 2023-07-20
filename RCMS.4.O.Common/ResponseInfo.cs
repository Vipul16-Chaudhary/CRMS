using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Common
{
    public class ResponseInfo<T>
    {
        public int ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public Enums.ResultType ResultType { get; set; }
        public T Result { get; set; }
    }
}
