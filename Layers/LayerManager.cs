namespace Engine.Rendering.Layers;

public class LayerManager
{
    public IReadOnlyList<Layer> Layers => _layers;
    private readonly List<Layer> _layers = [];

    public void RegisterLayer(string name)
    {
        _layers.Add(new Layer { Name = name, Order = _layers.Count });
    }
}