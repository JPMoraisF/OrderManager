using Microsoft.EntityFrameworkCore;
using OrderManager.Data;
using OrderManager.Models;

namespace OrderManager.Repository
{
    public class WorkOrderRepository(OrderManagerContext context) : IWorkOrderRepository
    {
        public async Task<WorkOrder?> CreateWorkOrder(WorkOrder newWorkOrder)
        {
            context.WorkOrders.Add(newWorkOrder);
            await SaveChangesAsync();
            return newWorkOrder;
        }

        /// <summary>
        /// Returns a list of all work orders in the database.
        /// </summary>
        /// <returns>A list of work orders in the database</returns>
        public Task<List<WorkOrder>> GetAllWorkOrders()
        {
            return context.WorkOrders.ToListAsync();
        }

        /// <summary>
        /// Gets a work order by a given client identifier
        /// </summary>
        /// <param name="clientId">The client identifier</param>
        /// <returns>The work order for a given client id.</returns>
        public Task<WorkOrder?> GetWorkOrderByClientId(Guid clientId)
        {
            var workOrder = context.WorkOrders
                .FirstOrDefaultAsync(wo => wo.ClientId == clientId);
            return workOrder;
        }

        /// <summary>
        /// Gets a work order by a given unique identifier
        /// </summary>
        /// <param name="workOrderId">The work order id guid</param>
        /// <returns>The work order object</returns>
        public async Task<WorkOrder?> GetWorkOrderById(Guid workOrderId)
        {
            var workOrder = await context.WorkOrders
                .Include(wo => wo.Client)
                .Include(wo => wo.Comments)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId);
            return workOrder;
        }

        /// <summary>
        /// Updates a work order in the database.
        /// </summary>
        /// <param name="workOrder">The work order object to be updated</param>
        /// <returns>A boolean indicating whether the update operation was successful or not</returns>
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
