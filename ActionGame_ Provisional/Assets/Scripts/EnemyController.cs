using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    NavMeshHit hit;
    public GameObject ShotShell;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
        if (!agent.Raycast(target.position, out hit))
        {
            //見える
            agent.ResetPath();
            transform.LookAt(target);
            ShotShell.GetComponent<EnemyShotShell>().enabled=true;
        }
        else
        {
            // 見えない
            ShotShell.GetComponent<EnemyShotShell>().enabled=false;
        }
    }
}
