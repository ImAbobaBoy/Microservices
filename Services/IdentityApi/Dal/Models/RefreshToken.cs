namespace Dal.Models;

/// <summary>
/// Модель рефреш токена
/// </summary>
public class RefreshToken
{
    public Guid Id { get; set; }
    public string TokenHash { get; set; } = null!;
    public DateTimeOffset Expires { get; set; }
    public DateTimeOffset Created { get; set; }
    public string? CreatedByIp { get; set; }
    public DateTimeOffset? Revoked { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReplacedByTokenHash { get; set; }
    public bool IsActive => Revoked == null && DateTimeOffset.UtcNow < Expires;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
