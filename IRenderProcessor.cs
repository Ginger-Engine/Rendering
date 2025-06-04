namespace Engine.Rendering;

public interface IRenderProcessor
{
    void Process(object obj);
}
public interface IRenderProcessor<in T> : IRenderProcessor
{
    public void Process(T renderable);
}