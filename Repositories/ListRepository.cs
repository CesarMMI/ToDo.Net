using api.Data;
using api.Dtos.List;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class ListRepository : IListRepository
{
    private readonly AppDbContext _context;
    public ListRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<List>> GetAllAsync()
    {
        return await _context.Lists.ToListAsync();
    }

    public async Task<List?> GetByIdAsync(int id)
    {
        return await _context.Lists.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List> CreateAsync(CreateListDto listDto)
    {
        var list = new List
        {
            Name = listDto.Name,
            Description = listDto.Description,
        };

        await _context.Lists.AddAsync(list);
        await _context.SaveChangesAsync();

        return list;
    }

    public async Task<List?> UpdateAsync(int id, UpdateListDto listDto)
    {
        var list = await GetByIdAsync(id);

        if (list is null)
        {
            return null;
        }

        list.Name = listDto.Name;
        list.Description = listDto.Description;
        list.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return list;
    }

    public async Task<List?> DeleteAsync(int id)
    {
        var list = await GetByIdAsync(id);

        if (list is null)
        {
            return null;
        }

        _context.Lists.Remove(list);
        await _context.SaveChangesAsync();

        return list;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await GetByIdAsync(id) is not null;
    }
}
