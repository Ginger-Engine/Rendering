using Engine.Core;
using Engine.Rendering.Materials;

namespace Engine.Rendering;

public struct RenderableComponent : IComponent
{
    public IRenderable Renderable { get; set; }
    public Layer Layer {get; set;}
    public Material Material {get; set;}
}