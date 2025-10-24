using OrderManager.Data;
using OrderManager.Models;

namespace OrderManager.Repository
{
    public class CommentRepository(OrderManagerContext context) : ICommentRepository
    {
        /// <summary>
        /// Adds a new comment to the database.
        /// </summary>
        /// <param name="newComment">The comment object to be saved</param>
        /// <returns>The comment object created in the database</returns>
        public async Task<Comment> AddComment(Comment newComment)
        {
            await context.Comments.AddAsync(newComment);
            await SaveChangesAsync();
            return newComment;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
