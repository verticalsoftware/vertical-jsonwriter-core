namespace Vertical.JsonWriter;

/// <summary>
/// Represents the type of node the current item is a child of.
/// </summary>
public enum JsonPlacement
{
    /// <summary>
    /// The node is a child of the top-most document element.
    /// </summary>
    RootDocument,
    
    /// <summary>
    /// The node is a child of a JSON object.
    /// </summary>
    Object,
    
    /// <summary>
    /// The node is a child of a JSON array.
    /// </summary>
    Array,
    
    /// <summary>
    /// The node is the value of a property within a JSON object.
    /// </summary>
    Property
}