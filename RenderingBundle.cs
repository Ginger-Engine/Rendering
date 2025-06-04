using Engine.Core;
using Engine.Core.Serialization;
using Engine.Rendering.Cameras;
using Engine.Rendering.Layers;
using Engine.Rendering.Sprites;
using Engine.Rendering.Textures;
using Engine.Rendering.TypeResolvers;
using GignerEngine.DiContainer;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Engine.Rendering;

public class RenderingBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<RenderLoopBehaviour>();
        builder.Bind<ITypeResolver<ITexture>>().From<ResourceResolver<ITexture>>();
        builder.Bind<RenderQueue>();
        builder.Bind<SpriteRenderer>();
        builder.Bind<SpriteRenderProcessor>();
        builder.Bind<LayerManager>();
        builder.Bind<ITypeResolver<Layer>>().From<LayerLoader>();
        builder.Bind<CameraCollection>();
        builder.Bind<CameraBehaviour>();
    }

    public void Configure(string c, IReadonlyDiContainer diContainer)
    {
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var config = deserializer.Deserialize<RenderingConfig>(c);
        
        var layerManager = diContainer.Resolve<LayerManager>();
        
        foreach (var layer in config.layers)
        {
            layerManager.RegisterLayer(layer);
        }
    }
}