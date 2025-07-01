using System.Numerics;

namespace Engine.Rendering.Windows;

public interface IWindow
{
    public void SetSize(SizeExpression size);
    public Vector2 GetSize();
    public string Title { get; set; }
}