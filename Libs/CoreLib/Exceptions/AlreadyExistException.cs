namespace CoreLib.Exceptions;

/// <summary>
/// Объект уже существует
/// </summary>
public class AlreadyExistException : BaseException.BaseException
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public AlreadyExistException(string message) : base(message)
    {
    }
}