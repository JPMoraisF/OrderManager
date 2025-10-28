using Mapster;
using OrderManager.Models;
using OrderManager.Models.DTOs.WorkOrderDto;
using OrderManager.Services.Helpers;

namespace OrderManager.Services
{
    public class WorkOrderService(Repository.IWorkOrderRepository workOrderRepository, IClientService clientService) : IWorkOrderService
    {
        public async Task<ServiceResponse<WorkOrderDTO>> CreateWorkOrder(WorkOrderCreateDto newWorkOrder)
        {
            var clientServiceResponse = await clientService.GetByIdAsync(newWorkOrder.ClientId);
            if(clientServiceResponse.Data == null)
            {
                return ResponseBuilderHelper.Failure<WorkOrderDTO>("Client not found. Cannot create work order.");
            }
            var workOrder = new WorkOrder();
            // This can throw an exception if validation fails
            workOrder.Create(
                newWorkOrder.Description,
                newWorkOrder.Price,
                clientServiceResponse.Data.Id
            );

            var createdOrder = await workOrderRepository.CreateWorkOrder(workOrder);
            // This doesn't return the created work order
            if(createdOrder == null)
            {
                return ResponseBuilderHelper.Failure<WorkOrderDTO>("Error creating work order. Please try again.");
            }
            else
            {
                var orderDto = createdOrder.Adapt<WorkOrderDTO>();
                return ResponseBuilderHelper.Success<WorkOrderDTO>(orderDto, "Work order created successfully.");
            }
        }

        public async Task<ServiceResponse<WorkOrderDTO>> FindByClientId(Guid clientId)
        {
            var order = await workOrderRepository.GetWorkOrderByClientId(clientId);
            if(order == null)
            {
                return ResponseBuilderHelper.Failure<WorkOrderDTO>("No work orders found for this client.");
            }
            else
            {
                var orderDto = order.Adapt<WorkOrderDTO>();
                return ResponseBuilderHelper.Success<WorkOrderDTO>(orderDto, "Work orders retrieved successfully.");
            }
        }

        public async Task<ServiceResponse<WorkOrderDetailsDto>> GetWorkOrder(Guid workOrderId)
        {
            var order = await workOrderRepository.GetWorkOrderById(workOrderId);
            if(order == null)
            {
                return ResponseBuilderHelper.Failure<WorkOrderDetailsDto>("Work order not found.");
            }
            else
            {
                var orderDto = order.Adapt<WorkOrderDetailsDto>();
                return ResponseBuilderHelper.Success<WorkOrderDetailsDto>(orderDto, "Work order retrieved successfully.");
            }
        }

        public async Task<ServiceResponse<List<WorkOrderDTO>>> GetWorkOrders()
        {
            var orders = await workOrderRepository.GetAllWorkOrders();
            var orderDto = orders.Adapt<List<WorkOrderDTO>>();
            return ResponseBuilderHelper.Success<List<WorkOrderDTO>>(orderDto, "Work orders retrieved.");
        }

        public async Task<ServiceResponse<bool>> MarkWorkOrderAsCompleted(Guid workOrderId)
        {
            var existingOrder = await workOrderRepository.GetWorkOrderById(workOrderId);
            if (existingOrder == null)
            {
                return ResponseBuilderHelper.Failure<bool>("Work order not found.");
            }
            // This can throw an exception if the status is not OPEN
            try
            {
                existingOrder.MarkAsCompleted();
                var updateResult = await workOrderRepository.UpdateWorkOrder(existingOrder);
                if (!updateResult)
                {
                    return ResponseBuilderHelper.Failure<bool>("Error updating work order status. Please try again.");
                }
                else
                {
                    return ResponseBuilderHelper.Success<bool>(true, "Work order marked as completed successfully.");  
                }
            }
            catch (InvalidOperationException ex)
            {
                return ResponseBuilderHelper.Failure<bool>(ex.Message);
            }
        }
    }
}
