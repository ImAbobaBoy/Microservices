namespace Dal.Models;

/// <summary>
/// Модель пользователя
/// </summary>
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!; 
    public string? VkLink { get; set; }
    public string? TgLink { get; set; }
    public string? Description { get; set; }
    public string? SkillLevel { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public Guid TeamId { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
