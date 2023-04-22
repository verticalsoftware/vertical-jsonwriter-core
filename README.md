# vertical-jsonwriter-core

Provides an abstraction over `System.Text.Json.Utf8JsonReader` that handles the structural state of JSON document parsing.

## Overview

Simply put, the `JsonStructureReader` class accepts a `Utf8JsonReader` structure, and forwards node information to a visitor implementation. The node information contains the current depth, parent type, applicable values, property names, etc.

## Quick Start

Add a package reference to your `.csproj` file:

```
$ dotnet add package vertical-jsonwriter-core 
```

## Usage

Define an implementation of `IJsonStructureVisitor`:

```csharp
public class MyJsonStructureVisitor : IJsonStructureVisitor
{
    // Implement methods...

    void VisitObjectStart(in JsonVisitingState state) { /* Implementation */ }    
    void VisitObjectEnd(in JsonVisitingState state, int childCount) { /* Implementation */ }    
    void VisitArrayStart(in JsonVisitingState state) { /* Implementation */ }    
    void VisitArrayEnd(in JsonVisitingState state, int childCount) { /* Implementation */ }
    void VisitPropertyName(in JsonVisitingState state, string name) { /* Implementation */ }
    void VisitStringValue(in JsonVisitingState state, string value) { /* Implementation */ }
    void VisitInt32Value(in JsonVisitingState state, int value) { /* Implementation */ }
    void VisitInt64Value(in JsonVisitingState state, long value) { /* Implementation */ }
    void VisitDoubleValue(in JsonVisitingState state, double value) { /* Implementation */ }
    void VisitDecimalValue(in JsonVisitingState state, decimal value) { /* Implementation */ }
    void VisitBooleanValue(in JsonVisitingState state, bool value) { /* Implementation */ }
    void VisitNull(in JsonVisitingState state) { /* Implementation */ }    
}
```

The `JsonVisitingState` structure contains information regarding the location of the reader that may be of interest:

|Property|Description|
|---|---|
|`Depth`|The current depth level of the reader. Any time the reader descends into an object or array, the depth is incremented.|
|`Index`|The ordinal index of the node within its parent object or array. This property is zero-based.|
|`JsonPlacement`|One of `JsonPlacement` values that describe the location of the current node.|

The `JsonPlacement` enum describes the placement of the current node, relative to its parent:

|Value|Description|
|---|---|
|`RootDocument`|The node is a child of the top-most array or object.|
|`Object`|The node is a child of a JSON object.|
|`Array`|The node is a child of a JSON array.|
|`Property`|The node is the literal value of a property in a JSON object, or a value in a JSON array.|