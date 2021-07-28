using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Middlewares
{
    public static class DeleteNotConfirmedMiddlewareExt
    {
        public static IApplicationBuilder UseDeleteNotConfirmed(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DeleteNotConfirmedMiddleware>();
        }
    }
}
