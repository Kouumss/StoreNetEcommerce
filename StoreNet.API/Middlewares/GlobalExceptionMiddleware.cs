using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using StoreNet.Application.Interfaces.Logging;

namespace StoreNet.API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAppLogger<GlobalExceptionMiddleware> _logger;
    private readonly ApiBehaviorOptions _apiBehaviorOptions;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        IAppLogger<GlobalExceptionMiddleware> logger,
        IOptions<ApiBehaviorOptions> apiBehaviorOptions,
        IWebHostEnvironment env)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _apiBehaviorOptions = apiBehaviorOptions?.Value ?? throw new ArgumentNullException(nameof(apiBehaviorOptions));
        _env = env ?? throw new ArgumentNullException(nameof(env));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception intercepted: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = CreateProblemDetails(context, exception);
        await WriteProblemDetailsResponse(context, problemDetails);
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var problemDetailsFactory = context.RequestServices.GetRequiredService<ProblemDetailsFactory>();
        var statusCode = GetStatusCodeForException(exception);
        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode: statusCode,
            title: GetTitleForException(exception),
            detail: exception.Message,
            type: GetTypeForException(exception, statusCode));

        ConfigureExceptionSpecificDetails(problemDetails, exception, statusCode);
        AddStandardExtensions(context, problemDetails);
        AddDevelopmentSpecificDetails(problemDetails, exception);

        return problemDetails;
    }

    private static int GetStatusCodeForException(Exception exception)
    {
        return exception switch
        {
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            ArgumentNullException or ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            TimeoutException => StatusCodes.Status408RequestTimeout,
            InvalidOperationException => StatusCodes.Status409Conflict,
            NotImplementedException => StatusCodes.Status501NotImplemented,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitleForException(Exception exception)
    {
        return exception switch
        {
            ValidationException => "Validation error",
            ArgumentNullException => "Missing required argument",
            ArgumentException => "Invalid argument",
            KeyNotFoundException => "Resource not found",
            UnauthorizedAccessException => "Unauthorized",
            TimeoutException => "Request timeout",
            InvalidOperationException => "Invalid operation",
            NotImplementedException => "Not implemented",
            _ => "Internal server error"
        };
    }

    private static string GetTypeForException(Exception exception, int statusCode)
    {
        return $"https://httpstatuses.io/{statusCode}";
    }

    private void ConfigureExceptionSpecificDetails(ProblemDetails problemDetails, Exception exception, int statusCode)
    {
        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = new Dictionary<string, string[]>
            {
                { "General", new[] { validationException.Message } }
            };
        }

        if (_apiBehaviorOptions.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }
    }

    private static void AddStandardExtensions(HttpContext context, ProblemDetails problemDetails)
    {
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        problemDetails.Extensions["traceId"] = traceId;
        problemDetails.Extensions["timestamp"] = DateTime.UtcNow.ToString("o");
        problemDetails.Extensions["instance"] = context.Request.Path;
    }

    private void AddDevelopmentSpecificDetails(ProblemDetails problemDetails, Exception exception)
    {
        if (!_env.IsDevelopment())
            return;

        problemDetails.Extensions["exception"] = new
        {
            Type = exception.GetType().Name,
            StackTrace = exception.StackTrace,
            InnerException = exception.InnerException?.Message
        };
    }

    private static async Task WriteProblemDetailsResponse(HttpContext context, ProblemDetails problemDetails)
    {
        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}