namespace Engine.Rendering.Drawables;

public abstract class AbstractDrawer<T> : IDrawer<T> where T : struct, IDrawable
{
    public Type Type => typeof(T);
    public abstract void Draw(T drawable);
    public void Draw(object drawable)
    {
        Draw((T)drawable);
    }
}