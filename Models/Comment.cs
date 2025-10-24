namespace OrderManager.Models
{
    public class Comment
    {
        /// <summary>
        /// The comment id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The comment
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// The comment created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The work order id associated with the comment
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// The work order reference object
        /// </summary>
        public WorkOrder WorkOrder { get; set; } = null!;

        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="text">The comment text</param>
        /// <param name="workOrderId">The work order id for this comment.</param>
        public void Create(string text, Guid workOrderId)
        {
            Id = Guid.NewGuid();
            Text = text;
            CreatedAt = DateTime.UtcNow;
            WorkOrderId = workOrderId;
        }
    }
}
