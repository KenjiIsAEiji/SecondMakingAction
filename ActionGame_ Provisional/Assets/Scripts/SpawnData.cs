using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data",menuName ="SpawnData")]
public class SpawnData : ScriptableObject
{
    public enum EnemyType
    {
        nomal,
        longRange
    }
    
    [SerializeField] string DataName;
    public EnemyType SpawnEnemyType = EnemyType.nomal;
    public int SpawnEnemys = 0;

}
