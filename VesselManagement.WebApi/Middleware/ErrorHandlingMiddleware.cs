using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using FluentValidation;

namespace VesselManagement.WebApi.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                ProblemDetails problemDetails = ex switch
                {
                    DbUpdateException dbUpdateException => new()
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = "Database update error.",
                        Detail = dbUpdateException.Message,
                        Instance = context.Request.Path
                    },
                    ValidationException validationException => new()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation error.",
                        Detail = ex.Message,
                        Instance = context.Request.Path
                    },
                    KeyNotFoundException keyNotFoundException => new()
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Resource not found.",
                        Detail = keyNotFoundException.Message,
                        Instance = context.Request.Path
                    },
                    _ => new()
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "An unexpected error occurred while processing your request.",
                        Detail = ex.Message,
                        Instance = context.Request.Path
                    }
                };

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = problemDetails.Status.Value;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
