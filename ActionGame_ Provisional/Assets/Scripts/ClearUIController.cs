using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int tortalScore =
            GameManager.Instance.NomalDestroyPoints +
            GameManager.Instance.LongRangeDestroyPoints +
            GameManager.Instance.LPRemaingPoints +
            GameManager.Instance.PassingTimePoints;

        Debug.Log("<color=#0000ffff>TortalScore " + tortalScore + "</color>");

    }
}
