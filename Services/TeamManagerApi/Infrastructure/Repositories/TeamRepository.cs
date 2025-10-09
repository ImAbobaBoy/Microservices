using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <inheritdoc />
public class TeamRepository : ITeamRepository
{
    private readonly TeamManagerDbContext _context;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="context"></param>
    public TeamRepository(TeamManagerDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Team?> GetByIdAsync(Guid id)
    {
        return await _context.Teams.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Team team)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
    }
}