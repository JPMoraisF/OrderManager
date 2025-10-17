using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using OrderManager.Models.DTOs;
using OrderManager.Services;

namespace OrderManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderService _workOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<WorkOrder>>>> GetWorkOrders()
        {
            var response = await _workOrderService.GetWorkOrders();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{workOrderId}")]
        public async Task<ActionResult<ServiceResponse<WorkOrder>>> GetWorkOrder(Guid workOrderId)
        {
            var response = await _workOrderService.GetWorkOrder(workOrderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{workOrderId}/complete")]
        public async Task<ActionResult> MarkWorkOrderAsCompleted(Guid workOrderId)
        {
            var response = await _workOrderService.MarkWorkOrderAsCompleted(workOrderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<WorkOrder>>> AddWorkOrder(WorkOrderDTO newWorkOrder)
        {
            var response = await _workOrderService.CreateWorkOrder(newWorkOrder);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
