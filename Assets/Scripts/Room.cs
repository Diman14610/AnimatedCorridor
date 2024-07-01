using UnityEngine;

public class Room : MonoBehaviour
{
  [SerializeField] public int NumberOfGenerations = 5;
  [SerializeField] public float Delay = 0.09f;

  [SerializeField] public int MinNumberOfCorridors = 1;
  [SerializeField] public int MaxNumberOfCorridors = 6;

  private CorridorGenerator _corridorGenerator;

  void Start()
  {
    _corridorGenerator = new GameObject("CorridorGenerator").AddComponent<CorridorGenerator>();
    StartCoroutine(_corridorGenerator.StartGeneration(
      NumberOfGenerations,
      Delay,
      MinNumberOfCorridors,
      MaxNumberOfCorridors
      ));
  }
}
