using NSubstitute;
using Xunit;

namespace Vertical.JsonWriter;

public class ReadObjectTests
{
    [Fact]
    public void Read_Calls_Root_Start_And_End()
    {
        var visitor = new { }.ConfigureMock();
        visitor.Received().VisitObjectStart(new JsonVisitingState(0, 0, JsonPlacement.RootDocument));
        visitor.Received().VisitObjectEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 0);
    }

    [Fact]
    public void Read_Calls_Inner_Object_Start_And_End()
    {
        var visitor = new { Inner = new { } }.ConfigureMock();
        visitor.Received().VisitObjectStart(new JsonVisitingState(1, 0, JsonPlacement.Property));
        visitor.Received().VisitObjectEnd(new JsonVisitingState(1, 0, JsonPlacement.Property), 0);
    }

    [Fact]
    public void Read_Calls_Inner_Array_Object_Start_And_End()
    {
        var visitor = new[] { new { } }.ConfigureMock();
        visitor.Received().VisitObjectStart(new JsonVisitingState(1, 0, JsonPlacement.Array));
        visitor.Received().VisitObjectEnd(new JsonVisitingState(1, 0, JsonPlacement.Array), 0);
    }

    [Fact]
    public void Read_Calls_Object_Start_And_End_From_Array_With_Indices()
    {
        var visitor = new[] { new { }, new { } }.ConfigureMock();
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 0, JsonPlacement.Array));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 0, JsonPlacement.Array), 0);
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 1, JsonPlacement.Array));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 1, JsonPlacement.Array), 0);
    }

    [Fact]
    public void Read_Calls_Object_Start_And_End_From_Object_With_Indices()
    {
        var visitor = new { obj1 = new { }, obj2 = new { } }.ConfigureMock();
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 0, JsonPlacement.Property));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 1, JsonPlacement.Property));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 0, JsonPlacement.Property), 0);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 1, JsonPlacement.Property), 0);
    }

    [Fact]
    public void Read_Calls_End_Object_With_Child_Count()
    {
        var visitor = new { prop = "", prop2 = "" }.ConfigureMock();
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 2);
    }

    [Fact]
    public void Read_Calls_Properties_Names()
    {
        var visitor = new { prop1 = "", prop2 = "" }.ConfigureMock();
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 0, JsonPlacement.Object), "prop1");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 1, JsonPlacement.Object), "prop2");
    }

    [Fact]
    public void Read_Calls_Object_Start_And_End_When_In_Array()
    {
        var visitor = new[] { new { } }.ConfigureMock();
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 0, JsonPlacement.Array));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 0, JsonPlacement.Array), 0);
    }
    
    [Fact]
    public void Read_Calls_Object_Start_And_End_When_In_Object()
    {
        var visitor = new { prop = new{} }.ConfigureMock();
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 0, JsonPlacement.Property));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 0, JsonPlacement.Property), 0);
    }
}