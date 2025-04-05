using api.Dtos.Task;
using api.Exceptions;
using api.Interfaces;

namespace api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IListRepository _listRepository;

        public TaskService(ITaskRepository taskRepository, IListRepository listRepository)
        {
            _taskRepository = taskRepository;
            _listRepository = listRepository;
        }

        public async Task<IEnumerable<TaskDto>> GetAll()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var tasksDto = tasks.Select(t => TaskDto.FromModel(t));

            return tasksDto;
        }

        public async Task<TaskDto?> GetById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task is null)
            {
                throw new NotFoundException("Task not found");
            }

            return TaskDto.FromModel(task);
        }

        public async Task<TaskDto> Create(CreateTaskDto dto)
        {
            if (dto.ListId is not null)
            {
                if (!(await _listRepository.ExistsAsync((int)dto.ListId)))
                {
                    throw new NotFoundException("List not found");
                }
            }

            var task = await _taskRepository.CreateAsync(dto);

            return TaskDto.FromModel(task);
        }

        public async Task<TaskDto> Update(int id, UpdateTaskDto dto)
        {
            if (dto.ListId is not null)
            {
                if (!(await _listRepository.ExistsAsync((int)dto.ListId)))
                {
                    throw new NotFoundException("List not found");
                }
            }

            var task = await _taskRepository.UpdateAsync(id, dto);

            if (task is null)
            {
                throw new NotFoundException("Task not found");
            }

            return TaskDto.FromModel(task);
        }

        public async Task<TaskDto> Delete(int id)
        {
            var task = await _taskRepository.DeleteAsync(id);

            if (task is null)
            {
                throw new NotFoundException("Task not found");
            }

            return TaskDto.FromModel(task);
        }
    }
}
