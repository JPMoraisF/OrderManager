namespace OrderManager.Models.DTOs.WorkOrderDto
{
    public class WorkOrderCreateDto
    {
        /// <summary>
        /// The work order description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The work order price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The client id associated with the work order.
        /// </summary>
        public Guid ClientId { get; set; }
    }
}
