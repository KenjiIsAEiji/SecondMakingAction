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

                break;

            case GameState.End:

                break;

        }
    }
    
    /// <summary>
    /// Change now gameState
    /// </summary>
    /// <param name="state">To State</param>
    public void SetGameState(GameState state)
    {
        GameState beforeState = gameState;
        gameState = state;

        Debug.Log("Change game state from:" + beforeState + " To:" + gameState);
    }
}
