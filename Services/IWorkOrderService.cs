using OrderManager.Models;
using OrderManager.Models.DTOs;

namespace OrderManager.Services
{
    public interface IWorkOrderService
    {
        public Task<ServiceResponse<List<WorkOrder>>> GetWorkOrders();
        public Task<ServiceResponse<WorkOrder>> GetWorkOrder(Guid workOrderId);
        public Task<ServiceResponse<WorkOrder>> CreateWorkOrder(WorkOrderDTO newWorkOrder);
        public Task<ServiceResponse<bool>> MarkWorkOrderAsCompleted(Guid workOrderId);
        public Task<ServiceResponse<WorkOrder>> FindByClientId(Guid clientId);
    }

}
