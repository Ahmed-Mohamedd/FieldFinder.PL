
using Serilog;
using System.Net;
using System.Reflection;

/// <summary>
/// Abstract handler for all exceptions.
/// </summary>
public abstract class AbstractExceptionHandlerMiddleware
{
    //// Enrich is a custom extension method that enriches the Serilog functionality - you may ignore it
    private static readonly Serilog.ILogger Logger = Log.ForContext(MethodBase.GetCurrentMethod()?.DeclaringType);

    /// <summary>
    /// This key should be used to store the exception in the <see cref="IDictionary{TKey,TValue}"/> of the exception data,
    /// to be localized in the abstract handler.
    /// </summary>
    public static string LocalizationKey => "LocalizationKey";

    private readonly RequestDelegate _next;


    /// <summary>
    /// Gets HTTP status code response and message to be returned to the caller.
    /// Use the ".Data" property to set the key of the messages if it's localized.
    /// </summary>
    /// <param name="exception">The actual exception</param>
    /// <returns>Tuple of HTTP status code and a message</returns>
    public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

    public AbstractExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
      
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            // log the error
            Logger.Error(exception, "error during executing {Context}", context.Request.Path.Value);
            var response = context.Response;
            response.ContentType = "application/json";

            // get the response code and message
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int)status;
            await response.WriteAsync(message);
        }
    }
}