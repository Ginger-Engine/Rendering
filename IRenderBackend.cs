using System.Drawing;
using System.Numerics;
using Engine.Rendering.Cameras;
using Engine.Rendering.Drawables;
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
    void Draw(IDrawable drawable);

    void SetCamera(ICamera? camera);
}