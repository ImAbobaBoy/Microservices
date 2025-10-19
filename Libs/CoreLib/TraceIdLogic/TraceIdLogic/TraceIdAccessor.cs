using TraceIdLogic.Interfaces;

namespace TraceIdLogic;

/// <summary>
/// Реализация доступа к TraceId в рамках одного scoped.
/// Объединяет чтение (ITraceReader) и запись (ITraceWriter) трассировочного идентификатора.
/// </summary>
internal class TraceIdAccessor : ITraceWriter, ITraceReader, ITraceIdAccessor
{
    /// <summary>
    /// Имя заголовка/свойства трассировки.
    /// </summary>
    public string Name => "TraceId";

    private string _value;

    /// <summary>
    /// Получить текущее значение TraceId.
    /// </summary>
    public string GetValue()
    {
        return _value;
    }

    /// <summary>
    /// Установить новое значение TraceId.
    /// Если значение null или пустое, генерируется новый Guid.
    /// </summary>
    public void WriteValue(string value)
    {
        // на случай если это первый в цепочке сервис и до этого не было traceId
        if (string.IsNullOrWhiteSpace(value))
        {
            value = Guid.NewGuid().ToString();
        }

        _value = value;
    }
}
