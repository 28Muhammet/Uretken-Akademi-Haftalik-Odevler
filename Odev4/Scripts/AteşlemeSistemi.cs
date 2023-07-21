using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AteşlemeSistemi : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletExitPos;
    static public AudioSource audioSource;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, bulletExitPos.position, transform.rotation * Quaternion.Euler(0, 0, 90));
        }
    }
}
