using FormulaOneSite.Hangfire;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Middlewares
{
    public class DeleteNotConfirmedMiddleware
    {
        private readonly RequestDelegate _next;

        public DeleteNotConfirmedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,IHangfireJobs jobs)
        {
            jobs.ClearNotConfirmedUsers();

            await _next(context);
        }
    }
}
