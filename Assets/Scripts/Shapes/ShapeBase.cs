using System;
using UnityEngine;

public enum Side { Left, Right, Top, Bottom }

public abstract class ShapeBase
{
  public string Name { get; private set; }

  protected float pixelsPerUnit { get; } = 1f;

  protected GameObject Shape { get; private set; }
  protected Texture2D RectTexture { get; private set; }
  protected SpriteRenderer SpriteRenderer { get; private set; }

  protected abstract string ShapeColor { get; }

  public ShapeBase()
  {
    Name = Guid.NewGuid().ToString("n");
  }

  public ShapeBase(string name)
  {
    Name = name;
  }

  protected abstract void Create(GameObject target);

  public void CreateShape(GameObject target, Side side)
  {
    // Создание фигуры
    Shape = new GameObject(Name);

    // Добавление компонента SpriteRenderer и установка цвета
    SpriteRenderer = Shape.AddComponent<SpriteRenderer>();
    SpriteRenderer.color = Color.white;

    // Создание текстуры для спрайта
    RectTexture = new Texture2D(1, 1);
    RectTexture.SetPixel(0, 0, Color.white);
    RectTexture.Apply();

    SetColorByHex(Shape, ShapeColor);

    // Создание спрайта
    SpriteRenderer.sprite = Sprite.Create(
        RectTexture,
        new Rect(0.0f, 0.0f, RectTexture.width, RectTexture.height),
        new Vector2(0.5f, 0.5f),
        pixelsPerUnit
        );

    Create(target);

    Bounds roomBounds = target.GetComponent<Renderer>().bounds;
    switch (side)
    {
      case Side.Right:
        Shape.transform.position = new Vector3(roomBounds.max.x + SpriteRenderer.bounds.size.x / 2, target.transform.position.y, target.transform.position.z);
        break;

      case Side.Left:
        Shape.transform.position = new Vector3(roomBounds.min.x - SpriteRenderer.bounds.size.x / 2, target.transform.position.y, target.transform.position.z);
        break;

      case Side.Top:
        Shape.transform.position = new Vector3(target.transform.position.x, roomBounds.max.y + SpriteRenderer.bounds.size.y / 2, target.transform.position.z);

        // Вместо y берётся x, т.к. идёт поворот ( Shape.transform.position = new Vector3(target.transform.position.x, roomBounds.max.y + SpriteRenderer.bounds.size.y / 2, target.transform.position.z);)
        // Shape.transform.position = new Vector3(target.transform.position.x, roomBounds.max.y + SpriteRenderer.bounds.size.x / 2, target.transform.position.z);
        // Shape.transform.Rotate(0, 0, 90);
        break;

      case Side.Bottom:
        Shape.transform.position = new Vector3(target.transform.position.x, roomBounds.min.y - SpriteRenderer.bounds.size.y / 2, target.transform.position.z);

        // Вместо y берётся x, т.к. идёт поворот ( Shape.transform.position = new Vector3(target.transform.position.x, roomBounds.min.y - SpriteRenderer.bounds.size.y / 2, target.transform.position.z);)
        // Shape.transform.position = new Vector3(target.transform.position.x, roomBounds.min.y - SpriteRenderer.bounds.size.x / 2, target.transform.position.z);
        // Shape.transform.Rotate(0, 0, 90);
        break;
    }
  }

  protected void SetColorByHex(GameObject obj, string hex)
  {
    // Проверка на корректность HEX-кода
    if (hex.Length != 6) return;

    // Конвертация HEX в цвет RGB
    float r = (float)System.Convert.ToInt32(hex.Substring(0, 2), 16) / 255;
    float g = (float)System.Convert.ToInt32(hex.Substring(2, 2), 16) / 255;
    float b = (float)System.Convert.ToInt32(hex.Substring(4, 2), 16) / 255;

    // Установка цвета объекту
    if (obj.GetComponent<SpriteRenderer>())
    {
      obj.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
    }
    else if (obj.GetComponent<Renderer>())
    {
      obj.GetComponent<Renderer>().material.color = new Color(r, g, b);
    }
  }
}