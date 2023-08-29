using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private GameObject[] paneller;

    private bool hasCollided = false;

    private void Update()
    {
       if(!hasCollided)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            Vector3 moveDirection = directionToPlayer.normalized;
            Vector3 newPosition = transform.position + chaseSpeed * Time.deltaTime * moveDirection;
            transform.position = newPosition;

            transform.LookAt(player);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasCollided = true;
            paneller[0].SetActive(true);
            other.transform.gameObject.GetComponent<PlayerController>().enabled = false;
            other.transform.gameObject.GetComponentInChildren<mouseLook>().enabled = false;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
