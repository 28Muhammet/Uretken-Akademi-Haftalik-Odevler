using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MenüManager : MonoBehaviour
{
    [SerializeField] private Menu[] menus;
    public GameObject[] paneller;

    public Slider loadingSlider;
    public TextMeshProUGUI progressText;

    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                (menus[i]).Open();
            }
            else if(menus[i].open)
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EscBtn();
        }
    }

    public void StartBtn()
    {
        paneller[0].SetActive(true);
        ChangeScene("Character");
    }

    public void SettingsBtn()
    {
        OpenMenu("settingsMenu");
    }

    public void ProducersBtn()
    {
        OpenMenu("producersMenu");
    }

    public void BackBtn()
    {
        OpenMenu("mainMenu");
    }

    private void EscBtn()
    {
        for(int i = 0;i < menus.Length;i++)
        {
            menus[i].Close();
            menus[0].Open();
        }
    }

    public void AnimFinish()
    {
        OpenMenu("mainMenu");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            progressText.text = Mathf.Round(progress * 100f) + "%";

            yield return null;
        }
    }
}
