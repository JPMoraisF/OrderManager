namespace OrderManager.Models.DTOs.WorkOrderDto
{
    public class WorkOrderCreateDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid ClientId { get; set; }
    }
}
