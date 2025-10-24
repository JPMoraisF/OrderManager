using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using OrderManager.Models.DTOs.CommentDto;
using OrderManager.Services;

namespace OrderManager.Controllers
{
    [Route("api/workorders/{workOrderId}[controller]")]
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
