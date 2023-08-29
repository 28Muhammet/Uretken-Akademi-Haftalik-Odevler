using UnityEngine;
using UnityEngine.SceneManagement;

public class MenüManager : MonoBehaviour
{
    public void Başla()
    {
        SceneManager.LoadScene(1);
    }

    public void MenüyeDön()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
