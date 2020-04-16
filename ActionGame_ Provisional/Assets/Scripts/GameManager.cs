﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum GameState
    {
        Ready,
        Play,
        End
    }

    [SerializeField] GameState gameState;

    [Header("プレイヤー参照")]
    [SerializeField] PlayerController player;

    [Header("スコアデータ")]
    public int nomalEnemyDestroys = 0;
    public int longRangeEnemyDestroys = 0;
    public float fightTime = 0;

    [Header("スコア参照用")]
    public int NomalDestroyPoints;
    public int LongRangeDestroyPoints;
    public int LPRemaingPoints;
    public int PassingTimePoints;


    [Header("スコア基底ポイント")]
    [SerializeField] ScoreManagementData scoreManagement;

    [Header("Clear画面")]
    [SerializeField] GameObject ClearUIObject;

    /// <summary>
    /// スコア算出メモ
    /// 
    /// nomalEnemyDestroy(通常の敵撃破数)              * 10p
    /// longRangeEnemyDestroy(遠距離タイプの敵撃破数)  * 15p
    /// 
    /// 1000p * player.GetPlayerLPRatio()
    /// 1000p / (int)fightTime
    /// 
    /// </summary>

    // Start is called before the first frame update
    private void Start()
    {
        gameState = GameState.Ready;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (gameState)
        {
            case GameState.Ready:

                break;

            case GameState.Play:

                fightTime += Time.deltaTime;

                break;

            case GameState.End:
                NomalDestroyPoints = nomalEnemyDestroys * scoreManagement.BaseNomalPoint;
                LongRangeDestroyPoints = longRangeEnemyDestroys * scoreManagement.BaseLongRangePoint;
                LPRemaingPoints = (int)(scoreManagement.BaseLPRemaingPoint * player.GetPlayerLPRatio());
                PassingTimePoints = (int)(scoreManagement.BasePassingTimePoint / fightTime);

                ClearUIObject.SetActive(true);
                break;

        }
    }
    
    /// <summary>
    /// Change now gameState
    /// </summary>
    /// <param name="state">Change to State</param>
    public void SetGameState(GameState state)
    {
        GameState beforeState = gameState;
        gameState = state;

        Debug.Log("Change game state from:" + beforeState + " To:" + gameState);
    }
}
