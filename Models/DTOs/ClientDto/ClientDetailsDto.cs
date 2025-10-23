using OrderManager.Models.DTOs.WorkOrderDto;

namespace OrderManager.Models.DTOs.ClientDto
{
    public class ClientDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<WorkOrderDetailsDto> WorkOrders { get; set; } = [];
    }
}
