using Microsoft.EntityFrameworkCore;
using OrderManager.Data;
using OrderManager.Models;

namespace OrderManager.Repository
{
    public class WorkOrderRepository(OrderManagerContext context) : IWorkOrderRepository
    {
        public async Task<WorkOrder> CreateWorkOrder(WorkOrder newWorkOrder)
        {
            context.WorkOrders.Add(newWorkOrder);
            await SaveChangesAsync();
            return newWorkOrder;
        }

        public Task<List<WorkOrder>> GetAllWorkOrders()
        {
            return context.WorkOrders.ToListAsync();
        }

        public Task<WorkOrder?> GetWorkOrderByClientId(Guid clientId)
        {
            var workOrder = context.WorkOrders
                .FirstOrDefaultAsync(wo => wo.Id == clientId);
            return workOrder;
        }

        public async Task<WorkOrder?> GetWorkOrderById(Guid workOrderId)
        {
            var workOrder = await context.WorkOrders
                .Include(wo => wo.Client)
                .Include(wo => wo.Comments)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId);
            return workOrder;
        }

        public async Task<bool> UpdateWorkOrder(WorkOrder workOrder)
        {
            context.WorkOrders.Update(workOrder);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
