namespace Logic.Models;

/// <summary>
/// Модель пользователя для передачи
/// </summary>
public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? VkLink { get; set; }
    public string? TgLink { get; set; }
    public string? Description { get; set; }
    public string? SkillLevel { get; set; }
}