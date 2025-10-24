using Microsoft.EntityFrameworkCore;
using OrderManager.Data;
using OrderManager.Models;

namespace OrderManager.Repository
{
    public class ClientRepository(OrderManagerContext context) : IClientRepository
    {
        public async Task<Client> AddClient(Client client)
        {
            context.Clients.Add(client);
            await SaveChangesAsync();
            return client;
        }

        public async Task<bool> DeleteClient(Client clientToDelete)
        {
            context.Clients.Remove(clientToDelete);
            return await SaveChangesAsync();
        }

        public Task<bool> EmailExists(string email)
        {
            return context.Clients.AnyAsync(c => c.Email == email);
        }

        public Task<List<Client>> FindAll()
        {
            return context.Clients
                .Include(w => w.WorkOrders)
                .ToListAsync();
        }

        public Task<Client?> FindByEmail(string email)
        {
            var client = context.Clients.FirstOrDefaultAsync(c => c.Email == email);
            return client;
        }

        public async Task<Client?> FindById(Guid id)
        {
            var client = await context.Clients
                .Include(wo => wo.WorkOrders)
                .FirstOrDefaultAsync(c => c.Id == id);
            return client;
        }

        public async Task<List<Client>> FindByName(string name)
        {
            var clients = await context.Clients.Where(c => c.Name.Contains(name)).ToListAsync();
            return clients;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Client> UpdateClient(Client client)
        {
            context.Clients.Update(client);
            await SaveChangesAsync();
            return client;
        }
    }
}
