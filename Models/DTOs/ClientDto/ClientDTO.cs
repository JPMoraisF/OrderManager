using OrderManager.Models.DTOs.WorkOrderDto;

namespace OrderManager.Models.DTOs.ClientDto
{
    public class ClientDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public List<Guid>? WorkOrders { get; set; }

    }
}
