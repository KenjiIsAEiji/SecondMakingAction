using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("スコア基底ポイント")]
    [SerializeField] int nomalPoint = 10;
    [SerializeField] int LongRangePoint = 15;
    [SerializeField] int LPRemaingPoint = 1000;
    [SerializeField] int PassingTimePoint = 5000;

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
                int NomalDestroyPoints = nomalEnemyDestroys * nomalPoint;
                int LongRangeDestroyPoints = longRangeEnemyDestroys * LongRangePoint;
                int LPRemaingPoints = (int)(LPRemaingPoint * player.GetPlayerLPRatio());
                int PassingTimePoints = (int)(PassingTimePoint / fightTime);

                int Score = NomalDestroyPoints + LongRangeDestroyPoints + LPRemaingPoints + PassingTimePoints;

                Debug.Log("<color=#0000ffff>score " + Score + "</color>");

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
