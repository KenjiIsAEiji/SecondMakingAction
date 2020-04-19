using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data",menuName ="SpawnData")]
public class SpawnData : ScriptableObject
{
    
    [SerializeField] string DataName;
    [Header("通常の敵出現数")]
    public int NomalEnemys = 0;
    [Header("遠距離タイプの敵出現数")]
    public int LongRangeEnemys = 0;

}
