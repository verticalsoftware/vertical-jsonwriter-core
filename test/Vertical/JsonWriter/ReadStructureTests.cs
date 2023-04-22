using NSubstitute;
using Xunit;

namespace Vertical.JsonWriter;

public class ReadStructureTests
{
    [Fact]
    public void Read_Invokes_Methods_With_Correct_Structural_Values()
    {
        var obj = new
        {
            prop0 = "prop0",
            prop1 = "prop1",
            prop2 = new
            {
                prop20 = "prop20",
                prop21 = "prop21"
            },
            prop3 = new
            {
                prop30 = new
                {
                    prop300 = "prop300",
                    prop301 = "prop301"
                },
                prop31 = new[]
                {
                    "prop310",
                    "prop311"
                },
                prop32 = new
                {
                    prop320 = new[]
                    {
                        "prop3200",
                        "prop3201"
                    },
                    prop321 = new
                    {
                        prop3210 = "prop3210",
                        prop3211 = "prop3211"
                    }
                }
            },
            prop4 = new[]
            {
                "prop40",
                "prop41"
            },
            prop5 = new[]
            {
                new
                {
                    prop5x0 = "prop5x0",
                    prop5x1 = "prop5x1"
                },
                new
                {
                    prop5x0 = "prop5x0",
                    prop5x1 = "prop5x1"
                }
            },
            prop6 = new[]
            {
                new[] { 0, 1 },
                new[] { 0, 1 }
            }
        };

        var visitor = obj.ConfigureMock();
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(0, 0, JsonPlacement.RootDocument));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 2, JsonPlacement.Property));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(1, 3, JsonPlacement.Property));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(2, 0, JsonPlacement.Property));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(2, 2, JsonPlacement.Property));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(3, 1, JsonPlacement.Property));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(2, 0, JsonPlacement.Array));
        visitor.Received(1).VisitObjectStart(new JsonVisitingState(2, 1, JsonPlacement.Array));
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(0, 0, JsonPlacement.RootDocument), 7);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(2, 1, JsonPlacement.Array), 2);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(2, 0, JsonPlacement.Array), 2);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 3, JsonPlacement.Property), 3);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(2, 2, JsonPlacement.Property), 2);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(3, 1, JsonPlacement.Property), 2);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(2, 0, JsonPlacement.Property), 2);
        visitor.Received(1).VisitObjectEnd(new JsonVisitingState(1, 2, JsonPlacement.Property), 2);
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 0, JsonPlacement.Object), "prop0");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 1, JsonPlacement.Object), "prop1");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 2, JsonPlacement.Object), "prop2");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(2, 0, JsonPlacement.Object), "prop20");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(2, 1, JsonPlacement.Object), "prop21");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 3, JsonPlacement.Object), "prop3");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(2, 0, JsonPlacement.Object), "prop30");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(3, 0, JsonPlacement.Object), "prop300");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(3, 1, JsonPlacement.Object), "prop301");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(2, 1, JsonPlacement.Object), "prop31");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(2, 2, JsonPlacement.Object), "prop32");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(3, 0, JsonPlacement.Object), "prop320");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(3, 1, JsonPlacement.Object), "prop321");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(4, 0, JsonPlacement.Object), "prop3210");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(4, 1, JsonPlacement.Object), "prop3211");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 4, JsonPlacement.Object), "prop4");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 5, JsonPlacement.Object), "prop5");
        visitor.Received(2).VisitPropertyName(new JsonVisitingState(3, 0, JsonPlacement.Object), "prop5x0");
        visitor.Received(2).VisitPropertyName(new JsonVisitingState(3, 1, JsonPlacement.Object), "prop5x1");
        visitor.Received(1).VisitPropertyName(new JsonVisitingState(1, 6, JsonPlacement.Object), "prop6");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(1, 0, JsonPlacement.Property), "prop0");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(1, 1, JsonPlacement.Property), "prop1");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(2, 0, JsonPlacement.Property), "prop20");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(2, 1, JsonPlacement.Property), "prop21");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(3, 0, JsonPlacement.Property), "prop300");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(3, 1, JsonPlacement.Property), "prop301");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(3, 0, JsonPlacement.Array), "prop310");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(3, 1, JsonPlacement.Array), "prop311");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(4, 0, JsonPlacement.Array), "prop3200");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(4, 1, JsonPlacement.Array), "prop3201");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(4, 0, JsonPlacement.Property), "prop3210");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(4, 1, JsonPlacement.Property), "prop3211");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(2, 0, JsonPlacement.Array), "prop40");
        visitor.Received(1).VisitStringValue(new JsonVisitingState(2, 1, JsonPlacement.Array), "prop41");
        visitor.Received(2).VisitStringValue(new JsonVisitingState(3, 0, JsonPlacement.Property), "prop5x0");
        visitor.Received(2).VisitStringValue(new JsonVisitingState(3, 1, JsonPlacement.Property), "prop5x1");
    }
}