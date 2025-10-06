namespace CoreLib.Exceptions;

/// <summary>
/// Неверный токен
/// </summary>
public class InvalidTokenException : BaseException.BaseException
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public InvalidTokenException(string message) : base(message)
    {
    }
}