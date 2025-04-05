using api.Dtos.List;

namespace api.Interfaces;

public interface IListService
{
    Task<IEnumerable<ListDto>> GetAll();
    Task<ListDto?> GetById(int id);
    Task<ListDto> Create(CreateListDto request);
    Task<ListDto> Update(int id, UpdateListDto request);
    Task<ListDto> Delete(int id);
}
