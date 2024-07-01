using UnityEngine;

public class Rectangle : ShapeBase
{
  protected override string ShapeColor { get; } = "FFF1C1";

  protected override void Create(GameObject target)
  {
    // Shape.transform.localScale = new Vector3(1.5f, 0.5f, 1);
  }
}