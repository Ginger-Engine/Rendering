using System.Numerics;
using Engine.Core.Entities;
using Engine.Rendering.Layers;
using Engine.Rendering.Textures;

namespace Engine.Rendering.Sprites;

public class Sprite : IRenderable
{
    public required ITexture Texture;
    public Vector2 Position;
    public Vector2 Scale;
    public Entity Entity { get; set; }
    public Layer Layer { get; set; }
    public float Rotation;
}