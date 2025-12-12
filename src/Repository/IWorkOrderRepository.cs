using OrderManager.Models;

namespace OrderManager.Repository
{
    public interface IWorkOrderRepository
    {
        public Task<List<WorkOrder>> GetAllWorkOrders();
        public Task<WorkOrder?> GetWorkOrderById(Guid workOrderId);
        public Task<WorkOrder?> GetWorkOrderByClientId(Guid clientId);
        public Task<WorkOrder?> CreateWorkOrder(WorkOrder newWorkOrder);
        public Task<bool> UpdateWorkOrder(WorkOrder workOrder);
    }
}
