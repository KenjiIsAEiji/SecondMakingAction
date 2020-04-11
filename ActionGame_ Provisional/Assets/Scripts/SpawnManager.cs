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

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        Debug.Log("enemys : " + Enemys.Count);
    }

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
        int N_enemys = 0, L_enemys = 0;

        if ((spawnDatas[0].NomalEnemys + spawnDatas[0].LongRangeEnemys) > SpawnPosionList.Count) 
        { 
            Debug.LogError("出現位置（SpawnPoint）の数が足りません!");
            Debug.LogError("敵の合計数を" + SpawnPosionList.Count + "以下にしてください");
            return;
        }

        for (int i = 1; i <= (spawnDatas[0].NomalEnemys + spawnDatas[0].LongRangeEnemys); i++)
        {
            int spawnIndex = (pointIndex + i) < SpawnPosionList.Count ? (pointIndex + i) : (pointIndex + i) - SpawnPosionList.Count;

            if (N_enemys < spawnDatas[0].NomalEnemys && L_enemys < spawnDatas[0].LongRangeEnemys)
            {
                int nomalFlag = Random.Range(0, 2);

                if (nomalFlag == 1)
                {
                    Enemys.Add(Instantiate(NormalEnemy, SpawnPosionList[spawnIndex].position, SpawnPosionList[spawnIndex].rotation));
                    N_enemys++;
                }
                else
                {
                    Enemys.Add(Instantiate(LongRangeEnemy, SpawnPosionList[spawnIndex].position, SpawnPosionList[spawnIndex].rotation));
                    L_enemys++;
                }
            }
            else if (N_enemys >= spawnDatas[0].NomalEnemys && L_enemys < spawnDatas[0].LongRangeEnemys)
            {
                Enemys.Add(Instantiate(LongRangeEnemy, SpawnPosionList[spawnIndex].position, SpawnPosionList[spawnIndex].rotation));
                L_enemys++;
            }
            else if (N_enemys < spawnDatas[0].NomalEnemys && L_enemys >= spawnDatas[0].LongRangeEnemys)
            {
                Enemys.Add(Instantiate(NormalEnemy, SpawnPosionList[spawnIndex].position, SpawnPosionList[spawnIndex].rotation));
                N_enemys++;
            }
        }
    }
}
