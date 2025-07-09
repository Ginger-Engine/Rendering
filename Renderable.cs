using Engine.Core.Entities;
using Engine.Rendering.Drawables;
using Engine.Rendering.Layers;

namespace Engine.Rendering;

public class Renderable
{
    public Entity Entity;
    public Layer Layer;
    public IDrawable Drawable;
}