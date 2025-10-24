using OrderManager.Models;

namespace OrderManager.Repository
{
    public interface ICommentRepository
    {
        Task<Comment> AddComment(Comment newComment);
        Task<bool> SaveChangesAsync();
    }
}
