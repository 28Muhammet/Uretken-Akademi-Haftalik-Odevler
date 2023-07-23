using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public GameObject targetObject;

    public float speed = 7f;
    public float rotationSpeed = 20f;
    private float rotationAmound = 0f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0f)
        {
            rotationAmound += horizontal * rotationSpeed * Time.deltaTime;
            targetObject.transform.rotation = Quaternion.Euler(0f, rotationAmound, 0f);
        }
        
        Vector3 movement = (targetObject.transform.forward * vertical * speed * Time.deltaTime);
        transform.Translate(movement);
    }
}
