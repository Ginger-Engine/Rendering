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
        entity.SubscribeComponentChange<CameraComponent>(e => e.newValue.Camera.CameraType = e.newValue.CameraType);
        entity.SubscribeComponentChange<TransformComponent>(e =>
        {
            var cameraComponent = entity.GetComponent<CameraComponent>();
            cameraComponent.Camera.Position = e.newValue.WorldTransform.Position;
            cameraComponent.Camera.Rotation = e.newValue.WorldTransform.Rotation;
        });
    }
}