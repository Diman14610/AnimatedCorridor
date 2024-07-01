using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

public class CorridorGenerator : MonoBehaviour
{
  private float _delay = 0.09f;
  private Array sides = Enum.GetValues(typeof(Side));
  private CrossroadController _controller;
  private System.Random _random = new System.Random();

  void Awake()
  {
    _controller = gameObject.AddComponent<CrossroadController>();
  }

  public IEnumerator StartGeneration(int size)
  {
    Debug.Log(size);
    yield return StartCoroutine(Generate(size));
  }

  private IEnumerator Generate(int numberOfGenerations)
  {
    for (int i = 0; i < numberOfGenerations; i++)
    {
      // Добавляем перекрёсток
      _controller.AddCrossroadTo(GetRandomSide());
      yield return new WaitForSeconds(_delay);

      // Рандомное количество коридоров от 1 до 5
      int corridorsCount = _random.Next(1, 6);
      for (int j = 0; j < corridorsCount; j++)
      {
        _controller.AddCorridorTo(GetRandomSide());
        yield return new WaitForSeconds(_delay);
      }
    }
  }

  private Side GetRandomSide()
  {
    return (Side)_random.Next(0, sides.Length);
  }
}
