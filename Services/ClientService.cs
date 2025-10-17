using OrderManager.Models;
using OrderManager.Models.DTOs;
using OrderManager.Repository;
using OrderManager.Services.Helpers;

namespace OrderManager.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<ServiceResponse<Client>> AddClient(ClientDTO newClient)
        {
            var existingClient = await _clientRepository.FindByEmail(newClient.Email);
            if (existingClient != null && existingClient.Email == newClient.Email)
            {
                return ResponseBuilderHelper.Failure<Client>("A client with this email already exists.");
            }

            // TODO: Refactor this to be more DDD like with methods to manipulate the Client entity itself
            var newClientRequest = new Client
            {
                Name = newClient.Name,
                Email = newClient.Email,
                Phone = newClient.Phone,
            };

            var addResult = await _clientRepository.AddClient(newClientRequest);
            if(!addResult)
            {
                return ResponseBuilderHelper.Failure<Client>("Error adding the client. Please try again.");
            }
            else
            {
                return ResponseBuilderHelper.Success<Client>(newClientRequest, "Client added successfully.");
            }
        }

        public async Task<ServiceResponse<Client>> DeleteClient(Guid clientId)
        {
            var existingClient = await _clientRepository.FindById(clientId);
            if (existingClient == null)
            {
                return ResponseBuilderHelper.Failure<Client>("Client not found!");
            }
            
            var deleteResult = await _clientRepository.DeleteClient(existingClient);
            if(!deleteResult)
            {
                return ResponseBuilderHelper.Failure<Client>("Error deleting the client. Please try again.");
            }
            else
            {
                return ResponseBuilderHelper.Success<Client>(existingClient, "Client deleted successfully.");
            }
        }

        public async Task<ServiceResponse<Client>> GetClient(Guid clientId)
        {
            // This Find By Id check could be more reusable.
            var client = await _clientRepository.FindById(clientId);
            if (client == null)
            {
                return ResponseBuilderHelper.Failure<Client>("Client not found.");
            }
            else
            {
                return ResponseBuilderHelper.Success<Client>(client, "Client retrieved successfully.");
            };
        }

        public async Task<ServiceResponse<List<Client>>> GetClients()
        {
            var allClients = await _clientRepository.FindAll();
            return ResponseBuilderHelper.Success<List<Client>>(allClients, "Clients retrieved successfully.");
        }

        public async Task<ServiceResponse<Client>> UpdateClient(ClientDTO updatedClient)
        {
            var existingClient = await _clientRepository.FindByEmail(updatedClient.Email);
            if (existingClient == null)
            {
                return ResponseBuilderHelper.Failure<Client>("Client not found.");
            }
            // This update could be in the domain class
            existingClient.Name = updatedClient.Name ?? existingClient.Name;
            existingClient.Phone = updatedClient.Phone ?? existingClient.Phone;

            var updateResult = await _clientRepository.UpdateClient(existingClient);
            if(!updateResult)
            {
                return ResponseBuilderHelper.Failure<Client>("Error updating the client. Please try again.");
            }
            else
            {
                return ResponseBuilderHelper.Success<Client>(existingClient, "Client updated successfully.");
            }
        }
    }
}
