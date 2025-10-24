namespace OrderManager.Models.DTOs.ClientDto
{
    public class ClientUpdateDto
    {
        /// <summary>
        /// The client name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The client phone number
        /// </summary>
        public string Phone { get; set; }
    }
}
