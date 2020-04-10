using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonMonoBehaviour<SpawnManager>
{
    [SerializeField] GameObject NormalEnemy;
    [SerializeField] GameObject LongRangeEnemy;

    [SerializeField] List<Transform> SpawnPosionList;
    public List<GameObject> Enemys;

    [Header("スポーンデータ")]
    [SerializeField] List<SpawnData> spawnDatas;

    public enum SponeType
    {
        Single,
        Multi
    }

    private void Start()
    {
        Spawn();
    }

    //private void Update()
    //{
    //    Debug.Log("enemys : " + Enemys.Count);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f, 0.7f);

        for(int i = 0; i < SpawnPosionList.Count; i++)
        {
            Gizmos.DrawCube(SpawnPosionList[i].position, new Vector3(1, 2, 1));
        }
    }

    public void Spawn()
    {
        int pointIndex = Random.Range(0, SpawnPosionList.Count);

        for(int i = 0; i < spawnDatas[0].SpawnEnemys; i++)
        {
            int spownIndex = SpawnPosionList.Count - (pointIndex + i);

            Enemys.Add(Instantiate(NormalEnemy, SpawnPosionList[spownIndex].position, SpawnPosionList[spownIndex].rotation));
            Debug.Log("Spawning point = " + spownIndex);
        }

        Debug.Break();
    }
}
