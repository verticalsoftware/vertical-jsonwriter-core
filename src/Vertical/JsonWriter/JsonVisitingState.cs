namespace Vertical.JsonWriter;

/// <summary>
/// Describes the placement of the current node with a JSON document.
/// </summary>
/// <param name="Depth">Gets the nested depth of the current node.</param>
/// <param name="Index">Gets the zero-based ordinal position of the node within its parent.</param>
/// <param name="Placement">Gets the type of node the current item belongs to.</param>
public readonly record struct JsonVisitingState(int Depth, int Index, JsonPlacement Placement);