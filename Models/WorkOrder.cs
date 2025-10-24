namespace OrderManager.Models
{
    public class WorkOrder
    {
        /// <summary>
        /// The work order id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The work order description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The work order price, expressed in decimal
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The work order status. Default value is OPEN
        /// </summary>
        public WorkOrderStatusEnum Status { get; set; } = WorkOrderStatusEnum.OPEN;

        /// <summary>
        /// The work order created date.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The work order updated date.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// The work order completed date.
        /// </summary>
        public DateTime? CompletedDate { get; set; }


        // Navigation
        /// <summary>
        /// The work order's client id
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// The client reference object
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// The work order's comment list
        /// </summary>
        public List<Comment>? Comments { get; set; }


        /// <summary>
        /// Marks a work order as completed and sets the completed date to the current UTC time.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if order is not on OPEN state.</exception>
        public void MarkAsCompleted()
        {
            if(Status != WorkOrderStatusEnum.OPEN)
            {
                throw new InvalidOperationException("Work order cannot be closed.");
            }
            Status = WorkOrderStatusEnum.CLOSED;
            UpdatedAt = DateTime.UtcNow;
            CompletedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Cancels a work order and sets the Updated At date to the current UTC time.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the order is not on OPEN state.</exception>
        public void Cancel()
        {
            if(Status != WorkOrderStatusEnum.OPEN)
            {
                throw new InvalidOperationException("Work order cannot be cancelled.");
            }
            Status = WorkOrderStatusEnum.CANCELLED;
            UpdatedAt = DateTime.UtcNow;
        }


        /// <summary>
        /// Creates a new work order with the provided description, price, and client ID.
        /// </summary>
        /// <param name="description">The work order description.</param>
        /// <param name="price">The work order price</param>
        /// <param name="clientId">The client id associated with that order.</param>
        /// <exception cref="ArgumentException">Thrown if any of the arguments given to the work order fails validation.</exception>
        public void Create(
            string description,
            decimal price,
            Guid clientId
            )
        {
            if(string.IsNullOrEmpty(description) || description.Length > 500)
            {
                throw new ArgumentException("Description is required and must be less than 500 characters.");
            }
            if(price < 0)
            {
                throw new ArgumentException("Price must be a positive value.");
            }
            Id = Guid.NewGuid();
            Description = description;
            Price = price;
            Status = WorkOrderStatusEnum.OPEN;
            CreatedAt = DateTime.UtcNow;
            ClientId = clientId;
        }
    }
}
