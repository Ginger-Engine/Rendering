using System.Drawing;
using System.Numerics;
using Engine.Rendering.Cameras;
using Engine.Rendering.Textures;
using Engine.Rendering.Ui;

namespace Engine.Rendering;

public interface IRenderBackend
{
    void Init();
    bool IsRunning();
    void Shutdown();

    void Start();
    void End();
    void Render();
    void DrawTexture(ITexture texture, Vector2 position, float rotation, Vector2 scale);
    void DrawText(string text, Vector2 position, float rotation, Vector2 scale, float fontSize, Color color, IFont? font = null);

    void SetCamera(ICamera? camera);
    void DrawLine(Vector2 from, Vector2 to,  Color color, float thickness);
}