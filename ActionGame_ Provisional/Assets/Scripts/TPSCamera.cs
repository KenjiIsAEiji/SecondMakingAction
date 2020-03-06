using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float RotateSpeed = 10.0f;
    [SerializeField] float FollowSpeed = 10.0f;

    float yaw, pitch;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            Player.position,
            Time.deltaTime * FollowSpeed
        );

        yaw += Input.GetAxis("Mouse X") * RotateSpeed * Time.unscaledDeltaTime;
        pitch -= Input.GetAxis("Mouse Y") * RotateSpeed * Time.unscaledDeltaTime;

        pitch = Mathf.Clamp(pitch, -80, 60);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
