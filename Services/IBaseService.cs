using OrderManager.Models;

namespace OrderManager.Services
{
    public interface IBaseService<TEntity, TCreateDto, TUpdateDto, TDto>
        where TEntity : class
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class
    {
        Task<ServiceResponse<List<TDto>>> GetAllAsync();
        Task<ServiceResponse<TDto>> GetByIdAsync(Guid id);
        Task<ServiceResponse<TDto>> CreateAsync(TCreateDto dto);
        Task<ServiceResponse<TDto>> UpdateAsync(Guid id, TUpdateDto dto);
        Task<ServiceResponse<bool>> DeleteAsync(Guid id);
    }
}
