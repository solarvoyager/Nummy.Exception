﻿using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nummy.ExceptionHandler.Models;

namespace Nummy.ExceptionHandler.Middlewares;

public class NummyExceptionMiddleware
{
    private readonly RequestDelegate _next;

    //private readonly ILogger _logger;
    private readonly NummyExceptionOptions _options;

    public NummyExceptionMiddleware(RequestDelegate next, IOptions<NummyExceptionOptions> options)
    {
        //_logger = logger;
        _next = next;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            //_logger.LogError(exception: ex);

            if (_options.ReturnResponseDuringException)
                await HandleExceptionAsync(httpContext, _options);
            else throw ex;
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, NummyExceptionOptions options)
    {
        context.Response.ContentType = options.ResponseContentType == NummyResponseContentType.Json
            ? "application/json"
            : "application/xml";

        context.Response.StatusCode = (int)options.ResponseStatusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(options.Response));
    }
}