namespace Engine.Rendering.Shaders;

public interface IShaderLoader
{
    IShaderProgram Load(string shaderName);
}