using Microsoft.EntityFrameworkCore;

namespace Dal.Models;

/// <summary>
/// Контекст базы данных
/// </summary>
public class IdentityDbContext : DbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) 
        : base(options) { }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.Username).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.CreatedAt).IsRequired();
            entity.Property(u => u.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt => rt.Id);
            entity.Property(rt => rt.TokenHash).IsRequired();
            entity.Property(rt => rt.Expires).IsRequired();
            entity.Property(rt => rt.Created).IsRequired();

            entity.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

}

