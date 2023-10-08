using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenüSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI yüklemeYüzdesi;
    [SerializeField] private Image yüklemeBar;
    [SerializeField] private GameObject[] paneller;
    [SerializeField] private AudioSource source;

    private AsyncOperation asyncOperation;
    private bool escBasildimi = false;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        EscButonu("BüyükTaarruz");
    }

    public void BaşlaButonu(string sceneName)
    {
        paneller[4].SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
        source.Stop();
    }

    public void AyarlarButonu()
    {
        paneller[1].SetActive(false);
        paneller[2].SetActive(true);
    }

    public void YapımcılarButonu()
    {
        paneller[1].SetActive(false);
        paneller[3].SetActive(true);
        StartCoroutine(YapimcilerLoad());
    }

    private void EscButonu(string sceneName) 
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if (paneller[4].activeSelf)
            {
                escBasildimi = true;
                paneller[4].SetActive(false);
                paneller[0].SetActive(true);
                StartCoroutine(LoadSceneAsync(sceneName));
            }
            else
            {              
                for (int i = 0; i < paneller.Length; i++)
                {
                    paneller[i].SetActive(false);
                }

                paneller[1].SetActive(true);
            }
        }
    }

    public void GeriButonu()
    {
        paneller[2].SetActive(false);
        paneller[1].SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (escBasildimi == true)
        {
            asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                paneller[4].SetActive(false);
                paneller[1].SetActive(false);
                paneller[0].SetActive(true);
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                yüklemeYüzdesi.text = (progress * 100f).ToString("F0") + "%";
                yüklemeBar.fillAmount = progress;

                if (progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(57);
            paneller[4].SetActive(false);
            asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                paneller[1].SetActive(false);
                paneller[0].SetActive(true);
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                yüklemeYüzdesi.text = (progress * 100f).ToString("F0") + "%";
                yüklemeBar.fillAmount = progress;

                if (progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }

    private IEnumerator YapimcilerLoad()
    {
        yield return new WaitForSeconds(15f);
        paneller[3].SetActive(false);
        paneller[1].SetActive(true);
    }
}
