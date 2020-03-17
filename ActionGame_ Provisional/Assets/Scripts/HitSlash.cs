using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlash : MonoBehaviour
{
    [SerializeField] float AttackPower = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyStateController>().Damage(AttackPower, 1f);
        }

        if (other.gameObject.CompareTag("MidleEnemy"))
        {
            other.gameObject.GetComponent<EnemyMidleController>().Damage(AttackPower, 1f);
        }
    }
}
