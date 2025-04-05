using api.Dtos.List;
using api.Exceptions;
using api.Interfaces;

namespace api.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public async Task<IEnumerable<ListDto>> GetAll()
        {
            var lists = await _listRepository.GetAllAsync();
            var listsDto = lists.Select(ListDto.FromModel);

            return listsDto;
        }

        public async Task<ListDto?> GetById(int id)
        {
            var list = await _listRepository.GetByIdAsync(id);

            if (list is null)
            {
                throw new NotFoundException("List not found");
            }

            return ListDto.FromModel(list);
        }

        public async Task<ListDto> Create(CreateListDto dto)
        {
            var list = await _listRepository.CreateAsync(dto);

            return ListDto.FromModel(list);
        }

        public async Task<ListDto> Update(int id, UpdateListDto dto)
        {
            var list = await _listRepository.UpdateAsync(id, dto);

            if (list is null)
            {
                throw new NotFoundException("List not found");
            }

            return ListDto.FromModel(list);
        }

        public async Task<ListDto> Delete(int id)
        {
            var list = await _listRepository.DeleteAsync(id);

            if (list is null)
            {
                throw new NotFoundException("List not found");
            }

            return ListDto.FromModel(list);
        }
    }
}
