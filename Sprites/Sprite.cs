using System.Numerics;
using Engine.Rendering.Layers;
using Engine.Rendering.Textures;

namespace Engine.Rendering.Sprites;

public class Sprite : IRenderable
{
    public required ITexture Texture;
    public Vector2 Position;
    public Layer Layer { get; set; }
}