namespace Api.Models;

/// <summary>
/// Запрос на добавление результата
/// </summary>
public class AddTeamResultRequest
{
    public string Description { get; set; } 
    
    public int Score { get; set; }
}
