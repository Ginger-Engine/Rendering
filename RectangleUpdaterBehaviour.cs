using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Rendering.Windows;
using System.Numerics;

namespace Engine.Rendering;

public class RectangleUpdaterBehaviour : IEntityBehaviour
{
    private readonly IWindow _window;
    public RectangleUpdaterBehaviour(IWindow window)
    {
        _window = window;
    }

    public void OnStart(Entity entity)
    {
        UpdateRectangle(entity);
        entity.SubscribeComponentChange<RectangleComponent>(_ => UpdateRectangle(entity));
        // TODO: подписка на изменение размеров окна для корневых элементов
    }

    private void UpdateRectangle(Entity entity)
    {
        entity.Modify((ref RectangleComponent rect) =>
        {
            Vector2 parentSize;
            if (entity.Parent != null && entity.Parent.TryGetComponent<RectangleComponent>(out var parentRect))
                parentSize = parentRect.SizeCache;
            else
                parentSize = _window.GetSize();

            var variables = new Dictionary<string, float>
            {
                ["parent.width"] = parentSize.X,
                ["parent.height"] = parentSize.Y
            };
            rect.SizeCache = rect.Size.Evaluate(variables);
        });

        // Рекурсивно обновляем всех детей с RectangleComponent
        foreach (var child in entity.Children)
        {
            if (child.HasComponent<RectangleComponent>())
                UpdateRectangle(child);
        }
    }
} 