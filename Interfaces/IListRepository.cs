using api.Dtos.List;

namespace api.Interfaces;

public interface IListRepository
{
    Task<IEnumerable<Models.List>> GetAllAsync();
    Task<Models.List?> GetByIdAsync(int id);
    Task<Models.List> CreateAsync(CreateListDto listDto);
    Task<Models.List?> UpdateAsync(int id, UpdateListDto listDto);
    Task<Models.List?> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
