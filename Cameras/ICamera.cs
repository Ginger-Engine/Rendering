using System.Numerics;
using Engine.Rendering.Layers;

namespace Engine.Rendering.Cameras;

public interface ICamera
{
    public enum Type
    {
        Orthographic,
        ScreenSpace,
    }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Zoom { get; set; }
    public Layer[] Layers { get; set; }
    public Type CameraType { get; set; }
}