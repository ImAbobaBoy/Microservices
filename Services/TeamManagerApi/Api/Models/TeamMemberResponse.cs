namespace Api.Models;

/// <summary>
/// Ответ об игроке
/// </summary>
public class TeamMemberResponse
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public string Role { get; set; }
}