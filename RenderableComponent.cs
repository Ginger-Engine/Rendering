using Engine.Core;
using Engine.Core.Entities;
using Engine.Rendering.Layers;
using Engine.Rendering.Materials;

namespace Engine.Rendering;

public struct RenderableComponent : IComponent
{
    public IRenderable Renderable { get; set; }
    public Layer Layer;
    public Material Material;
}