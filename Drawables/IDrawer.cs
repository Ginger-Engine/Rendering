namespace Engine.Rendering.Drawables;

public interface IDrawer
{
    public Type Type { get; }
    void Draw(object drawable);
}

public interface IDrawer<in T> : IDrawer where T : struct, IDrawable
{
    void Draw(T drawable);
}