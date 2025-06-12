using Engine.Core;
using Engine.Rendering.Layers;

namespace Engine.Rendering.Cameras;

public struct CameraComponent : IComponent
{
    public ICamera Camera;
    public Layer[] Layers;
    public ICamera.Type CameraType = ICamera.Type.Orthographic;

    public CameraComponent()
    {
        Camera = null;
        Layers = new Layer[] { };
    }
}