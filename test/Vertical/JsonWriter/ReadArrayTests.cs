using NSubstitute;
using Xunit;

namespace Vertical.JsonWriter;

public class ReadArrayTests
{
    [Fact]
    public void Read_Calls_Root_Array_Start_And_End()
    {
        var visitor = Array.Empty<object>().ConfigureMock();
        visitor.Received(1).VisitArrayStart(new JsonVisitingState(0, 0, JsonPlacement.RootDocument));
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 0);
    }

    [Fact]
    public void Read_Calls_Inner_Array_Start_And_End()
    {
        var visitor = new[] { Array.Empty<object>(), Array.Empty<object>() }.ConfigureMock();
        visitor.Received(1).VisitArrayStart(new JsonVisitingState(1, 0, JsonPlacement.Array));
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(1, 0, JsonPlacement.Array), 0);
        visitor.Received(1).VisitArrayStart(new JsonVisitingState(1, 1, JsonPlacement.Array));
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(1, 1, JsonPlacement.Array), 0);
    }

    [Fact]
    public void Read_Calls_Inner_Values_With_Indices()
    {
        var visitor = new[] { "value1", "value2" }.ConfigureMock();
        visitor.Received(1).VisitStringValue(new JsonVisitingState(1, 0, JsonPlacement.Array), "value1");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(1, 1, JsonPlacement.Array), "value2");
    }

    [Fact]
    public void Read_Calls_Array_End_With_Child_Count_Of_Values()
    {
        var visitor = new[] { "value1", "value2" }.ConfigureMock();
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 2);
    }
    
    [Fact]
    public void Read_Calls_Array_End_With_Child_Count_Of_Arrays()
    {
        var visitor = new[] { Array.Empty<object>(), Array.Empty<object>() }.ConfigureMock();
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 2);
    }
    
    [Fact]
    public void Read_Calls_Array_End_With_Child_Count_Of_Objects()
    {
        var visitor = new[] { new {}, new{} }.ConfigureMock();
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 2);
    }

    [Fact]
    public void Read_Calls_Inner_Objects_With_Indices()
    {
        var visitor = new[] { new { }, new { } }.ConfigureMock();
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 0, JsonPlacement.Array));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 0, JsonPlacement.Array), 0);
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 1, JsonPlacement.Array));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 1, JsonPlacement.Array), 0);
    }

    [Fact]
    public void Read_Calls_Array_Start_And_End_When_In_Array()
    {
        var visitor = new[] { Array.Empty<object>() }.ConfigureMock();
        visitor.Received(1).VisitArrayStart(new JsonVisitingState(1, 0, JsonPlacement.Array));
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(1, 0, JsonPlacement.Array), 0);
    }
    
    [Fact]
    public void Read_Calls_Array_Start_And_End_When_In_Object()
    {
        var visitor = new { array = Array.Empty<object>() }.ConfigureMock();
        visitor.Received(1).VisitArrayStart(new JsonVisitingState(1, 0, JsonPlacement.Property));
        visitor.Received(1).VisitArrayEnd(new JsonVisitingState(1, 0, JsonPlacement.Property), 0);
    }
}