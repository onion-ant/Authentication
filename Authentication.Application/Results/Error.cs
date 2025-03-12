namespace Authentication.Application.Results;
public sealed class Error
{
    public static readonly Error None = new(null, string.Empty, string.Empty);
    public static readonly Error NullValue = new(null, "Null Value", "The specified result value is null");
    public static Error BadRequest(string message) => new(400, "Bad Request", message);
    public static Error BadRequest(List<string> message) => new(400, "Bad Request", message);
    public static Error Unauthorized() => new(401, "Unauthorized", "The specified method requires authorization.");
    public static Error Unauthorized(string message) => new(401, "Unauthorized", message);
    public static Error Unauthorized(List<string> message) => new(401, "Unauthorized", message);
    public static readonly Error Forbidden = new(403, "Forbidden", "The specified method requires higher permission.");
    public static Error NotFound() => new(404, "Not Found", "The specified resource was not found.");
    public static Error NotFound(string message) => new(404, "Not Found", message);
    public static Error Conflict(string message) => new(409, "Conflict", message);
    public static Error Conflict(List<string> message) => new(409, "Conflict", message);
    public static Error InternalServerError() => new(500, "Internal Server Error", "An internal error occurred.");
    public static Error InternalServerError(string message) => new(500, "Internal Server Error", message);

    private Error(int? statusCode, string title, string message)
    {
        StatusCode = statusCode;
        Title = title;
        Message.Add(message);
    }

    private Error(int? statusCode, string title, List<string> messageList)
    {
        StatusCode = statusCode;
        Title = title;
        Message = messageList;
    }

    public readonly int? StatusCode;
    public readonly string? Title;
    public List<string> Message = new List<string>();
}

