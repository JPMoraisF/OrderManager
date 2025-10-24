using OrderManager.Models.DTOs.WorkOrderDto;

namespace OrderManager.Models.DTOs.ClientDto
{
    public class ClientDetailsDto
    {
        /// <summary>
        /// The client id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The client name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The client email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The client phone number
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// The date and time when the client was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the client was last updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// A list of work orders associated with the client
        /// </summary>
        public List<WorkOrderDetailsDto> WorkOrders { get; set; } = [];
    }
}
