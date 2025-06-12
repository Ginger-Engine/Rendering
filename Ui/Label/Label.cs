using System.Drawing;
using System.Numerics;
using Engine.Rendering.Layers;

namespace Engine.Rendering.Ui.Label;

public class Label : IRenderable
{
        public required string Text;
        public IFont? Font;
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public int FontSize;
        public Color Color;
        public Layer Layer { get; set; }
}