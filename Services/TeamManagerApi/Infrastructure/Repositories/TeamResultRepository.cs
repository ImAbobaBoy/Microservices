using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <inheritdoc />
public class TeamResultRepository : ITeamResultRepository
{
    private readonly TeamManagerDbContext _context;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TeamResultRepository(TeamManagerDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task AddAsync(TeamResult result)
    {
        await _context.TeamResults.AddAsync(result);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TeamResult>> GetByTeamIdAsync(Guid teamId)
    {
        return await _context.TeamResults
            .Where(r => r.TeamId == teamId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task RemoveAsync(Guid resultId)
    {
        var result = await _context.TeamResults.FindAsync(resultId);
        if (result != null)
        {
            _context.TeamResults.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}