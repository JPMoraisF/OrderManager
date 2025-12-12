using OrderManager.Models;
using Mapster;
using OrderManager.Models.DTOs.WorkOrderDto;
using OrderManager.Models.DTOs.ClientDto;

namespace OrderManager.Mapper
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<WorkOrder, Guid>
                .NewConfig()
                .MapWith(src => src.Id);


            TypeAdapterConfig<Comment, Guid>
                .NewConfig()
                .MapWith(src => src.Id);


            TypeAdapterConfig<ClientCreateDto, Client>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone);

            TypeAdapterConfig<ClientUpdateDto, Client>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Phone, src => src.Phone);

            TypeAdapterConfig<Client, ClientDetailsDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone);

            TypeAdapterConfig<Client, ClientDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.WorkOrders, src => src.WorkOrders.Select(w => w.Id).ToList() ?? new List<Guid>());

            TypeAdapterConfig<WorkOrderDTO, WorkOrder>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            TypeAdapterConfig<WorkOrder, WorkOrderDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.Comments, src => src.Comments == null ? null : src.Comments.Select(w => w.Id).ToList());
        }
    }
}
