using Mapster;
using OrderManager.Models;
using OrderManager.Models.DTOs.CommentDto;
using OrderManager.Repository;
using OrderManager.Services.Helpers;

namespace OrderManager.Services
{
    public class CommentService(ICommentRepository commentRepository, IWorkOrderService workOrderService) : ICommentService
    {
        public async Task<ServiceResponse<CommentDTO>> AddComment(Guid workOrderId, string newComment)
        {
            var workOrder = await workOrderService.GetWorkOrder(workOrderId);
            if(workOrder.Success == false)
            {
                return ResponseBuilderHelper.Failure<CommentDTO>("Work order not found!");
            }
            try
            {
                var newCommentRequest = new Comment();
                newCommentRequest.Create(newComment, workOrderId);

                var createdComment = await commentRepository.AddComment(newCommentRequest);
                var commentDto = createdComment.Adapt<CommentDTO>();
                return ResponseBuilderHelper.Success<CommentDTO>(commentDto, "Comment added successfully.");
            }
            catch (Exception)
            {
                return ResponseBuilderHelper.Failure<CommentDTO>($"Error adding the comment. Please try again.");
            }
        }


        public async Task<ServiceResponse<List<CommentDTO>>> GetCommentsByWorkOrderId(Guid workOrderId)
        {
            var workOrder = await workOrderService.GetWorkOrder(workOrderId);
            return ResponseBuilderHelper.Success<List<CommentDTO>>(workOrder.Data.Comments.Select(c => new CommentDTO
            {
                WorkOrderId = c.WorkOrderId,
                Text = c.Text
            }).ToList(), "Comments retrieved successfully.");
        }
    }
}
