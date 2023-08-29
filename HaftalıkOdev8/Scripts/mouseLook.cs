using UnityEngine;

public class mouseLook : MonoBehaviour
{
    [Range(50, 500)]
    [SerializeField] private float sens;
    [SerializeField] private Transform body;

    private float xRot = 0f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float rotX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        float rotY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        xRot -= rotY;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        body.Rotate(Vector3.up * rotX);
    }
}
