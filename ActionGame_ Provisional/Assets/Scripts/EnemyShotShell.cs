using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotShell : MonoBehaviour
{
    public GameObject enemyShellPrefab;
    public float shotSpeed;
    public float rate;
    float shootInterval=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shootInterval += Time.deltaTime;
        if(shootInterval >= rate)
        {
            Shoot();
            shootInterval = 0.0f;
        }
    }
    private void Shoot()
    {
        GameObject enemyShell = Instantiate(enemyShellPrefab, transform.position, Quaternion.identity);

        Rigidbody enemyShellRb = enemyShell.GetComponent<Rigidbody>();

        // forwardはZ軸方向（青軸方向）・・・＞この方向に力を加える。
        enemyShellRb.AddForce(transform.forward * shotSpeed, ForceMode.Impulse);

        Destroy(enemyShell, 3.0f);
    }
}