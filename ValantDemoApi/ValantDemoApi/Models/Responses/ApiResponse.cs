using System;
using System.Net;

namespace ValantDemoApi.Models.Responses
{
    public class ApiResponse<T>
    {
        private static readonly string _messageHttp200 = "Information successfully recorded";
        private static readonly string _messageHttp400 = "An error occurred while trying to perform this action";
        private static readonly string _messageHttp401 = "Access denied";
        private static readonly string _messageHttp403 = "Access denied. You do not have permission to access this functionality";
        private static readonly string _messageHttp404 = "Address not found";
        private static readonly string _messageHttp415 = "The request type is not supported by the API";
        private static readonly string _messageHttp500 = "An unexpected error occurred";

        public static ApiResponse<T> SuccessResult(HttpStatusCode statusCode, T result)
            => new(statusCode, result, (int)statusCode is >= (int)HttpStatusCode.OK and <= 299);

        public static ApiResponse<T> BadRequestResult()
            => new(HttpStatusCode.BadRequest, default!, false, _messageHttp400);

        public static ApiResponse<T> BadRequestResult(string message = null!)
            => new(HttpStatusCode.BadRequest, default!, false, message ?? _messageHttp400);

        public static ApiResponse<T> BadRequestResult(T result)
            => new(HttpStatusCode.BadRequest, result, false, _messageHttp400);

        public static ApiResponse<T> CreatedResult()
            => new(HttpStatusCode.Created, default!, true, _messageHttp200);

        public static ApiResponse<T> UnauthorizedResult(string? message = null)
            => new(HttpStatusCode.Unauthorized, default!, false, message ?? _messageHttp401);

        public static ApiResponse<T> ForbiddenResult()
            => new(HttpStatusCode.Forbidden, default!, false, _messageHttp403);

        public static ApiResponse<T> NotFoundResult()
            => new(HttpStatusCode.NotFound, default!, false, _messageHttp404);

        public static ApiResponse<T> UnsupportedMediaTypeResult()
            => new(HttpStatusCode.UnsupportedMediaType, default!, false, _messageHttp415);

        public static ApiResponse<T> InternalServerErrorResult(string message = null!)
            => new(HttpStatusCode.InternalServerError, default!, false, message ?? _messageHttp500);

        public static ApiResponse<T> InternalServerErrorResult()
            => new(HttpStatusCode.InternalServerError, default!, false, _messageHttp500);

        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; }
        public string Message { get; set; }
        public T Result { get; set; }

        protected ApiResponse(HttpStatusCode statusCode, T result, bool success, string message = null)
        {
            RequestId = Guid.NewGuid().ToString();
            Success = success;
            StatusCode = (int)statusCode;
            Result = result;
            Message = message;
        }
    }
}
