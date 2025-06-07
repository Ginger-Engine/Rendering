using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Core.Transform;

namespace Engine.Rendering.Cameras;

public class CameraBehaviour(ICameraCreator cameraCreator, CameraCollection cameraCollection) : IEntityBehaviour
{
    public void OnStart(Entity entity)
    {
        entity.Modify((ref CameraComponent c) =>
        {
            c.Camera = cameraCreator.Create();
            c.Camera.Layers = c.Layers;
            c.Camera.Zoom = 1;
            cameraCollection.Add(c.Camera);
        });
        entity.SubscribeComponentChange<CameraComponent>((newValue, oldValue) => HandleChanges(entity));
        entity.SubscribeComponentChange<WorldTransformComponent>((newValue, oldValue) => HandleChanges(entity));
    }

    private void HandleChanges(Entity entity)
    {
        var cameraComponent = entity.GetComponent<CameraComponent>();
        var transform = entity.GetComponent<WorldTransformComponent>();
        var camera = cameraComponent.Camera;
        camera.Position = transform.Position;
        camera.Rotation = transform.Rotation;
    }
}