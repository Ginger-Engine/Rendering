using Engine.Core;
using Engine.Core.Serialization;
using Engine.Rendering.Layers;
using Engine.Rendering.Textures;
using Engine.Rendering.TypeResolvers;
using Engine.Rendering.Ui;
using Engine.Rendering.Windows;
using GignerEngine.DiContainer;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Engine.Rendering;

public class RenderingBundle : IBundle
{
    public void InstallBindings(DiBuilder builder)
    {
        builder.Bind<ITypeResolver<ITexture>>().From<ResourceResolver<ITexture>>();
        builder.Bind<RenderQueue>();
        builder.Bind<LayerManager>();
        builder.Bind<ITypeResolver<Layer>>().From<LayerLoader>();
        builder.Bind<CameraCollection>();
        builder.Bind<RenderingStage>();
    }

    public void Configure(string c, IReadonlyDiContainer diContainer)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var config = deserializer.Deserialize<RenderingConfig>(c);
        
        var layerManager = diContainer.Resolve<LayerManager>();
        
        foreach (var layer in config.Layers)
        {
            layerManager.RegisterLayer(layer);
        }

        var fontManager = diContainer.Resolve<IFontManager>();
        foreach (var fontDescription in config.Fonts)
        {
            fontManager.Register(fontDescription);
        }
        
        var window = diContainer.Resolve<IWindow>();
        window.SetSize(config.WindowSize);
        window.Title = config.WindowTitle;
    }
}