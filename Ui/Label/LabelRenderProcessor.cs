using Engine.Rendering.Sprites;

namespace Engine.Rendering.Ui.Label;

public class LabelRenderProcessor(IRenderBackend renderBackend) : IRenderProcessor<Label>
{
    public void Process(Label label)
    {
        renderBackend.DrawText(label.Text, label.Position, label.Rotation, label.Scale, label.FontSize, label.Color);
    }

    public void Process(object obj)
    {
        if (obj is Label label) Process(label);
        else throw new Exception("obj is not a Label");
    }
}