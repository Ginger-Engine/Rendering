namespace Engine.Rendering.Ui;

public interface IFont
{
    public string Name { get; }
    public string Filename { get; }
    public float BaseSize { get; }
    public float GlyphPadding { get; }
}