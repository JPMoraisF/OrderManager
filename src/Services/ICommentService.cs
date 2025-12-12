using OrderManager.Models;
using OrderManager.Models.DTOs.CommentDto;

namespace OrderManager.Services
{
    public interface ICommentService
    {
        Task<ServiceResponse<CommentDTO>> AddComment(Guid workOrderId, string newComment);
        Task<ServiceResponse<List<CommentDTO>>> GetCommentsByWorkOrderId(Guid workOrderId);
    }
}
