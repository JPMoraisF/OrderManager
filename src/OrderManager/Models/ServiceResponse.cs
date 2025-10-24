namespace OrderManager.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        /// <summary>
        /// Boolean indicating if the service call was successful.
        /// </summary>
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
    }
}
