namespace Vertical.JsonWriter;

/// <summary>
/// Represents an object that receives an ordered sequence of events that occur when a JSON document is read.
/// </summary>
public interface IJsonStructureVisitor
{
    /// <summary>
    /// Called when the start of a JSON object is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    void VisitObjectStart(in JsonVisitingState state);
    
    /// <summary>
    /// Called when the end of a JSON object is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="childCount">The number of child elements read from the object</param>
    void VisitObjectEnd(in JsonVisitingState state, int childCount);
    
    /// <summary>
    /// Called when the start of a JSON array is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    void VisitArrayStart(in JsonVisitingState state);
    
    /// <summary>
    /// Called when the end of a JSON array is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="childCount">The number of child elements read from the array</param>
    void VisitArrayEnd(in JsonVisitingState state, int childCount);
    
    /// <summary>
    /// Called when a property name is encountered. 
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="name">The name of the object property</param>
    void VisitPropertyName(in JsonVisitingState state, string name);
    
    /// <summary>
    /// Called when a string value is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="value">The property value</param>
    void VisitStringValue(in JsonVisitingState state, string value);
    
    /// <summary>
    /// Called when an integer value is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="value">The property value</param>
    void VisitInt32Value(in JsonVisitingState state, int value);
    
    /// <summary>
    /// Called when a long value is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="value">The property value</param>
    void VisitInt64Value(in JsonVisitingState state, long value);
    
    /// <summary>
    /// Called when a double value is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="value">The property value</param>
    void VisitDoubleValue(in JsonVisitingState state, double value);
    
    /// <summary>
    /// Called when a decimal value is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="value">The property value</param>
    void VisitDecimalValue(in JsonVisitingState state, decimal value);
    
    /// <summary>
    /// Called when a boolean value is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    /// <param name="value">The property value</param>
    void VisitBooleanValue(in JsonVisitingState state, bool value);
    
    /// <summary>
    /// Called when a null value is encountered.
    /// </summary>
    /// <param name="state">The current state of the structure reader</param>
    void VisitNull(in JsonVisitingState state);
}