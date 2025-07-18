using System.Drawing;
using Engine.Core.Serialization;

namespace Engine.Rendering.TypeResolvers;

public class ColorTypeResolver : ITypeResolver<Color>
{
    public object Resolve(Type type, object raw)
    {
        if (raw is string rawStr) return ColorTranslator.FromHtml(rawStr);
        return Color.White;
    }
}