using Engine.Core.Scenes;

namespace Engine.Rendering;

public class RenderLoopBehaviour(
    RenderQueue renderQueue,
    IEnumerable<IRenderProcessor> processors,
    IRenderBackend renderBackend) : ISceneBehaviour
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
        renderBackend.Start();

        foreach (var renderable in renderQueue.Renderables)
        {
            _processors[renderable.GetType()].Process(renderable);
        }

        renderBackend.End();
    }

    public void OnDestroy()
    {
        renderBackend.Shutdown();
    }
}