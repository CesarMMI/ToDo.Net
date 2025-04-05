using api.Dtos.List;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/lists")]
[ApiController]
public class ListController : ControllerBase
{
    private readonly IListService _listService;

    public ListController(IListService listService)
    {
        _listService = listService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _listService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _listService.GetById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateListDto request)
    {
        var result = await _listService.Create(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateListDto request)
    {
        var result = await _listService.Update(id, request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _listService.Delete(id);
        return NoContent();
    }
}
