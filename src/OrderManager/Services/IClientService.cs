using OrderManager.Models;
using OrderManager.Models.DTOs.ClientDto;

namespace OrderManager.Services
{
    public interface IClientService 
    {
        public Task<ServiceResponse<List<ClientDTO>>> GetAllAsync();
        public Task<ServiceResponse<ClientDetailsDto>> GetByIdAsync(Guid clientId);
        public Task<ServiceResponse<ClientDTO>> CreateAsync(ClientCreateDto newClient);
        public Task<ServiceResponse<ClientDTO>> UpdateAsync(Guid clientId, ClientUpdateDto updatedClient);
        public Task<ServiceResponse<bool>> DeleteAsync(Guid clientId);
    }
}
