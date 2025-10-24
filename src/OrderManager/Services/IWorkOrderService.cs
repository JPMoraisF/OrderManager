using OrderManager.Models;
using OrderManager.Models.DTOs.WorkOrderDto;

namespace OrderManager.Services
{
    public interface IWorkOrderService
    {
        public Task<ServiceResponse<List<WorkOrderDTO>>> GetWorkOrders();
        public Task<ServiceResponse<WorkOrderDetailsDto>> GetWorkOrder(Guid workOrderId);
        public Task<ServiceResponse<WorkOrderDTO>> CreateWorkOrder(WorkOrderCreateDto newWorkOrder);
        public Task<ServiceResponse<bool>> MarkWorkOrderAsCompleted(Guid workOrderId);
        public Task<ServiceResponse<WorkOrderDTO>> FindByClientId(Guid clientId);
    }

}
