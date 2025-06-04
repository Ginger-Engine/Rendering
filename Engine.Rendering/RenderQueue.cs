namespace Engine.Rendering;

public class RenderQueue
{
    public IList<IRenderable> Renderables = [];

    public void Add(IRenderable renderable)
    {
        Renderables.Add(renderable);
    }

    public void Clear()
    {
        Renderables.Clear();
    }
}