using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneData : ScriptableObject
{
    public Camera MainCamer;
    public GameObject Cube;
    public GameObject Platform;
    public GameObject CardPrefab;
    public List<CardData> CardsData;
    public float MaxTimer;
}
