using Engine.Core.Helper;
using Engine.Core.Serialization;

namespace Engine.Rendering.TypeResolvers;

public class ResourceResolver<T>(IResourceLoader<T> loader) : TypeResolverBase<T>
{
    public override T? Resolve(object raw)
    {
        if (raw == null) return default;
        return raw is string path ? loader.Load(PathHelper.Normalize(path)) : throw new Exception("Invalid type");
    }
}