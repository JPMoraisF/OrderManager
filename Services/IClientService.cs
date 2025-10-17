using OrderManager.Models;
using OrderManager.Models.DTOs;

namespace OrderManager.Services
{
    public interface IClientService
    {
        public Task<ServiceResponse<List<Client>>> GetClients();
        public Task<ServiceResponse<Client>> GetClient(Guid clientId);
        public Task<ServiceResponse<Client>> AddClient(ClientDTO newClient);
        public Task<ServiceResponse<Client>> UpdateClient(ClientDTO updatedClient);
        public Task<ServiceResponse<Client>> DeleteClient(Guid clientId);
    }
}
