using UnityEngine;
using System;
using System.Collections;

public class CorridorGenerator : MonoBehaviour
{
  private int _minNumberOfCorridors = 1;
  private int _maxNumberOfCorridors = 6;

  private float _delay = 0.09f;
  private Array sides = Enum.GetValues(typeof(Side));
  private CrossroadController _controller;
  private System.Random _random = new System.Random();

  void Awake()
  {
    _controller = gameObject.AddComponent<CrossroadController>();
  }

  public IEnumerator StartGeneration(int numberOfGenerations, float delay, int minNumberOfCorridors, int maxNumberOfCorridors)
  {
    Debug.Log($"numberOfGenerations: {numberOfGenerations}, delay: {delay}, minNumberOfCorridors: {minNumberOfCorridors}, maxNumberOfCorridors: {maxNumberOfCorridors}.");
    _delay = delay;
    _minNumberOfCorridors = minNumberOfCorridors;
    _maxNumberOfCorridors = maxNumberOfCorridors + 1;
    yield return StartCoroutine(Generate(numberOfGenerations));
  }

  private IEnumerator Generate(int numberOfGenerations)
  {
    for (int i = 0; i < numberOfGenerations; i++)
    {
      // var side = GetRandomSide();
      _controller.AddCrossroadTo(GetRandomSide());
      yield return new WaitForSeconds(_delay);

      int corridorsCount = _random.Next(_minNumberOfCorridors, _maxNumberOfCorridors);
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
