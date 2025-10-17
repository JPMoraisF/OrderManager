using OrderManager.Models;

namespace OrderManager.Repository
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        public Task<bool> CreateWorkOrder(WorkOrder newWorkOrder)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkOrder>> GetAllWorkOrders()
        {
            throw new NotImplementedException();
        }

        public Task<WorkOrder> GetWorkOrderByClientId(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkOrder> GetWorkOrderById(Guid workOrderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWorkOrder(WorkOrder workOrder)
        {
            throw new NotImplementedException();
        }
    }
}
