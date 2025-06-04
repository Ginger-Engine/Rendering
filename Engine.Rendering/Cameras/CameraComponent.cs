using Engine.Core;

namespace Engine.Rendering.Cameras;

public struct CameraComponent : IComponent
{
    public ICamera Camera;
    public Layer Layer;
}