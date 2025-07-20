using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData: ScriptableObject
{
    public string Name;
    public int Id;
    public int Cost;
    public GameObject Material;
}
