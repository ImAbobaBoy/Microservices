using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// Контекст БД
/// </summary>
public class TeamManagerDbContext : DbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public TeamManagerDbContext(DbContextOptions<TeamManagerDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<TeamMember> TeamMembers { get; set; } = null!;
    public DbSet<TeamResult> TeamResults { get; set; } = null!;
}