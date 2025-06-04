namespace Engine.Rendering.Shaders;

public interface IShaderProgram
{
    void Use();
    void SetUniform(string name, object value);
}