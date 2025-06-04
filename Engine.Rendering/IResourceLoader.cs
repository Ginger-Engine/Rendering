namespace Engine.Rendering;

public interface IResourceLoader<out T>
{
    T Load(string path);
}