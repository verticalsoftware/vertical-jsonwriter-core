using System.Text.Json;
using NSubstitute;

namespace Vertical.JsonWriter;

public static class Helpers
{
    public static IJsonStructureVisitor ConfigureMock<T>(this T obj)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
        var reader = new Utf8JsonReader(bytes);
        var mock = Substitute.For<IJsonStructureVisitor>();
        JsonStructureReader.Read(ref reader, mock);
        return mock;
    }
}