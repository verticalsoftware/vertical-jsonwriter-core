using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Vertical.JsonWriter;

[ExcludeFromCodeCoverage]
internal static class ExceptionHelper
{
    public static Exception PropertyNameExpected(ref Utf8JsonReader reader)
    {
        var msg = $"Expected a string value immediately after reading a PropertyName token.{FormatReaderInfo(ref reader)}";
        return new JsonStructureReaderException(msg);
    }

    public static Exception StringValueExpected(ref Utf8JsonReader reader)
    {
        var msg = $"Expected a non-null string value after reading a String token.{FormatReaderInfo(ref reader)}";
        return new JsonStructureReaderException(msg);
    }

    private static string FormatReaderInfo(ref Utf8JsonReader reader)
    {
        return
            Environment.NewLine +
            $"Reader position: {reader.Position}" +
            $"{Environment.NewLine}Consumed: {reader.BytesConsumed}" +
            $"{Environment.NewLine}Depth: {reader.CurrentDepth}" +
            $"{Environment.NewLine}Current token: {reader.TokenType}" +
            $"{Environment.NewLine}Reader state: {reader.CurrentState}";
    }

    public static Exception UnexpectedEndOfDocument()
    {
        return new JsonStructureReaderException("Unexpected reached the end of the document.");
    }
}