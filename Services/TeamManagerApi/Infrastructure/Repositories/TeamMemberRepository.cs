using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <inheritdoc />
public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly TeamManagerDbContext _context;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TeamMemberRepository(TeamManagerDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task AddAsync(TeamMember member)
    {
        await _context.TeamMembers.AddAsync(member);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TeamMember>> GetByTeamIdAsync(Guid teamId)
    {
        return await _context.TeamMembers
            .Where(m => m.TeamId == teamId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<TeamMember?> GetByIdAsync(Guid id)
    {
        return await _context.TeamMembers.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(Guid id)
    {
        var member = await _context.TeamMembers.FindAsync(id);
        if (member != null)
        {
            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();
        }
    }
    
    /// <inheritdoc />
    public async Task<TeamMember?> GetByUserIdAsync(Guid userId)
    {
        return await _context.TeamMembers.FirstOrDefaultAsync(m => m.UserId == userId);
    }
}