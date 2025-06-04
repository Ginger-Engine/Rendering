using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Core.Transform;

namespace Engine.Rendering.Sprites;

public class SpriteRenderer : IEntityBehaviour
{
    private readonly RenderQueue _renderQueue;

    public SpriteRenderer(RenderQueue renderQueue)
    {
        _renderQueue = renderQueue;
    }

    public void OnStart(Entity entity)
    {
        UpdateRenderable(entity);
        entity.SubscribeComponentChange<SpriteComponent>((value, oldValue) =>
        {
            UpdateRenderable(entity);
        });
        entity.SubscribeComponentChange<TransformComponent>((value, oldValue) =>
        {
            UpdateRenderable(entity);
        });
    }

    public void OnUpdate(Entity entity, float dt)
    {
        _renderQueue.Add(entity.GetComponent<RenderableComponent>().Renderable);
    }

    public void UpdateRenderable(Entity entity)
    {
        var renderableComponent = entity.GetComponent<RenderableComponent>();
        
        var spriteComponent = entity.GetComponent<SpriteComponent>();
        var transformComponent = entity.GetComponent<WorldTransformComponent>();

        renderableComponent.Renderable = new Sprite
        {
            Texture = spriteComponent.Texture,
            Position = transformComponent.Position,
        };
        
        entity.ApplyComponent(renderableComponent);
    }
}