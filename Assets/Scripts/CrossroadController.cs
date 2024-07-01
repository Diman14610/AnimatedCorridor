using System;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadController : MonoBehaviour
{
  private bool _init;
  private List<ShapeBase> _storage = new List<ShapeBase>();
  private HashSet<Vector2> _occupiedPositions = new HashSet<Vector2>();

  public void AddCorridorTo(Side side)
  {
    AddShapeTo<Corridor>(side);
  }

  public void AddCrossroadTo(Side side)
  {
    AddShapeTo<Crossroad>(side);
  }

  private bool IsPositionOccupied(Vector2 position)
  {
    // Проверяем, занята ли позиция
    return _occupiedPositions.Contains(position);
  }

  private bool CanPlaceShape(Vector2 position, Side side, Vector2 shapeSize)
  {
    // Проверяем, можно ли разместить форму в данной позиции с учетом размера формы
    for (int x = 0; x < shapeSize.x; x++)
    {
      for (int y = 0; y < shapeSize.y; y++)
      {
        Vector2 posToCheck = position + new Vector2(x, y);
        if (IsPositionOccupied(posToCheck))
        {
          return false;
        }
      }
    }
    // Если позиция свободна, занимаем ее
    OccupyPosition(position, shapeSize);
    return true;
  }

  private void OccupyPosition(Vector2 position, Vector2 shapeSize)
  {
    // Занимаем позиции для всей формы
    for (int x = 0; x < shapeSize.x; x++)
    {
      for (int y = 0; y < shapeSize.y; y++)
      {
        Vector2 posToOccupy = position + new Vector2(x, y);
        _occupiedPositions.Add(posToOccupy);
      }
    }
  }

  private void AddShapeTo<TShape>(Side side) where TShape : IShape, new()
  {
    Init(side);

    var shape = new TShape();
    var corridor = shape.Create();
    var lastShape = _storage[_storage.Count - 1];
    var target = GameObject.Find(lastShape.Name);
    var newPosition = CalculateNewPosition(target.transform.position, side);

    // Предположим, что размеры коридора и перекрестка - это 1x1 и 2x2 соответственно
    Vector2 shapeSize = typeof(TShape) == typeof(Corridor) ? new Vector2(1, 1) : new Vector2(2, 2);

    if (!CanPlaceShape(newPosition, side, shapeSize))
    {
      Debug.Log("Невозможно добавить элемент: пространство уже занято.");
      return;
    }

    corridor.CreateShape(target, side);
    _storage.Add(corridor);
    _occupiedPositions.Add(newPosition);
  }

  private Vector2 CalculateNewPosition(Vector2 currentPosition, Side side)
  {
    switch (side)
    {
      case Side.Left:
        return currentPosition + new Vector2(-1, 0);
      case Side.Right:
        return currentPosition + new Vector2(1, 0);
      case Side.Top:
        return currentPosition + new Vector2(0, 1);
      case Side.Bottom:
        return currentPosition + new Vector2(0, -1);
      default:
        throw new ArgumentOutOfRangeException(nameof(side), side, null);
    }
  }

  private void Init(Side side)
  {
    if (_init)
    {
      return;
    }

    GameObject room = GameObject.Find("Room");

    var values = Enum.GetValues(typeof(Side));
    foreach (var item in values)
    {
      var corridor = new Corridor().Create();
      Enum.TryParse<Side>(item.ToString(), out var newSide);
      corridor.CreateShape(room, newSide);
      _storage.Add(corridor);
      _occupiedPositions.Add(room.transform.position);
    }

    _init = true;
  }
}
