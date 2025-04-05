using api.Dtos.Task;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IListRepository _listRepository;
    public TaskController(ITaskRepository taskRepository, IListRepository listRepository)
    {
        _taskRepository = taskRepository;
        _listRepository = listRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskRepository.GetAllAsync();
        var tasksDto = tasks.Select(t => TaskDto.FromModel(t));

        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task is null)
        {
            return NotFound("Task not found");
        }

        return Ok(TaskDto.FromModel(task));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.ListId is not null)
        {
            if (!(await _listRepository.ExistsAsync((int)request.ListId)))
            {
                return NotFound("List not found");
            }
        }

        var task = await _taskRepository.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, TaskDto.FromModel(task));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.ListId is not null)
        {
            if (!(await _listRepository.ExistsAsync((int)request.ListId)))
            {
                return NotFound("List not found");
            }
        }

        var task = await _taskRepository.UpdateAsync(id, request);

        if (task is null)
        {
            return NotFound("Task not found");
        }

        return Ok(TaskDto.FromModel(task));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskRepository.DeleteAsync(id);

        if (task is null)
        {
            return NotFound("Task not found");
        }

        return NoContent();
    }
}
