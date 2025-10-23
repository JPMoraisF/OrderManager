namespace OrderManager.Models.DTOs.CommentDto
{
    public class CommentDTO
    {
        public Guid WorkOrderId { get; set; }
        public string Text { get; set; }
    }
}
