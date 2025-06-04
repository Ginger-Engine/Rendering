namespace Engine.Rendering.Materials;

public interface IMaterialCompiler
{
    Material Compile(MaterialInfo info);
}