using api.Data;
using api.Dtos.Task;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Task>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<Models.Task?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Models.Task> CreateAsync(CreateTaskDto taskDto)
    {
        var task = new Models.Task
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            ListId = taskDto.ListId
        };

        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<Models.Task?> UpdateAsync(int id, UpdateTaskDto taskDto)
    {
        var task = await GetByIdAsync(id);

        if (task is null)
        {
            return null;
        }

        task.Title = taskDto.Title;
        task.Description = taskDto.Description;
        task.ListId = taskDto.ListId;

        if (!task.Done && taskDto.Done)
        {
            task.DoneAt = DateTime.Now;
        }

        task.Done = taskDto.Done;
        task.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<Models.Task?> DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);

        if (task is null)
        {
            return null;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return task;
    }
}
