using api.Dtos.Task;

namespace api.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAll();
    Task<TaskDto?> GetById(int id);
    Task<TaskDto> Create(CreateTaskDto request);
    Task<TaskDto> Update(int id, UpdateTaskDto request);
    Task<TaskDto> Delete(int id);
}
