namespace OrderManager.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
}
