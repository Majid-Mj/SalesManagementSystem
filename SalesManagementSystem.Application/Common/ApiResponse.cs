namespace SalesManagementSystem.Application.Common;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> SuccessResponse(T? data, string? message = "Success", int statusCode = 200) =>
        new() { IsSuccess = true, StatusCode = statusCode, Message = message, Data = data };

    public static ApiResponse<T> FailureResponse(string message, int statusCode = 400) =>
        new() { IsSuccess = false, StatusCode = statusCode, Message = message, Data = default };

    public static ApiResponse<T> Created(T data, string message = "Resource created successfully") =>
        SuccessResponse(data, message, 201);

    public static ApiResponse<T> NotFound(string message = "Resource not found") =>
        FailureResponse(message, 404);

    public static ApiResponse<T> BadRequest(string message = "Bad request") =>
        FailureResponse(message, 400);

    public static ApiResponse<T> Conflict(string message = "Resource already exists") =>
        FailureResponse(message, 409);

    public static ApiResponse<T> Unauthorized(string message = "Unauthorized access") =>
        FailureResponse(message, 401);
}

public class ApiResponse : ApiResponse<object>
{
    public static ApiResponse SuccessResponse(string? message = "Success", int statusCode = 200) =>
        new() { IsSuccess = true, StatusCode = statusCode, Message = message, Data = null };

    public static new ApiResponse FailureResponse(string message, int statusCode = 400) =>
        new() { IsSuccess = false, StatusCode = statusCode, Message = message, Data = null };

    public static ApiResponse Created(string message = "Resource created successfully") =>
        SuccessResponse(message, 201);

    public static new ApiResponse NotFound(string message = "Resource not found") =>
        FailureResponse(message, 404);

    public static new ApiResponse BadRequest(string message = "Bad request") =>
        FailureResponse(message, 400);

    public static new ApiResponse Conflict(string message = "Resource already exists") =>
        FailureResponse(message, 409);

    public static new ApiResponse Unauthorized(string message = "Unauthorized access") =>
        FailureResponse(message, 401);
}
