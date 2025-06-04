namespace Engine.Rendering.Textures;

public interface ITexture
{
    int Width { get; }
    int Height { get; }
    string Id { get; }
}