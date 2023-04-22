using System.Text;
using System.Text.Json;
using NSubstitute;
using Xunit;

namespace Vertical.JsonWriter;

public class ReadValueTests
{
    [Fact]
    public void Read_Calls_String_Value()
    {
        var visitor = new { str = "test" }.ConfigureMock();
        visitor.Received(1).VisitStringValue(new JsonVisitingState(1, 0, JsonPlacement.Property), "test");
    }

    [Fact]
    public void Read_Calls_Int32_Value()
    {
        var visitor = new { v = 100 }.ConfigureMock();
        visitor.Received(1).VisitInt32Value(new JsonVisitingState(1, 0, JsonPlacement.Property), 100);
    }

    [Fact]
    public void Read_Calls_Int64_Value()
    {
        var visitor = new { v = long.MaxValue }.ConfigureMock();
        visitor.Received(1).VisitInt64Value(new JsonVisitingState(1, 0, JsonPlacement.Property), long.MaxValue);
    }

    [Fact]
    public void Read_Calls_Double_Value()
    {
        var visitor = new { v = double.MaxValue }.ConfigureMock();
        visitor.Received(1).VisitDoubleValue(new JsonVisitingState(1, 0, JsonPlacement.Property), double.MaxValue);
    }

    [Theory, InlineData(true), InlineData(false)]
    public void Read_Calls_Boolean_Value(bool b)
    {
        var visitor = new { v = b }.ConfigureMock();
        visitor.Received(1).VisitBooleanValue(new JsonVisitingState(1, 0, JsonPlacement.Property), b);
    }

    [Fact]
    public void Read_Calls_Null_Value()
    {
        var visitor = new { v = default(object) }.ConfigureMock();
        visitor.Received(1).VisitNull(new JsonVisitingState(1, 0, JsonPlacement.Property));
    }

    [Fact]
    public void Read_Ignores_Comments()
    {
        // Ensures proper placement of breaks/returns in switch cases
        var json =
            "{/*comment*/'top':'value'/*comment*/,'obj':{'k':'v'},'arr1':[/*comment*/1,2,3], 'arr2':[{/*comment*/'k':'v'},{'k':/*comment*/'v'}],'arr3':[/*comment*/[1,2,3],[1,2,3]]}"
                .Replace('\'', '"');
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json), new JsonReaderOptions
        {
            CommentHandling = JsonCommentHandling.Allow
        });
        var visitor = Substitute.For<IJsonStructureVisitor>();
        JsonStructureReader.Read(ref reader, visitor);
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(0, 0, JsonPlacement.RootDocument));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 1, JsonPlacement.Property));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(2, 0, JsonPlacement.Array));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(2, 1, JsonPlacement.Array));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 5);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 1, JsonPlacement.Property), 1);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(2, 0, JsonPlacement.Array), 1);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(2, 1, JsonPlacement.Array), 1);
    }
}