using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject[] objeler;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.CompareTag("Player"))
        {
            objeler[0].transform.gameObject.GetComponent<Monster>().enabled = false;
            objeler[1].SetActive(true);

            other.transform.gameObject.GetComponent<PlayerController>().enabled = false;
            other.transform.gameObject.GetComponentInChildren<mouseLook>().enabled = false;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
