using api.Dtos.Task;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _taskService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _taskService.GetById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto request)
    {
        var result = await _taskService.Create(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto request)
    {
        var result = await _taskService.Update(id, request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.Delete(id);
        return NoContent();
    }
}
