using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellHItChecker : MonoBehaviour
{
    [SerializeField] float AttackPower = 5f;
    [SerializeField] GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(AttackPower, collision.contacts[0].normal);
            Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
