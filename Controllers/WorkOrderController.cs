using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using OrderManager.Models.DTOs.WorkOrderDto;
using OrderManager.Services;

namespace OrderManager.Controllers
{
    /// <summary>
    /// Work controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController(IWorkOrderService workOrderService) : ControllerBase
    {

        /// <summary>
        /// Returns a list of all work orders.
        /// </summary>
        /// <returns>A list of all work orders that have been created</returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<WorkOrderDTO>>>> GetWorkOrders()
        {
            var response = await workOrderService.GetWorkOrders();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets the details of a given work order id.
        /// </summary>
        /// <param name="workOrderId">The work order id Guid.</param>
        /// <returns>The work order details.</returns>
        [HttpGet("{workOrderId}")]
        public async Task<ActionResult<ServiceResponse<WorkOrderDTO>>> GetWorkOrder(Guid workOrderId)
        {
            var response = await workOrderService.GetWorkOrder(workOrderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Marks a work order id as complete. The work order must be in the OPEN state before closing.
        /// </summary>
        /// <param name="workOrderId">The work order id to be closed.</param>
        /// <returns>True or false indicating if the operation was successful</returns>
        [HttpPut("{workOrderId}/complete")]
        public async Task<ActionResult> MarkWorkOrderAsCompleted(Guid workOrderId)
        {
            var response = await workOrderService.MarkWorkOrderAsCompleted(workOrderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates a new work order
        /// </summary>
        /// <param name="newWorkOrder">The work order object</param>
        /// <returns>The newly created work order.</returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<WorkOrderDTO>>> AddWorkOrder(WorkOrderCreateDto newWorkOrder)
        {
            var response = await workOrderService.CreateWorkOrder(newWorkOrder);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
