using Microsoft.EntityFrameworkCore;
using Moq;
using OrderManager.Models;
using OrderManager.Models.DTOs.ClientDto;
using OrderManager.Models.DTOs.WorkOrderDto;
using OrderManager.Services;
using System.Diagnostics.CodeAnalysis;

namespace OrderManager.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class WorkOrderServiceTests
    {
        private readonly Mock<Repository.IWorkOrderRepository> _mockRepo;
        private readonly Mock<IClientService> _mockClientService;
        private readonly WorkOrderService _service;
        private List<WorkOrder> _workOrdersInMemory;

        public WorkOrderServiceTests()
        {
            _mockRepo = new Mock<Repository.IWorkOrderRepository>();
            _mockClientService = new Mock<IClientService>();
            _service = new WorkOrderService(_mockRepo.Object, _mockClientService.Object);

            _workOrdersInMemory = new List<WorkOrder>
            {
                new WorkOrder { Id = Guid.NewGuid(), Description = "Fix screen", Price = 150.0m, Client = new Client { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@email.com"}, },
                new WorkOrder { Id = Guid.NewGuid(), Description = "Replaced battery", Price = 200.0m, Client = new Client { Id = Guid.NewGuid(), Name = "Bob", Email = "bob@email.com" } },
            };
        }

        [Fact]
        public async Task ShouldCreateANewWorkOrder_WhenClientnotExist_ShouldReturnError()
        {
            // Arrange
            _mockClientService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ServiceResponse<ClientDetailsDto>
                {
                    Data = null,
                    Success = false,
                    Message = "Client not found. Cannot create work order."
                });

            var newWorkOrder = new Models.DTOs.WorkOrderDto.WorkOrderCreateDto
            {
                Description = "Test Work Order",
                Price = 100.0m,
                ClientId = Guid.NewGuid()
            };

            // Act
            var result = await _service.CreateWorkOrder(newWorkOrder);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task ShouldCreateANewWorkOrder_WhenErrorDuringWorkOrderSave_ShouldReturnError()
        {
            // Arrange
            var existingClient = new ClientDetailsDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Client",
                Email = "test@email.com",
            };

            _mockClientService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ServiceResponse<ClientDetailsDto>
                {
                    Data = existingClient,
                    Success = true,
                    Message = "Clients returned."
                });
            _mockRepo.Setup(r => r.CreateWorkOrder(It.IsAny<WorkOrder>()))
                .ThrowsAsync(new DbUpdateException());

            var newWorkOrder = new WorkOrderCreateDto
            {
                Description = "Test Work Order",
                Price = 100.0m,
                ClientId = existingClient.Id
            };

            // Act
            var result = await _service.CreateWorkOrder(newWorkOrder);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task ShouldCreateANewWorkOrder_WhenAllIsValid_ShouldReturnTheCreatedWorkOrder()
        {
            // Arrange
            var existingClient = new ClientDetailsDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Client",
                Email = "test@email.com",
            };
            _mockClientService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ServiceResponse<ClientDetailsDto>
                {
                    Data = existingClient,
                    Success = true,
                    Message = "Clients returned."
                });
            _mockRepo.Setup(r => r.CreateWorkOrder(It.IsAny<WorkOrder>()))
                .ReturnsAsync(new WorkOrder
                { 
                    CompletedDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    Comments = null,
                    Description = "test",
                    Price = 10,
                    Status  = WorkOrderStatusEnum.OPEN,
                    UpdatedAt = DateTime.Now,
                    Client = new Client { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@test.com", Phone = "123" }
                });


            var newWorkOrder = new WorkOrderCreateDto
            {
                Description = "Test Work Order",
                Price = 100.0m,
                ClientId = existingClient.Id
            };
            // Act
            var result = await _service.CreateWorkOrder(newWorkOrder);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(newWorkOrder.Description, result.Data.Description);
            Assert.Equal(newWorkOrder.Price, result.Data.Price);
            Assert.Equal(existingClient.Id, result.Data.Id);
        }

        [Fact]
        public async Task ShouldFindWorkOrderByClientId_WhenWorkOrderDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetWorkOrderByClientId(It.IsAny<Guid>()))
                .ReturnsAsync((WorkOrder?)null);
            // Act
            var result = await _service.FindByClientId(Guid.NewGuid());
            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
        }
        [Fact]
        public async Task ShouldFindWorkOrderByClientId_WhenWorkOrderExists_ShouldReturnWorkOrder()
        {
            // Arrange
            var existingWorkOrder = new WorkOrder
            {
                Id = Guid.NewGuid(),
                Description = "Existing Work Order",
                Price = 150.0m,
                Client = new Client { Id = Guid.NewGuid(), Name = "Test Client", Email = "testemail@email.com" },
            };
            _mockRepo.Setup(r => r.GetWorkOrderByClientId(It.IsAny<Guid>()))
                .ReturnsAsync(existingWorkOrder);
            // Act
            var result = await _service.FindByClientId(existingWorkOrder.Client.Id);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);

        }

        [Fact]
        public async Task ShouldFindWorkOrderById_WhenWorkOrderDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetWorkOrderById(It.IsAny<Guid>()))
                .ReturnsAsync((WorkOrder?)null);
            // Act
            var result = await _service.GetWorkOrder(Guid.NewGuid());
            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
        }
        [Fact]
        public async Task ShouldFindWorkOrderById_WhenWorkOrderExists_ShouldReturnWorkOrder()
        {
            // Arrange
            var existingWorkOrder = new WorkOrder
            {
                Id = Guid.NewGuid(),
                Description = "Existing Work Order",
                Price = 150.0m,
                Client = new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Client",
                    Email = "test@email.com"
                },
            };
            _mockRepo.Setup(r => r.GetWorkOrderById(It.IsAny<Guid>()))
                .ReturnsAsync(existingWorkOrder);
            // Act
            var result = await _service.GetWorkOrder(existingWorkOrder.Id);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ShouldGetWorkOrders_WhenOrdersExists_ShouldReturlList()
        {
            // Arrange
            var workOrders = new List<WorkOrder>
            {
                new WorkOrder
                {
                    Id = Guid.NewGuid(),
                    Description = "Work Order 1",
                    Price = 100.0m,
                    Client = new Client { Id = Guid.NewGuid(), Name = "Client 1", Email = "test@email.com"},
                },
                new WorkOrder
                {
                    Id = Guid.NewGuid(),
                    Description = "Work Order 2",
                    Price = 200.0m,
                    Client = new Client { Id = Guid.NewGuid(), Name = "Client 2", Email = "test@email.com"},
                }
            };
            _mockRepo.Setup(r => r.GetAllWorkOrders())
                .ReturnsAsync(workOrders);
            // Act
            var result = await _service.GetWorkOrders();
            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ShouldGetWorkOrders_WhenNoOrdersExists_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllWorkOrders())
                .ReturnsAsync(new List<WorkOrder>());
            // Act
            var result = await _service.GetWorkOrders();
            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }
        [Fact]
        public async Task ShouldMarkWorkOrderAsCompleted_WhenWorkOrderIsOpened_ShouldClose()
        {
            //Arrange 
            var existingWorkOrder = new WorkOrder
            {
                Id = Guid.NewGuid(),
                Description = "Existing Work Order",
                Price = 150.0m,
                Status = WorkOrderStatusEnum.OPEN,
                Client = new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Client",
                    Email = "test@email.com"
                }
            };
            _mockRepo.Setup(r => r.GetWorkOrderById(It.IsAny<Guid>()))
                .ReturnsAsync(existingWorkOrder);
            _mockRepo.Setup(r => r.UpdateWorkOrder(It.IsAny<WorkOrder>()))
                .ReturnsAsync(true);
            // Act
            var result = await _service.MarkWorkOrderAsCompleted(existingWorkOrder.Id);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task ShouldMarkWorkOrderAsCompleted_WhenWorkOrderIsAlreadyCompleted_ShouldReturnException()
        {
            //Arrange 
            var existingWorkOrder = new WorkOrder
            {
                Id = Guid.NewGuid(),
                Description = "Existing Work Order",
                Price = 150.0m,
                Status = WorkOrderStatusEnum.CLOSED,
                Client = new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Client",
                    Email = "test@email.com"
                }
            };
            _mockRepo.Setup(r => r.GetWorkOrderById(It.IsAny<Guid>()))
                .ReturnsAsync(existingWorkOrder);

            // Act
            var result = await _service.MarkWorkOrderAsCompleted(existingWorkOrder.Id);
            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Work order cannot be closed.", result.Message);
        }

        [Fact]
        public async Task ShouldMarkWorkOrderAsCompleted_WhenErrorDuringUpdate_ShouldReturnError()
        {
            //Arrange 
            var existingWorkOrder = new WorkOrder
            {
                Id = Guid.NewGuid(),
                Description = "Existing Work Order",
                Price = 150.0m,
                Status = WorkOrderStatusEnum.OPEN,
                Client = new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Client",
                    Email = "test@email.com"

                }
            };
            _mockRepo.Setup(r => r.GetWorkOrderById(It.IsAny<Guid>()))
                .ReturnsAsync(existingWorkOrder);
            _mockRepo.Setup(r => r.UpdateWorkOrder(It.IsAny<WorkOrder>()))
                .ReturnsAsync(false);
            // Act
            var result = await _service.MarkWorkOrderAsCompleted(existingWorkOrder.Id);
            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            // This is very weird
            Assert.False(result.Data);
        }
        [Fact]
        public async Task ShouldMarkWorkOrderAsCompleted_WhenWorkOrderIsNotFound_ShouldReturnError()
        {
            //Arrange 
            _mockRepo.Setup(r => r.GetWorkOrderById(It.IsAny<Guid>()))
                .ReturnsAsync((WorkOrder?)null);
            // Act
            var result = await _service.MarkWorkOrderAsCompleted(Guid.NewGuid());
            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal("Work order not found.", result.Message);
        }
    }
}
