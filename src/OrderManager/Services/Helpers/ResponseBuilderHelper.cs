using OrderManager.Models;

namespace OrderManager.Services.Helpers
{
    public static class ResponseBuilderHelper
    {
        public static ServiceResponse<T> Success<T>(T data, string message)
        {
            return new ServiceResponse<T>
            {
                Data = data,
                Success = true,
                Message = message
            };
        }
        public static ServiceResponse<T> Failure<T>(string message)
        {
            return new ServiceResponse<T>
            {
                Data = default,
                Success = false,
                Message = message
            };
        }
    }
}
