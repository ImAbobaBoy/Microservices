namespace CoreLib.Exceptions.BaseException;

/// <summary>
/// Базовый класс ошибок
/// От него наследуются все кастомные ошибки
/// </summary>
public class BaseException : Exception
{
    public BaseException(string message) : base(message)
    {
    }
}