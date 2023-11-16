﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nummy.ExceptionHandler.Middlewares;
using Nummy.ExceptionHandler.Models;

namespace Nummy.ExceptionHandler.Extensions
{
    public static class NummyExceptionServiceExtension
    {
        public static IServiceCollection AddNummyExceptionHandler(this IServiceCollection services, Action<NummyExceptionOptions> options)
        {
            services.Configure<NummyExceptionOptions>(options);

            return services;
        }

        public static void UseNummyExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<NummyExceptionMiddleware>();
        }

    }
}
