namespace OrderManager.Models.DTOs.CommentDto
{
    public class CommentDTO
    {
        /// <summary>
        /// The work order id that the comment is associated with
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// The comment text.
        /// </summary>
        public string Text { get; set; }
    }
}
