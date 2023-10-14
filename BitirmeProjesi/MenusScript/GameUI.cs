using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject[] paneller;
    [SerializeField] private Menu[] menus;

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                (menus[i]).Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paneller[5].activeSelf)
        {
            if (paneller[3].activeSelf)
            {
                paneller[3].SetActive(false);
                paneller[1].SetActive(false);
                paneller[2].SetActive(false);
                paneller[0].SetActive(true);

                Time.timeScale = 0;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (paneller[0].activeSelf)
            {

                paneller[0].SetActive(false);
                paneller[1].SetActive(true);
                paneller[2].SetActive(true);

                Time.timeScale = 1;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                paneller[0].SetActive(true);
                paneller[1].SetActive(false);
                paneller[2].SetActive(false);

                Time.timeScale = 0;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menü");
    }

    public void TryAgainBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackBtn()
    {
        paneller[0].SetActive(false);
        paneller[1].SetActive(true);
        paneller[2].SetActive(true);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackBtn2()
    {
        paneller[3].SetActive(false);
        paneller[0].SetActive(true);
    }
}
