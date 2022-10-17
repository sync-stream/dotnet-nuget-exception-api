using System.Net;
using Microsoft.AspNetCore.Http;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the model structure for our API exception
/// </summary>
public abstract class ApiException : System.Exception
{
    /// <summary>
    ///     This property contains the numeric value of the HTTP status code
    /// </summary>
    public int Code { get; set; } = StatusCodes.Status500InternalServerError;

    /// <summary>
    ///     This property contains the HTTP status code
    /// </summary>
    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

    /// <summary>
    ///     This method instantiates an empty throwable API exception
    /// </summary>
    public ApiException()
    {
    }

    /// <summary>
    ///     This method instantiates a throwable API exception with an HTTP <paramref name="status" />
    /// </summary>
    /// <param name="status">The HTTP status to send to the client</param>
    public ApiException(HttpStatusCode status)
    {
        // Set the HTTP status code into the instance
        Code = (int) status;

        // Set the HTTP status into the instance
        Status = status;
    }

    /// <summary>
    ///     This method instantiates a throwable API exception with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiException(string message, System.Exception innerException = null) : base(message, innerException)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable API exception with an HTTP <paramref name="status" />, <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="status">The HTTP status to send to the browser</param>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiException(HttpStatusCode status, string message, System.Exception innerException = null) : this(message,
        innerException)
    {
        // Set the HTTP status code into the instance
        Code = (int) status;

        // Set the HTTP status into the instance
        Status = status;
    }

    /// <summary>
    ///     This method instantiates an ApiException from an existing exception
    /// </summary>
    /// <param name="status">The HTTP status to send to the client</param>
    /// <param name="exception">The existing exception</param>
    public ApiException(HttpStatusCode status, System.Exception exception) : this(status, exception.Message,
        exception.InnerException)
    {
    }
}
