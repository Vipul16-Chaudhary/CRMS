using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Utilities.RCMSClock
{
    public class RCMSClockTraceMiddleware
    {
        private readonly RequestDelegate _next;

        public RCMSClockTraceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            /*not unique*/
            context.TraceIdentifier = Guid.NewGuid().ToString();
            string id = context.TraceIdentifier;
            context.Response.Headers["X-Trace-Id"] = id;
            await _next(context);
        }
    }
}
