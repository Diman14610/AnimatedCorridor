using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Room : MonoBehaviour
{
  [SerializeField] public int Size = 5;
  [SerializeField] public float Delay = 0.09f;

  private CorridorGenerator _corridorGenerator;

  void Start()
  {
    _corridorGenerator = new GameObject("CorridorGenerator").AddComponent<CorridorGenerator>();
    StartCoroutine(_corridorGenerator.StartGeneration(Size, Delay));
  }
}
