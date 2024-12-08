﻿using CommonPortfolio.Api.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Text.Json;

namespace CommonPortfolio.Api.Middlewares;

public class ValidationErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationErrorHandlingMiddleware(RequestDelegate next, ILogger<ValidationErrorHandlingMiddleware> logger)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest && !context.Response.HasStarted)
        {
            var response = new ApiResponseModel
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "One or more validation errors occurred.",
                Errors = new List<string>()
            };

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = context.Features.Get<IHttpResponseFeature>()?.ReasonPhrase ?? string.Empty;
            response.Errors.Add(errors);

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
}