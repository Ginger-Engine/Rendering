using Engine.Core.Entities;
using Engine.Rendering.Layers;

namespace Engine.Rendering;

public interface IRenderable
{
    public Entity Entity { get; }
    Layer Layer { get; }
}