using OrderManager.Models;
using OrderManager.Models.DTOs;
using OrderManager.Repository;

namespace OrderManager.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IClientService _clientService;

        public WorkOrderService(IWorkOrderRepository workOrderRepository, IClientService clientService)
        {
            _workOrderRepository = workOrderRepository;
            _clientService = clientService;
        }

        public async Task<ServiceResponse<WorkOrder>> CreateWorkOrder(WorkOrderDTO newWorkOrder)
        {
            var response = new ServiceResponse<WorkOrder>();
            var clientServiceResponse = await _clientService.GetClient(newWorkOrder.Client.Id);
            if(clientServiceResponse.Data == null)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "Client not found. Cannot create work order.";
                return response;
            }
            // This can be moved to the WorkOrder class so it's more DDD like
            var workOrder = new WorkOrder
            {
                Id = Guid.NewGuid(),
                Client = clientServiceResponse.Data,
                Description = newWorkOrder.Description,
                Price = newWorkOrder.Price,
                Status = WorkOrderStatusEnum.OPEN,
                CreatedDate = DateTime.UtcNow,
                Comments = new List<Comment>()
            };
            var createdOrder = await _workOrderRepository.CreateWorkOrder(workOrder);
            if(!createdOrder)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "Error creating work order. Please try again.";
            }
            else
            {
                response.Data = workOrder;
                response.Success = true;
                response.Message = "Work order created successfully.";
            }
            return response;
        }

        public async Task<ServiceResponse<WorkOrder>> FindByClientId(Guid clientId)
        {
            var response = new ServiceResponse<WorkOrder>();
            var order = await _workOrderRepository.GetWorkOrderByClientId(clientId);
            if(order == null)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "No work orders found for this client.";
            }
            else
            {
                response.Data = order;
                response.Success = true;
                response.Message = "Work orders retrieved successfully.";
            }
            return response;
        }

        public async Task<ServiceResponse<WorkOrder>> GetWorkOrder(Guid workOrderId)
        {
            var response = new ServiceResponse<WorkOrder>();
            var order = await _workOrderRepository.GetWorkOrderById(workOrderId);
            if(order == null)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "Work order not found.";
            }
            else
            {
                response.Data = order;
                response.Success = true;
                response.Message = "Work order retrieved successfully.";
            }
            return response;
        }

        public async Task<ServiceResponse<List<WorkOrder>>> GetWorkOrders()
        {
            var orders = await _workOrderRepository.GetAllWorkOrders();
            return new ServiceResponse<List<WorkOrder>>
            {
                Data = orders,
                Success = true,
                Message = "Work orders retrieved successfully."
            };
        }

        public async Task<ServiceResponse<bool>> MarkWorkOrderAsCompleted(Guid workOrderId)
        {
            var response = new ServiceResponse<bool>();
            var existingOrder = await GetWorkOrder(workOrderId);
            if (existingOrder.Data == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Work order not found."
                };
            }
            // This can throw an exception if the status is not OPEN
            try
            {
                existingOrder.Data.MarkAsCompleted();
                var updateResult = await _workOrderRepository.UpdateWorkOrder(existingOrder.Data);
                if (!updateResult)
                {
                    response.Data = false;
                    response.Success = false;
                    response.Message = "Error updating work order status. Please try again.";
                }
                else
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Work order marked as completed successfully.";
                }
            }
            catch (InvalidOperationException ex)
            {

                response.Success = false;
                response.Data = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
