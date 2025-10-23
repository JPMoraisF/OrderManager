using OrderManager.Models;

namespace OrderManager.Repository
{
    public interface IClientRepository
    {
        public Task<List<Client>> FindAll();
        public Task<List<Client>> FindByName(string name);
        public Task<Client?> FindByEmail(string email);
        public Task<bool> EmailExists(string email);
        public Task<Client?> FindById(Guid id);

        public Task<Client> AddClient(Client client);

        public Task<bool> DeleteClient(Client clientToDelete);
        public Task<Client> UpdateClient(Client client);

        public Task<bool> SaveChangesAsync();
    }
}
