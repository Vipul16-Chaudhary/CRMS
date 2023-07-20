using Microsoft.AspNetCore.Http;
using RCMS._4.O.Interfaces.RCMSClockInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Utilities.RCMSClock
{
    public class RCMSClockMiddleware
    {
        private readonly RequestDelegate _next;
        public RCMSClockMiddleware (RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context, IRCMSClockStopwatch watch)
        {
            watch.Start();
            context.Response.OnStarting(state =>
            {
                watch.Stop();
                string value = string.Format("{0}ms", watch.ElapsedMilliseconds);
                context.Response.Headers["X-Response-Time"] = value;
                return Task.CompletedTask;
            }, context);
            await _next(context);
        }
    }

    public interface IRCMSClockStopwatch : IRCMSClockInterface
    {
    }

    public class RCMSClockMiddlewareStopwatch : Stopwatch, IRCMSClockStopwatch
    {
        public RCMSClockMiddlewareStopwatch() : base()
        {
        }
    }
}
