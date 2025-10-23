namespace OrderManager.Models.DTOs.ClientDto
{
    public class ClientCreateDto
    {
        /// <summary>
        /// The client name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The client email. Must me in email format.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The client phone. Must be at maximum 20 characters
        /// </summary>
        public string Phone { get; set; }
    }
}
