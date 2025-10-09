namespace Domain.Models;

/// <summary>
/// Модель участника команды
/// </summary>
public class TeamMember
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } 
    public string Role { get; set; }
    public Guid TeamId { get; set; } 
}