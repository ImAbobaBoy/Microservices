using Dal.Interfaces;
using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

/// <inheritdoc/>
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private IdentityDbContext _context;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RefreshTokenRepository(IdentityDbContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc/>
    public async Task AddAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }
    
    /// <inheritdoc/>
    public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(RefreshToken token)
    {
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync();
    }
}