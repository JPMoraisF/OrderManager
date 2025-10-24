using OrderManager.Models.DTOs.WorkOrderDto;

namespace OrderManager.Models.DTOs.ClientDto
{
    /// <summary>
    /// Client DTO for retrieving client general information as in a list
    /// </summary>
    public class ClientDTO
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
        public string Phone { get; set; }

        /// <summary>
        /// A list of work order IDs associated with the client
        /// </summary>
        public List<Guid>? WorkOrders { get; set; }

    }
}
