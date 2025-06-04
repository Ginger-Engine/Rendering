using Engine.Core.Serialization;

namespace Engine.Rendering.Layers;

public class LayerLoader : TypeResolverBase<Layer>, IResourceLoader<Layer>
{
    private readonly LayerManager _manager;

    public LayerLoader(LayerManager manager)
    {
        _manager = manager;
    }
    
    public Layer Load(string path)
    {
        return _manager.Layers.First(layer => layer.Name == path);
    }

    public override Layer? Resolve(object raw)
    {
        return raw is string path ? Load(path) : throw new Exception("Invalid type");
    }
}