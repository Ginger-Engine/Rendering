using System.Drawing;
using Engine.Core;
using Engine.Rendering.Layers;

namespace Engine.Rendering.Ui.Label;

public struct LabelComponent : IComponent
{
    public string Text;
    public string Font;
    public int FontSize;
    public Color Color;
}