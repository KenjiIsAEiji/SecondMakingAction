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
    [SerializeField] int Waves = 0;

    private void Start()
    {
        Spawn(spawnDatas[0]);
        Waves++;

        GameManager.Instance.SetGameState(GameManager.GameState.Play);
    }

    private void Update()
    {
        Debug.Log("enemys : " + Enemys.Count);
        if(Enemys.Count <= 0 && Waves > 0)
        {
            if(spawnDatas.Count > Waves)
            {
                Spawn(spawnDatas[Waves]);
                Waves++;
            }
            else
            {
                Debug.Log("ALL WAVE CLEAR");
                GameManager.Instance.SetGameState(GameManager.GameState.End);
                Waves = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f, 0.7f);

        for(int i = 0; i < SpawnPosionList.Count; i++)
        {
            Gizmos.DrawCube(SpawnPosionList[i].position, new Vector3(1, 2, 1));
        }
    }

    public void Spawn(SpawnData spawnData)
    {
        int pointIndex = Random.Range(0, SpawnPosionList.Count);
        int N_enemys = 0, L_enemys = 0;

        if ((spawnData.NomalEnemys + spawnData.LongRangeEnemys) > SpawnPosionList.Count) 
        { 
            Debug.LogError("出現位置（SpawnPoint）の数が足りません!");
            Debug.LogError("敵の合計数を" + SpawnPosionList.Count + "以下にしてください");
            return;
        }

        for (int i = 1; i <= (spawnData.NomalEnemys + spawnData.LongRangeEnemys); i++)
        {
            int spawnIndex = (pointIndex + i) < SpawnPosionList.Count ? (pointIndex + i) : (pointIndex + i) - SpawnPosionList.Count;

            if (N_enemys < spawnData.NomalEnemys && L_enemys < spawnData.LongRangeEnemys)
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
            else if (N_enemys >= spawnData.NomalEnemys && L_enemys < spawnData.LongRangeEnemys)
            {
                Enemys.Add(Instantiate(LongRangeEnemy, SpawnPosionList[spawnIndex].position, SpawnPosionList[spawnIndex].rotation));
                L_enemys++;
            }
            else if (N_enemys < spawnData.NomalEnemys && L_enemys >= spawnData.LongRangeEnemys)
            {
                Enemys.Add(Instantiate(NormalEnemy, SpawnPosionList[spawnIndex].position, SpawnPosionList[spawnIndex].rotation));
                N_enemys++;
            }
        }
    }

    public void DestroyEnemy(GameObject enemyObj)
    {
        if (enemyObj.CompareTag("Enemy"))
        {
            GameManager.Instance.nomalEnemyDestroys++;
        }
        else
        {
            GameManager.Instance.longRangeEnemyDestroys++;
        }

        Enemys.Remove(enemyObj);
    }
}
