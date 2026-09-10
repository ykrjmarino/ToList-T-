using backend.exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace backend.middleware;

public class GlobalExceptionHandler(
ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        _logger.LogError(
            exception,
            "An unhandled exception occurred: {Message}",
            exception.Message
        );

        var (statusCode, title) = exception switch
        {
            KeyNotFoundException
                or EmailNotFoundException
                or ResourceNotFoundException
                => (
                    StatusCodes.Status404NotFound,
                    "Resource Not Found"
                ),

            InvalidCredentialsException
                => (
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized"
                ),

            UserDeactivatedException
                or AdminRegistrationException
                or UnauthorizedAccessException
                => (
                    StatusCodes.Status403Forbidden,
                    "Forbidden Access"
                ),

            EmailAlreadyExistsException
                or UsernameAlreadyExistsException
                => (
                    StatusCodes.Status409Conflict,
                    "Conflict"
                ),

            AuthRoleNotFoundException
                or InvalidDataException
                or ArgumentException
                => (
                    StatusCodes.Status400BadRequest,
                    "Bad Request"
                ),

            DomainExceptions
                => (
                    StatusCodes.Status400BadRequest,
                    "Business Rule Violation"
                ),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error"
            )
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode == StatusCodes.Status500InternalServerError
                ? "An unexpected error occurred."
                : exception.Message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken
        );

        return true;
    }
}