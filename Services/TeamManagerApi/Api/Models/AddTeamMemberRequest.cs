namespace Api.Models;

/// <summary>
/// Запрос на добавление игрока в команду
/// </summary>
public class AddTeamMemberRequest
{
    public Guid UserId { get; set; }
    
    public string Role { get; set; } = string.Empty;
}