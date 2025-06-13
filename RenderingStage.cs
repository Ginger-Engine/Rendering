using Engine.Core.Stages;

namespace Engine.Rendering;

public class RenderingStage(
    RenderQueue renderQueue,
    IEnumerable<IRenderProcessor> processors,
    IRenderBackend renderBackend,
    CameraCollection cameraCollection) : IStage
{
    public Type[] Before { get; set; } = [];
    public Type[] After { get; set; } = [typeof(LogicStage)];
 
    private Dictionary<Type, IRenderProcessor> _processors = processors.ToDictionary(
        processor => processor
            .GetType()
            .GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRenderProcessor<>))
            .GetGenericArguments()[0],
        processor => processor
    );

    public void Start()
    {
        renderBackend.Init();
    }

    public void Update(float dt)
    {
        if (renderQueue.Renderables.Count == 0)
        {
            return;
        }
        renderBackend.Start();

        var groupedByLayers = renderQueue.Renderables.GroupBy(r => r.Layer).OrderBy(grouping =>  grouping.Key.Order);
        foreach (var group in groupedByLayers)
        {
            var layer = group.Key;
            var renderables = group.ToList();
            
            var cameras = cameraCollection.FindAll(camera => camera.Layers.Contains(layer));
            foreach (var camera in cameras)
            {
                renderBackend.SetCamera(camera);
                foreach (var renderable in renderables)
                {
                    _processors[renderable.GetType()].Process(renderable);
                }
                renderBackend.SetCamera(null);
            }
        }
        renderQueue.Renderables.Clear();

        renderBackend.End();   
    }
}