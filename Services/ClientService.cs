using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderManager.Models;
using OrderManager.Models.DTOs.ClientDto;
using OrderManager.Repository;
using OrderManager.Services.Helpers;

namespace OrderManager.Services
{
    public class ClientService(IClientRepository clientRepository) : IClientService
        
    {
        public async Task<ServiceResponse<ClientDTO>> CreateAsync(ClientCreateDto newClient)
        {
            try
            {
                var newClientRequest = await Client.Create(
                    newClient.Name,
                    newClient.Email,
                    clientRepository.EmailExists,
                    newClient.Phone
                );
                var addResult = await clientRepository.AddClient(newClientRequest);
                return ResponseBuilderHelper.Success<ClientDTO>(new ClientDTO
                {
                    Name = addResult.Name,
                    Email = addResult.Email,
                    Phone = addResult.Phone,
                    Id = addResult.Id
                }, "Client added successfully.");
            }
            catch (ArgumentException argEx)
            {
                return ResponseBuilderHelper.Failure<ClientDTO>($"Validation error: {argEx.Message}");
            }
            catch (DbUpdateException dbEx)
            {
                return ResponseBuilderHelper.Failure<ClientDTO>($"Database error adding the client. Please try again.{dbEx.Message}");
            }
            catch (Exception ex)
            {
                return ResponseBuilderHelper.Failure<ClientDTO>($"Error adding the client. Please try again.{ex.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(Guid clientId)
        {
            var existingClient = await clientRepository.FindById(clientId);
            if (existingClient == null)
            {
                return ResponseBuilderHelper.Failure<bool>("Client not found!");
            }
            var canDeleteClient = existingClient.CanDeleteClient();
            if (!canDeleteClient)
            {
                return ResponseBuilderHelper.Failure<bool>("Client cannot be deleted because it has associated work orders that are not closed.");
            }
            var deleteResult = await clientRepository.DeleteClient(existingClient);
            if(!deleteResult)
            {
                return ResponseBuilderHelper.Failure<bool>("Error deleting the client. Please try again.");
            }
            else
            {
                return ResponseBuilderHelper.Success<bool>(true, "Client deleted successfully.");
            }
        }

        public async Task<ServiceResponse<ClientDetailsDto>> GetByIdAsync(Guid clientId)
        {
            var client = await clientRepository.FindById(clientId);
            if (client == null)
            {
                return ResponseBuilderHelper.Failure<ClientDetailsDto>("Client not found.");
            }
            var clientDto = client.Adapt<ClientDetailsDto>();
            return ResponseBuilderHelper.Success<ClientDetailsDto>(clientDto, "Client retrieved successfully.");
        }

        public async Task<ServiceResponse<List<ClientDTO>>> GetAllAsync()
        {
            var allClients = await clientRepository.FindAll();
            var allClientsDto = allClients.Adapt<List<ClientDTO>>();
            return ResponseBuilderHelper.Success<List<ClientDTO>>(allClientsDto, "Clients retrieved successfully.");
        }

        public async Task<ServiceResponse<ClientDTO>> UpdateAsync(Guid clientId, ClientUpdateDto updatedClient)
        {
            var existingClient = await clientRepository.FindById(clientId);
            if (existingClient == null)
            {
                return ResponseBuilderHelper.Failure<ClientDTO>("Client not found.");
            }
            try
            {
                existingClient.UpdateClient(
                updatedClient.Name,
                updatedClient.Phone
            );
                var updateResult = await clientRepository.UpdateClient(existingClient);
                // missing check here
                var existingClientDto = existingClient.Adapt<ClientDTO>();
                return ResponseBuilderHelper.Success<ClientDTO>(existingClientDto, "Client updated successfully.");
            }
            catch (ArgumentNullException argEx)
            {
                return ResponseBuilderHelper.Failure<ClientDTO>($"Validation error: {argEx.Message}");
            }
            catch (DbUpdateException dbEx)
            {
                return ResponseBuilderHelper.Failure<ClientDTO>($"Database error updating the client. Please try again.{dbEx.Message}");
            }
            catch (Exception ex)
            {
                return ResponseBuilderHelper.Failure<ClientDTO>($"Error updating the client. Please try again.{ex.Message}");
            }
        }
    }
}
