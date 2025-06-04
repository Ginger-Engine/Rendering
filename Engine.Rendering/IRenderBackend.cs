using System.Numerics;
using Engine.Rendering.Textures;

namespace Engine.Rendering;

public interface IRenderBackend
{
    void Init();
    bool IsRunning();
    void Shutdown();

    void Start();
    void End();
    void Render();
    void DrawTexture(ITexture texture, Vector2 position);
}