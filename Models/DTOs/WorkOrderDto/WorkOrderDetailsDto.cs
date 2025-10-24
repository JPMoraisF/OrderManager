namespace OrderManager.Models.DTOs.WorkOrderDto
{
    public class WorkOrderDetailsDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public WorkOrderStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string ClientEmail { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
