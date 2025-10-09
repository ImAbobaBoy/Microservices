namespace Api.Models;

/// <summary>
/// Запрос на создание команды
/// </summary>
public class CreateTeamRequest
{
    public string Name { get; set; }
    
    public string Description { get; set; }
}