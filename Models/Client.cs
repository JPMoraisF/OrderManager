namespace OrderManager.Models
{
    public class Client
    {
        /// <summary>
        /// The client id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The client name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The client email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The client phone number
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Creation date of the client
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Updated date of the client
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Work order list for the client
        /// </summary>
        public List<WorkOrder> WorkOrders { get; set; } = [];

        /// <summary>
        /// Create the client entity to be saved on the database.
        /// </summary>
        /// <remarks>This method also updates the <see cref="CreatedAt"/> property to the current UTC
        /// time.</remarks>
        /// <param name="name">The name of the client. Must be a non-empty string with a maximum length of 100 characters.</param>
        /// <param name="phone">The phone number of the client. Must have a maximum length of 20 characters.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is null, empty, or exceeds 100 characters,  or if <paramref name="phone"/>
        /// exceeds 20 characters.</exception>
        public static async Task<Client> Create(
            string name,
            string email,
            Func<string, Task<bool>> emailExists,
            string? phone = null
            )
        {
            if(string.IsNullOrEmpty(name) || name.Length > 100)
            {
                throw new ArgumentException("Name is required and must be less than 100 characters.");
            }
            if(string.IsNullOrEmpty(email) || !email.Contains('@'))
            {
                throw new ArgumentException("Email is required and must be valid.");
            }
            if(!string.IsNullOrEmpty(phone) && phone.Length > 20)
            {
                throw new ArgumentException("Phone must be less than 20 characters.");
            }

            if(await emailExists(email))
            {
                throw new ArgumentException("Email must be unique.");
            }
            return new Client
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Phone = phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }
        /// <summary>
        /// Returns a boolean indicating if the client can be deleted.
        /// The client can be deleted only if all associated work orders are closed.
        /// </summary>
        /// <returns>A bool indicating if the client can be deleted.</returns>
        public bool CanDeleteClient()
        {
            return WorkOrders.Any(o => o.Status != WorkOrderStatusEnum.CLOSED) ? false : true;
        }

        /// <summary>
        /// Updates the client's name and phone number.
        /// </summary>
        /// <remarks>This method also updates the <see cref="UpdatedAt"/> property to the current UTC
        /// time.</remarks>
        /// <param name="name">The new name of the client. Must be a non-empty string with a maximum length of 100 characters.</param>
        /// <param name="phone">The new phone number of the client. Must be a non-empty string with a maximum length of 20 characters.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is null, empty, or exceeds 100 characters,  or if <paramref name="phone"/>
        /// exceeds 20 characters.</exception>
        public void UpdateClient(
            string name,
            string phone
            )
        {
            if(string.IsNullOrEmpty(name) || name.Length > 100)
            {
                throw new ArgumentException("Name is required and must be less than 100 characters.");
            }
            if(!string.IsNullOrEmpty(phone) && phone.Length > 20)
            {
                throw new ArgumentException("Phone must be less than 20 characters.");
            }
            Name = name;
            Phone = phone;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
