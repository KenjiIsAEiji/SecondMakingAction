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
        int index = Random.Range(0, SpawnPosionList.Count);

        Debug.Log("Spawning point = " + index + " " + spawnDatas[0].SpawnEnemys + "Enenys");

        Enemys.Add(Instantiate(NormalEnemy, SpawnPosionList[index].position, SpawnPosionList[index].rotation));
    }
}
