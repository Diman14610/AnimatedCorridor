using System;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadController : MonoBehaviour
{
  private bool _init;
  private List<ShapeBase> _storage = new List<ShapeBase>();
  private HashSet<Vector3> _occupiedPositions = new HashSet<Vector3>();

  public void AddCorridorTo(Side side)
  {
    AddShapeTo<Corridor>(side);
  }

  public void AddCrossroadTo(Side side)
  {
    AddShapeTo<Crossroad>(side);
  }

  private void AddShapeTo<TShape>(Side side) where TShape : IShape, new()
  {
    var shape = new TShape();
    var shapeBase = shape.Create();

    if (!_init)
    {
      var room = GameObject.Find("Room");
      Bounds bounds_room = room.GetComponent<Renderer>().bounds;
      shapeBase.CreateShape(room, side);

      var innerShapeObject = GameObject.Find(shapeBase.Name);
      Bounds shapeBounds = innerShapeObject.GetComponent<Renderer>().bounds;

      _occupiedPositions.Add(shapeBounds.center);
      _storage.Add(shapeBase);
      _init = true;
      Destroy(room);
      return;
    }

    var lastShape = _storage[_storage.Count - 1];
    var target = GameObject.Find(lastShape.Name);

    shapeBase.CreateShape(target, side);
    var shapeObject = GameObject.Find(shapeBase.Name);

    Bounds bounds = shapeObject.GetComponent<Renderer>().bounds;

    if (_occupiedPositions.Contains(bounds.center))
    {
      Destroy(shapeObject);
      return;
    }

    _occupiedPositions.Add(bounds.center);
    _storage.Add(shapeBase);
  }

  private bool Init(Side side)
  {
    if (_init)
    {
      return false;
    }

    GameObject room = GameObject.Find("Room");

    var values = Enum.GetValues(typeof(Side));
    foreach (var item in values)
    {
      var corridor = new Corridor().Create();
      Enum.TryParse<Side>(item.ToString(), out var newSide);
      corridor.CreateShape(room, newSide);
      var z = GameObject.Find(corridor.Name);
      Bounds bounds = z.GetComponent<Renderer>().bounds;
      _occupiedPositions.Add(bounds.center);
      _storage.Add(corridor);
    }

    _init = true;

    return true;
  }
}
