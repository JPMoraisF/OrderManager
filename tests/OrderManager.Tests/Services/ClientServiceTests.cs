using Microsoft.EntityFrameworkCore;
using Moq;
using OrderManager.Models;
using OrderManager.Models.DTOs.ClientDto;
using OrderManager.Repository;
using OrderManager.Services;
using System.Diagnostics.CodeAnalysis;

namespace OrderManager.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _mockRepo;
        private readonly ClientService _service;
        private List<Client> _clientsInMemory;

        public ClientServiceTests()
        {
            _clientsInMemory = new List<Client>
            {
                new Client
                {
                    Id = Guid.NewGuid(), 
                    Name = "Alice", 
                    Email = "alice@test.com", 
                    Phone = "123", 
                    WorkOrders = new List<WorkOrder>
                    {
                        new WorkOrder
                        {
                            Id = Guid.NewGuid(),
                            ClientId = Guid.NewGuid(),
                        }
                    }
                },
                new Client { Id = Guid.NewGuid(), Name = "Bob", Email = "bob@test.com", Phone = "456", WorkOrders = new List<WorkOrder>
                {
                    new WorkOrder
                    {
                        Id = Guid.NewGuid(),
                        ClientId = Guid.NewGuid(),
                    }
                }},
                new Client { Id = Guid.NewGuid(), Name = "Charlie", Email = "test@email.com", Phone = "789" }
            };
            _mockRepo = new Mock<IClientRepository>();
            _service = new ClientService(_mockRepo.Object);

            _mockRepo.Setup(r => r.FindAll())
                .ReturnsAsync(_clientsInMemory);

            _mockRepo.Setup(r => r.FindById(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => _clientsInMemory.FirstOrDefault(c => c.Id == id));

            _mockRepo.Setup(r => r.AddClient(It.IsAny<Client>()))
                .ReturnsAsync(new Client());
        }

        #region
        [Fact]
        public async Task ShouldRetrieveAllClients()
        {
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task ShouldAddANewClient_WhenClientNotExists_ShouldReturnAddedClient()
        {
            // Arrange
            var newClient = await Client.Create(
                "Test Client",
                "testemail@email.com",
                async (email) => await Task.FromResult(false),
                "123456"
            );

            var newClientDto = new ClientCreateDto
            {
                Name = newClient.Name,
                Email = newClient.Email,
                Phone = newClient.Phone
            };

            _mockRepo.Setup(r => r.FindById(newClient.Id))
                .ReturnsAsync((Client?)null);

            _mockRepo.Setup(r => r.AddClient(newClient))
                .ReturnsAsync(newClient);

            // Act
            var result = await _service.CreateAsync(newClientDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ShouldAddANewClient_WhenEmailAlreadyExists_ShouldReturnError()
        {
            // Arrange
            Func<Task> action = async () =>
            {
                await Client.Create(
                    "Alice",
                    "alice@test.com",
                    async (email) => await Task.FromResult(true),
                    "123456"
                );
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Equal("Email must be unique.", exception.Message);
        }
        #endregion
        
        [Fact]
        public async Task ShouldGetClient_WhenClientExists_ShouldReturnClient()
        {
            // Arrange
            var client = new Client
            {
                Email = "randomEmail@email.com",
                Name = "Client Name",
                Phone = "12345",
                Id = Guid.NewGuid()
            };

            _mockRepo.Setup(r => r.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(client);

            // Act
            var result = await _service.GetByIdAsync(client.Id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ShouldGetClient_WhenClientNotExists_ShouldReturnNotFoundError()
        {
            // Act
            var result = await _service.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task ShouldDeleteClient_WhenClientExists_ShouldReturnDeletedClient()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteClient(_clientsInMemory[2]))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(_clientsInMemory[2].Id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ShouldDeleteClient_WhenClientNotExists_ShouldReturnNotFoundError()
        {
            // Arrange
            var clientToDelete = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Client to Delete",
                Email = "testEmail@email.com",
                Phone = "123456"
            };

            // Act
            var result = await _service.DeleteAsync(clientToDelete.Id);

            // Assert
            Assert.False(result.Success);
            Assert.False(result.Data);
        }


        [Fact]
        public async Task ShouldDeleteClient_WhenErrorDuringSave_ShouldReturnTransactionError()
        {
            // Arrange
            var clientToDelete = new Client()
            {
                Id = _clientsInMemory[0].Id,
                Name = "Client to Delete",
                Email = "testEmail@email.com",
                Phone = "123456"
            };

            _mockRepo.Setup(r => r.DeleteClient(clientToDelete))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(clientToDelete.Id);

            // Assert
            Assert.False(result.Success);
            Assert.False(result.Data);
        }

        [Fact]
        public async Task ShouldUpdateClient_WhenClientFound_ShouldReturnUpdatedClient()
        {
            var clientUpdateDto = new ClientUpdateDto
            {
                Name = "New Name",
            };

            // Arrange
            var existingClient = _clientsInMemory[0];
            var changedClient = existingClient;
            changedClient.Name = clientUpdateDto.Name;

            _mockRepo.Setup(r => r.UpdateClient(existingClient))
                .ReturnsAsync(changedClient);

            // Act
            var result = await _service.UpdateAsync(existingClient.Id, clientUpdateDto);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("New Name", result.Data.Name);
        }

        [Fact]
        public async Task ShouldUpdateClient_WhenClientNotExists_ShouldReturnNotFoundError()
        {
            // Arrange
            var clientUpdateDto = new ClientUpdateDto
            {
                Name = "New Name",
            };

            // Act
            var result = await _service.UpdateAsync(It.IsAny<Guid>(), clientUpdateDto);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task ShouldUpdateClient_WhenErrorDuringSave_ShouldReturnTransactionError()
        {
            // Arrange
            var clientToUpdate = new ClientUpdateDto()
            {
                Name = "Client to Update",
                Phone = "123456"
            };

            var existingClient = _clientsInMemory[0];

            _mockRepo.Setup(r => r.UpdateClient(existingClient))
                .ThrowsAsync(new DbUpdateException());

            // Act
            var result = await _service.UpdateAsync(existingClient.Id, clientToUpdate);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

    }
}
