using Engine.Core.Stages;

namespace Engine.Rendering;

public class RenderingStage(
    RenderQueue renderQueue,
    IRenderBackend renderBackend,
    CameraCollection cameraCollection) : IStage
{
    public Action<IRenderBackend>? OnAfterRenderEvent;
    public Type[] Before { get; set; } = [];
    public Type[] After { get; set; } = [typeof(LogicStage)];

    public void Start()
    {
        renderBackend.Init();
    }

    public void Update(float dt)
    {
        if (renderQueue.IsEmpty)
        {
            return;
        }
        renderBackend.Start();

        var groupedByLayers = renderQueue.GroupByLayer();
        foreach (var (layer, renderables) in groupedByLayers)
        {
            var cameras = cameraCollection.FindAll(camera => camera.Layers.Contains(layer));
            foreach (var camera in cameras)
            {
                renderBackend.SetCamera(camera);
                foreach (var renderable in renderables)
                {
                    renderBackend.Draw(renderable.Drawable);
                }
                renderBackend.SetCamera(null);
            }
        }
        renderBackend.SetCamera(cameraCollection[0]);
        OnAfterRenderEvent?.Invoke(renderBackend);
        renderBackend.SetCamera(null);
        // renderQueue.Renderables.Clear();
        renderBackend.End();   
    }
}