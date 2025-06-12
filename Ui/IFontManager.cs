namespace Engine.Rendering.Ui;

public interface IFontManager
{
    public void Register(FontDescription fontDescription);
    public IFont Get(string name);
}