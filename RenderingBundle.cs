using Engine.Core;
using Engine.Core.Serialization;
using Engine.Rendering.Sprites;
using Engine.Rendering.Textures;
using Engine.Rendering.TypeResolvers;
using GignerEngine.DiContainer;

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
    }

    public void Configure(object c)
    {
        if (c is not RenderingConfig config)
        {
            throw new Exception();
        }
        
        // config.L
    }
}