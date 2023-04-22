using System.Text.Json;

namespace Vertical.JsonWriter;

/// <summary>
/// Reads <see cref="Utf8JsonReader"/> data, and makes calls to the provided visitor.
/// </summary>
public static class JsonStructureReader
{
    /// <summary>
    /// Reads the given JSON data, and sends ordered notifications to the provided visitor.
    /// </summary>
    /// <param name="reader"><see cref="Utf8JsonReader"/> that contains the JSON object to read.</param>
    /// <param name="visitor">An object that receives node data.</param>
    public static void Read(ref Utf8JsonReader reader, IJsonStructureVisitor visitor)
    {
        WalkRoot(ref reader, visitor);
    }

    private static void WalkRoot(ref Utf8JsonReader reader, IJsonStructureVisitor visitor)
    {
        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    WalkJsonObject(ref reader, visitor, new JsonVisitingState(0, 0, JsonPlacement.RootDocument));
                    return;

                case JsonTokenType.StartArray:
                    WalkJsonArray(ref reader, visitor, new JsonVisitingState(0, 0, JsonPlacement.RootDocument));
                    return;
            }
        }
    }

    private static void WalkJsonObject(
        ref Utf8JsonReader reader,
        IJsonStructureVisitor visitor,
        in JsonVisitingState state)
    {
        visitor.VisitObjectStart(state);
        var count = 0;

        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.PropertyName:
                    WalkJsonProperty(
                        ref reader,
                        visitor,
                        new JsonVisitingState(state.Depth + 1, count++, JsonPlacement.Object));
                    break;

                case JsonTokenType.EndObject:
                    visitor.VisitObjectEnd(state, count);
                    return;
            }
        }
    }

    private static void WalkJsonArray(
        ref Utf8JsonReader reader,
        IJsonStructureVisitor visitor,
        in JsonVisitingState state)
    {
        visitor.VisitArrayStart(state);
        var count = 0;

        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    WalkJsonObject(
                        ref reader, 
                        visitor,
                        new JsonVisitingState(state.Depth + 1, count++, JsonPlacement.Array));
                    break;

                case JsonTokenType.StartArray:
                    WalkJsonArray(
                        ref reader, 
                        visitor,
                        new JsonVisitingState(state.Depth + 1, count++, JsonPlacement.Array));
                    break;

                case JsonTokenType.EndArray:
                    visitor.VisitArrayEnd(state, count);
                    return;

                default:
                    EvaluateTokenType(ref reader, visitor,
                        new JsonVisitingState(state.Depth + 1, count++, JsonPlacement.Array));
                    break;
            }
        }
    }

    private static void WalkJsonProperty(
        ref Utf8JsonReader reader,
        IJsonStructureVisitor visitor,
        in JsonVisitingState state)
    {
        visitor.VisitPropertyName(state, reader.GetString()!);
        if (!reader.Read()) throw ExceptionHelper.PropertyNameExpected(ref reader);
        EvaluateTokenType(ref reader, visitor, state with { Placement = JsonPlacement.Property });
    }

    private static void EvaluateTokenType(
        ref Utf8JsonReader reader,
        IJsonStructureVisitor visitor,
        in JsonVisitingState state)
    {
        do
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    WalkJsonObject(ref reader, visitor, state);
                    return;

                case JsonTokenType.StartArray:
                    WalkJsonArray(ref reader, visitor, state);
                    return;

                case JsonTokenType.String:
                    visitor.VisitStringValue(
                        state,
                        reader.GetString() ?? throw ExceptionHelper.StringValueExpected(ref reader));
                    return;

                case JsonTokenType.Number when reader.TryGetInt32(out var i):
                    visitor.VisitInt32Value(state, i);
                    return;

                case JsonTokenType.Number when reader.TryGetInt64(out var l):
                    visitor.VisitInt64Value(state, l);
                    return;

                case JsonTokenType.Number when reader.TryGetDouble(out var d):
                    visitor.VisitDoubleValue(state, d);
                    return;

                case JsonTokenType.Number when reader.TryGetDecimal(out var m):
                    visitor.VisitDecimalValue(state, m);
                    return;

                case JsonTokenType.True:
                    visitor.VisitBooleanValue(state, true);
                    return;

                case JsonTokenType.False:
                    visitor.VisitBooleanValue(state, false);
                    return;

                case JsonTokenType.Null:
                    visitor.VisitNull(state);
                    return;
            }
        } while (reader.Read());

        throw ExceptionHelper.UnexpectedEndOfDocument();
    }
}