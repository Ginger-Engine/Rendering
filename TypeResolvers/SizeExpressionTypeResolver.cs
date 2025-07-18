using Engine.Core.Serialization;
using Engine.Rendering;

namespace Engine.Rendering.TypeResolvers;

public class SizeExpressionTypeResolver : TypeResolverBase<SizeExpression>
{
    public override SizeExpression Resolve(object raw)
    {
        if (raw is Dictionary<object, object> dict)
        {
            return new SizeExpression
            {
                X = dict.TryGetValue("x", out var x) ? x.ToString() : "0",
                Y = dict.TryGetValue("y", out var y) ? y.ToString() : "0"
            };
        }
        if (raw is Dictionary<string, object> dictStr)
        {
            return new SizeExpression
            {
                X = dictStr.TryGetValue("x", out var x) ? x.ToString() : "0",
                Y = dictStr.TryGetValue("y", out var y) ? y.ToString() : "0"
            };
        }
        throw new Exception("Invalid SizeExpression format");
    }
} 