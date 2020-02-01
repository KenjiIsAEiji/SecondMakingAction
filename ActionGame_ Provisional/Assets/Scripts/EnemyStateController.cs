using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    enum EnemyState
    {
        Ready = 0,
        Move = 1,
        Attack = 2,
        Dead = 3
    }

    EnemyState NowEnemyState;
    
    // Start is called before the first frame update
    void Start()
    {
        NowEnemyState = EnemyState.Move;
    }

    // Update is called once per frame
    void Update()
    {
        switch (NowEnemyState)
        {
            case EnemyState.Ready:
                
                break;
            case EnemyState.Move:
                
                break;
            case EnemyState.Attack:

                break;
            case EnemyState.Dead:

                break;
        }
    }
}
