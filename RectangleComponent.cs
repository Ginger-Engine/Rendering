using Engine.Core;
using System.Numerics;

namespace Engine.Rendering;

public struct RectangleComponent : IComponent
{
    public SizeExpression Size;
    public Vector2 SizeCache;
} 