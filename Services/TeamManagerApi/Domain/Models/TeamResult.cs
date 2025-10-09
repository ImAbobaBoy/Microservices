namespace Domain.Models;

/// <summary>
/// Модель результатов игр между командами
/// </summary>
public class TeamResult
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; } 
    public string Description { get; set; } 
    public DateTime Date { get; set; } 
}