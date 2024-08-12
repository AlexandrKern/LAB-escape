using System.Collections.Generic;
using UnityEngine;


public class SwarmForm : SwarmFormBase
{
    [SerializeField] public SpriteRenderer spriteRenderer;

    private Texture2D texture;
    private List<DestinationPoint> _points = new List<DestinationPoint>();

    void Start()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not assigned.");
            return;
        }

        // Конвертация спрайта в Texture2D
        Rect spriteLocationInAtlas = spriteRenderer.sprite.rect;
        texture = new Texture2D((int)spriteLocationInAtlas.width, 
            (int)spriteLocationInAtlas.height);
        texture.SetPixels(spriteRenderer.sprite.texture.GetPixels((int)spriteLocationInAtlas.x, 
            (int)spriteLocationInAtlas.y, (int)spriteLocationInAtlas.width, 
            (int)spriteLocationInAtlas.height));

        // Генерация точек
        GeneratePoints();
    }

    void GeneratePoints()
    {
        for (int i = 0; i < Swarm.PointCounts; i++)
        {
            Vector2 point;
            bool validPoint = false;

            // Попытка найти случайную видимую точку
            while (!validPoint)
            {
                point = GetRandomPointInSprite();

                if (IsPointVisible(point))
                {
                    validPoint = true;
                    Transform t = new GameObject().transform;
                    t.parent = transform;
                    t.position = point;
                    _points.Add(new DestinationPoint(t, true));
                }
            }
        }
    }

    private Vector3 GetBoundsSizeScaled()
    {
        return spriteRenderer.bounds.max - spriteRenderer.bounds.min;
    }

    Vector2 GetRandomPointInSprite()
    {
        // Получаем случайную точку в пределах размеров спрайта
        float x = Random.Range(spriteRenderer.bounds.min.x, spriteRenderer.bounds.max.x);
        float y = Random.Range(spriteRenderer.bounds.min.y, spriteRenderer.bounds.max.y);

        return new Vector2(x, y);
    }

    bool IsPointVisible(Vector2 point)
    {
        // Преобразуем мировые координаты в пиксельные координаты текстуры
        Vector2 localPoint = (point - (Vector2)spriteRenderer.bounds.min) / GetBoundsSizeScaled();
        localPoint.x *= texture.width;
        localPoint.y *= texture.height;

        Color pixelColor = texture.GetPixel((int)localPoint.x, (int)localPoint.y);

        // Проверяем, если пиксель непрозрачен (альфа-канал больше 0)
        return pixelColor.a > 0;
    }

    public override List<DestinationPoint> GetDestenationPoints()
    {
        return _points;
    }
}
