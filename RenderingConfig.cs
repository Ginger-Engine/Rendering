using System.Numerics;
using Engine.Rendering.Ui;
using Engine.Rendering.Utils;

namespace Engine.Rendering;

public struct SizeExpression {
    public string X { get; set; }
    public string Y { get; set; }

    public Vector2 Evaluate(Dictionary<string, float> variables)
    {
        var y = EvaluateSingle(Y, variables);
        var x = EvaluateSingle(X, variables); // если x зависит от y

        return new Vector2(x, y);
    }

    private float EvaluateSingle(string expr, Dictionary<string, float> variables)
    {
        if (string.IsNullOrWhiteSpace(expr))
            throw new Exception("Expression is empty");

        return ExpressionEvaluator.Eval(expr, variables);
    }
}

public struct RenderingConfig
{
    public string[] Layers;
    public FontDescription[] Fonts;
    public SizeExpression WindowSize;
    public string WindowTitle;
}