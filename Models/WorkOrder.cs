namespace OrderManager.Models
{
    public class WorkOrder
    {
        public Guid Id { get; set; }

        public Client Client { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public WorkOrderStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public List<Comment> Comments { get; set; }

        public void MarkAsCompleted()
        {
            if(Status != WorkOrderStatusEnum.OPEN)
            {
                throw new InvalidOperationException("Work order cannot be closed.");
            }
            Status = WorkOrderStatusEnum.CLOSED;
            CompletedDate = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if(Status != WorkOrderStatusEnum.OPEN)
            {
                throw new InvalidOperationException("Work order cannot be cancelled.");
            }
            Status = WorkOrderStatusEnum.CANCELLED;
            CompletedDate = DateTime.UtcNow;
        }
    }
}
