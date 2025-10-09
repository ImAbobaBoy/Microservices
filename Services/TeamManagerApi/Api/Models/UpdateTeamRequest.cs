namespace Api.Models;

/// <summary>
/// Запрос на обновление команды
/// </summary>
public class UpdateTeamRequest
{
    public string Name { get; set; } 
    
    public string Description { get; set; } 
}