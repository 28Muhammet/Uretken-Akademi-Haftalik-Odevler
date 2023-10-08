using UnityEngine;
using UnityEngine.SceneManagement;

public class GörevPaneli : MonoBehaviour
{
    public void GörevPanel()
    {
        gameObject.SetActive(false);
    }

    public void AnaMenüyeDön()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
