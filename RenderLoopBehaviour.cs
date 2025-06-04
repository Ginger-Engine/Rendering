using Engine.Core.Scenes;
using Engine.Rendering.Cameras;
using Engine.Rendering.Layers;

namespace Engine.Rendering;

public class RenderLoopBehaviour(
    RenderQueue renderQueue,
    IEnumerable<IRenderProcessor> processors,
    IRenderBackend renderBackend,
    CameraCollection cameraCollection) : ISceneBehaviour
{
    private Dictionary<Type, IRenderProcessor> _processors = processors.ToDictionary(
        processor => processor
            .GetType()
            .GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRenderProcessor<>))
            .GetGenericArguments()[0],
        processor => processor
    );

    public void OnStart()
    {
        renderBackend.Init();
    }

    public void OnUpdate(float dt)
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

    public void OnDestroy()
    {
        renderBackend.Shutdown();
    }
}