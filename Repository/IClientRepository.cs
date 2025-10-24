using OrderManager.Models;

namespace OrderManager.Repository
{
    public interface IClientRepository
    {
        public Task<List<Client>> FindAll();
        /// <summary>
        /// Finds a client by name.
        /// </summary>
        /// <param name="name">The client name to search</param>
        /// <returns>The client object with the client data.</returns>
        public Task<List<Client>> FindByName(string name);

        /// <summary>
        /// Finds a client by email.
        /// </summary>
        /// <param name="email">The client email to search</param>
        /// <returns>The client object</returns>
        public Task<Client?> FindByEmail(string email);

        /// <summary>
        /// Checks if an email already exists in the database.
        /// </summary>
        /// <param name="email">The email to search for</param>
        /// <returns>A boolean indicating whether the email already exists or not</returns>
        public Task<bool> EmailExists(string email);

        /// <summary>
        /// Finds a client by unique identifier.
        /// </summary>
        /// <param name="id">The client unique identifier</param>
        /// <returns>The client object.</returns>
        public Task<Client?> FindById(Guid id);

        /// <summary>
        /// Adds a new client to the database.
        /// </summary>
        /// <param name="client">The client object to be created</param>
        /// <returns>The newly created client object</returns>
        public Task<Client> AddClient(Client client);

        public Task<bool> DeleteClient(Client clientToDelete);
        public Task<Client> UpdateClient(Client client);

        public Task<bool> SaveChangesAsync();
    }
}
