using Engine.Rendering.Drawables;
using Engine.Rendering.Layers;

namespace Engine.Rendering;

public class RenderQueue
{
    public HashSet<Renderable> Renderables = [];
    public bool IsEmpty => Renderables.Count == 0;

    private bool _isDirty;
    private Dictionary<Layer, Renderable[]> _groupedCache = [];
    
    public Dictionary<Layer, Renderable[]> GroupByLayer()
    {
        if (_isDirty)
        {
            _groupedCache = Renderables.GroupBy(r => r.Layer).OrderBy(grouping => grouping.Key.Order)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());
            _isDirty = false;
        }
        
        return _groupedCache;
    }

    public void Add(Renderable renderable)
    {
        Renderables.Add(renderable);
        _isDirty = true;
    }

    public void Remove(Renderable renderable)
    {
        Renderables.Remove(renderable);
        _isDirty = true;
    }
}