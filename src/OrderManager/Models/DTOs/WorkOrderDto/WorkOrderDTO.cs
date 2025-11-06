using System.ComponentModel.DataAnnotations;

namespace OrderManager.Models.DTOs.WorkOrderDto
{
    public class WorkOrderDTO
    {
        /// <summary>
        /// The work order Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The work order description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The price, expressed in decimal
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The work order Status Enum
        /// </summary>
        public WorkOrderStatusEnum Status { get; set; }

        /// <summary>
        /// Work order created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The client email
        /// </summary>
        public string ClientEmail { get; set; }

        /// <summary>
        /// A list of comments for the work order.
        /// </summary>

        public List<Guid>? Comments { get; set; }

    }
}
