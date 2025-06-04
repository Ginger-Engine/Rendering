using Engine.Rendering.Layers;

namespace Engine.Rendering;

public interface IRenderable
{
    Layer Layer { get; }
}