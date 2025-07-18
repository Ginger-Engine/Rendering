using System.Numerics;

namespace Engine.Rendering;

public struct Rectangle
{
    public Vector2 Position;
    public Vector2 Size;
    
    public Vector2 RightBottomPosition => Position + Size;
    public Vector2 Center => RightBottomPosition / 2f;
}