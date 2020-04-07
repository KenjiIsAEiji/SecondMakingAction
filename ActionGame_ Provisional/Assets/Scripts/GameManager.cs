using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Ready,
        Play,
        End
    }

    [SerializeField] GameState gameState;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

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
}
