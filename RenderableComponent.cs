using Engine.Core;
using Engine.Rendering.Layers;
using Engine.Rendering.Materials;

namespace Engine.Rendering;

public struct RenderableComponent : IComponent
{
    public Renderable Renderable { get; set; }
    public Layer Layer;
    public Material Material;
}