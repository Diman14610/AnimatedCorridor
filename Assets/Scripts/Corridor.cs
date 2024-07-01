using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : IShape
{
  public ShapeBase Create()
  {
    return new Rectangle();
  }
}
