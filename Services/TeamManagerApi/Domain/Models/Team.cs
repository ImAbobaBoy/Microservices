namespace Domain.Models;

/// <summary>
/// Модель команды
/// </summary>
public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; } 
    public bool IsDeleted { get; set; }

    public List<TeamMember> Members { get; set; } 
    public List<TeamResult> Results { get; set; }
}