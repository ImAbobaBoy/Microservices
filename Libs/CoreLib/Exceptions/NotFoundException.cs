namespace CoreLib.Exceptions;

/// <summary>
/// Объект не найден
/// </summary>
public class NotFoundException : BaseException.BaseException
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public NotFoundException(string message) : base(message)
    {
    }
}