using Microsoft.EntityFrameworkCore;
using OrderManager.Data;
using OrderManager.Models;
using System.Reflection.Metadata.Ecma335;

namespace OrderManager.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly OrderManagerContext _context;

        public ClientRepository(OrderManagerContext context)
        {
            _context = context;
        }

        public async Task<bool> AddClient(Client client)
        {
            _context.Clients.Add(client);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteClient(Client clientToDelete)
        {
            _context.Clients.Remove(clientToDelete);
            return await SaveChangesAsync();
        }

        public Task<List<Client>> FindAll()
        {
            return _context.Clients.ToListAsync();
        }

        public Task<Client?> FindByEmail(string email)
        {
            var client = _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
            return client;
        }

        public async Task<Client?> FindById(Guid id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            return client;
        }

        public async Task<List<Client>> FindByName(string name)
        {
            var clients = await _context.Clients.Where(c => c.Name.Contains(name)).ToListAsync();
            return clients;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            return await SaveChangesAsync();
        }
    }
}
