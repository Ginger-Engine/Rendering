using Engine.Core;
using Engine.Rendering.Textures;

namespace Engine.Rendering.Sprites;

public struct SpriteComponent : IComponent
{
    public ITexture Texture;
    public int Width;
    public int Height;
}