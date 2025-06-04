using Engine.Rendering.Shaders;

namespace Engine.Rendering.Materials;

public class Material
{
    public required IShaderProgram Shader;
    public Dictionary<string, object> Parameters = new();
}