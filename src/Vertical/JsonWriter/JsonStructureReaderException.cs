namespace Vertical.JsonWriter;

/// <summary>
/// Represents a fatal condition that occurs during a read operation.
/// </summary>
public class JsonStructureReaderException : Exception
{
    /// <summary>
    /// Creates a new instance
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception that caused this instance to be thrown</param>
    public JsonStructureReaderException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}