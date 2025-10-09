namespace Api.Models;

/// <summary>
/// Ответ о команде
/// </summary>
public class TeamResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } 
    
    public string Description { get; set; } 
}