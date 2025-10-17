using OrderManager.Models;
using OrderManager.Models.DTOs;

namespace OrderManager.Repository
{
    public interface IWorkOrderRepository
    {
        public Task<List<WorkOrder>> GetAllWorkOrders();
        public Task<WorkOrder> GetWorkOrderById(Guid workOrderId);
        public Task<WorkOrder> GetWorkOrderByClientId(Guid clientId);
        public Task<bool> CreateWorkOrder(WorkOrder newWorkOrder);
        public Task<bool> UpdateWorkOrder(WorkOrder workOrder);
    }
}
