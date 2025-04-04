using api.Dtos.Task;

namespace api.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<Models.Task>> GetAllAsync();
    Task<Models.Task?> GetByIdAsync(int id);
    Task<Models.Task> CreateAsync(CreateTaskDto taskDto);
    Task<Models.Task?> UpdateAsync(int id, UpdateTaskDto taskDto);
    Task<Models.Task?> DeleteAsync(int id);
}
