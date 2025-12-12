using Microsoft.EntityFrameworkCore;
using Moq;
using OrderManager.Models;
using OrderManager.Models.DTOs.CommentDto;
using OrderManager.Models.DTOs.WorkOrderDto;
using OrderManager.Repository;
using OrderManager.Services;
using System.Diagnostics.CodeAnalysis;

namespace OrderManager.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly Mock<IWorkOrderService> _workOrderService;
        private readonly CommentService _commentService;

        public CommentServiceTests()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _workOrderService = new Mock<IWorkOrderService>();
            _commentService = new CommentService(_commentRepository.Object, _workOrderService.Object);
        }

        [Fact]
        public async Task AddComment_ShouldAddCommentWhenWorkOrderIsFound()
        {
            // Arrange
            var existingWorkOrder = new WorkOrderDetailsDto
            {
                Id = Guid.NewGuid(),
                Description = "Test Work Order",
                Price = 100.0m,
            };

            _workOrderService.Setup(r => r.GetWorkOrder(existingWorkOrder.Id))
                .ReturnsAsync(new ServiceResponse<WorkOrderDetailsDto>
                {
                    Data = existingWorkOrder,
                    Success = true
                });

            // Act
            var result = await _commentService.AddComment(existingWorkOrder.Id, "New comment");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task AddComment_ShouldFailWhenWorkOrderIsNotFound()
        {
            // Arrange
            _workOrderService.Setup(r => r.GetWorkOrder(It.IsAny<Guid>()))
                .ReturnsAsync(new ServiceResponse<WorkOrderDetailsDto>
                {
                    Data = null,
                    Success = false
                });
            // Act
            var result = await _commentService.AddComment(It.IsAny<Guid>(), "This work order id doesn't exist");
            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Work order not found!", result.Message);
        }
           [Fact]
           public async Task AddComment_WhenRepositoryFails_ShouldReturnFailureResponse()
           {
               // Arrange
               var existingWorkOrder = new WorkOrderDetailsDto
               {
                   Id = Guid.NewGuid(),
                   Description = "Test Work Order",
                   Price = 100.0m,
               };
               var commentDto = new CommentDTO
               {
                   WorkOrderId = existingWorkOrder.Id,
                   Text = "This is a test comment."
               };
               _workOrderService.Setup(r => r.GetWorkOrder(existingWorkOrder.Id))
                   .ReturnsAsync(new ServiceResponse<WorkOrderDetailsDto>
                   {
                       Data = existingWorkOrder,
                       Success = true
                   });
               _commentRepository.Setup(r => r.AddComment(It.IsAny<Comment>()))
                   .ThrowsAsync(new DbUpdateException());
            // Act
            var result = await _commentService.AddComment(existingWorkOrder.Id, "This work order id exists");
               // Assert
               Assert.NotNull(result);
               Assert.False(result.Success);
               Assert.Equal("Error adding the comment. Please try again.", result.Message);
           }

        [Fact]
        public async Task GetCommentsByWorkOrderId_ShouldReturnComments()
        {
            // Arrange
            var workOrderId = Guid.NewGuid();
            var comments = new List<Comment>
            {
                new Comment { WorkOrderId = workOrderId, Text = "First comment" },
                new Comment { WorkOrderId = workOrderId, Text = "Second comment" }
            };
            _workOrderService.Setup(r => r.GetWorkOrder(workOrderId))
                .ReturnsAsync(new ServiceResponse<WorkOrderDetailsDto> { Data = new WorkOrderDetailsDto { Comments = comments } });
            // Act
            var result = await _commentService.GetCommentsByWorkOrderId(workOrderId);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
        }
    }
}
