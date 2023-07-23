using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float xRot = 0.0f;
    public float sens = 20f;
    public GameObject target;

    private void Update()
    {
        float rotX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float rotY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xRot -= rotY;
        xRot = Mathf.Clamp(xRot, -2, 3);

        transform.localRotation = Quaternion.Euler(xRot, rotY, 0);
        target.transform.Rotate(Vector3.up * rotX);
    }
}
