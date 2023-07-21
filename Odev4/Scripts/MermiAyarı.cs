using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MermiAyarı : MonoBehaviour
{
    public float speed = 100f;
    public float lifeTime = 5f;

    public GameObject particleSystem1;
    public GameObject particleSystem2;
    public GameObject particleSystem3;
    public AudioClip explosionSound;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        /*lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Nesne"))
        {
            //Particle çalıştır
            Instantiate(particleSystem1, transform.position, Quaternion.identity);
            Instantiate(particleSystem2, transform.position, Quaternion.Euler(-90, 0, 0));
            Instantiate(particleSystem3, transform.position, Quaternion.identity);

            //Sesi oynat
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
    }
}
