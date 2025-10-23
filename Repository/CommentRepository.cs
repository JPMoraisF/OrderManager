using OrderManager.Data;
using OrderManager.Models;

namespace OrderManager.Repository
{
    public class CommentRepository(OrderManagerContext context) : ICommentRepository
    {
        public async Task<Comment> AddComment(Comment newComment)
        {
            context.Comments.AddAsync(newComment);
            await SaveChangesAsync();
            return newComment;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
