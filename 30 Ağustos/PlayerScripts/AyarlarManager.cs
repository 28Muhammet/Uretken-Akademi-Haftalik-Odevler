using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AyarlarManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sesSeviyesi;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject[] obje;

    private void Start()
    {
        LoadAudio();
    }

    public void Quality(int qual)
    {
        QualitySettings.SetQualityLevel(qual);
    }

    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        sesSeviyesi.text = ((int)(value * 100)).ToString() + ("%");
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", AudioListener.volume);
    }

    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("audioVolume", .7f);
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
    }

    public void AnaMenüyeDön()
    {
        SceneManager.LoadScene(0);
    }

    public void GeriButonu()
    {
        obje[0].SetActive(true);
        obje[1].SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
