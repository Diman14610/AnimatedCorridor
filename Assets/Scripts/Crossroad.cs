using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroad: IShape
{
  public ShapeBase Create()
  {
    return new Square();
  }
}
