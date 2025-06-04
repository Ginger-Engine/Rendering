namespace Engine.Rendering.Sprites;

public class SpriteRenderProcessor(IRenderBackend renderBackend) : IRenderProcessor<Sprite>
{
    public void Process(Sprite sprite)
    {
        renderBackend.DrawTexture(sprite.Texture, sprite.Position);
    }

    public void Process(object obj)
    {
        if (obj is Sprite sprite) Process(sprite);
        else throw new Exception("obj is not a Sprite");
    }
}