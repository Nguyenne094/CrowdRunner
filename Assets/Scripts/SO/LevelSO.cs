using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_default", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    [Header("Ordered Chunk")]
    public Chunk[] chunks;
}
