using Engine.Rendering.Shaders;

namespace Engine.Rendering.Materials;

public class MaterialCompiler : IMaterialCompiler
{
    private readonly IShaderLoader _shaderLoader;

    public MaterialCompiler(IShaderLoader shaderLoader)
    {
        _shaderLoader = shaderLoader;
    }

    public Material Compile(MaterialInfo info)
    {
        var shader = _shaderLoader.Load(info.ShaderName);
        return new Material
        {
            Shader = shader,
            Parameters = info.Parameters
        };
    }
}