using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using OrderManager.Models.DTOs.CommentDto;
using OrderManager.Services;

namespace OrderManager.Controllers
{
    [Route("api/workorders/{workOrderId}/comments")]
    [ApiController]
    public class CommentController(ICommentService commentService) : ControllerBase
    {

        /// <summary>
        /// Adds a new comment for a specific work order.
        /// </summary>
        /// <param name="workOrderId">The work order id that will be associated with this comment.</param>
        /// <param name="newComment">The comment text to be added to the work order</param>
        /// <returns>The comment Dto object</returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CommentDTO>>> AddComment(Guid workOrderId, string newComment)
        {
            var response = await commentService.AddComment(workOrderId, newComment);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Get comments associated with a given work order id
        /// </summary>
        /// <param name="workOrderId">The work order id</param>
        /// <returns>A list of comments for a given work order id</returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CommentDTO>>>> GetCommentsByWorkOrderId(Guid workOrderId)
        {
            var response = await commentService.GetCommentsByWorkOrderId(workOrderId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
