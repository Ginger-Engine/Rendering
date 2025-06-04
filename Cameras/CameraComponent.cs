using Engine.Core;
using Engine.Rendering.Layers;

namespace Engine.Rendering.Cameras;

public struct CameraComponent : IComponent
{
    public ICamera Camera;
    public Layer[] Layers;
}