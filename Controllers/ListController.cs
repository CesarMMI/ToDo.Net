using api.Dtos.List;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/lists")]
[ApiController]
public class ListController : ControllerBase
{
    private readonly IListRepository _listRepository;
    public ListController(IListRepository taskRepository)
    {
        _listRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lists = await _listRepository.GetAllAsync();
        var listsDto = lists.Select(l => ListDto.FromModel(l));

        return Ok(lists);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var list = await _listRepository.GetByIdAsync(id);

        if (list is null)
        {
            return NotFound("List not found");
        }

        return Ok(ListDto.FromModel(list));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateListDto request)
    {
        var list = await _listRepository.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = list.Id }, ListDto.FromModel(list));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateListDto request)
    {
        var list = await _listRepository.UpdateAsync(id, request);

        if (list is null)
        {
            return NotFound("List not found");
        }

        return Ok(ListDto.FromModel(list));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var list = await _listRepository.DeleteAsync(id);

        if (list is null)
        {
            return NotFound("List not found");
        }

        return NoContent();
    }
}
