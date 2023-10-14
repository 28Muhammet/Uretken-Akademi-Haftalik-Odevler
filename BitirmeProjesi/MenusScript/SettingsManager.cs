using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioSource bgmSource; 
    public List<AudioSource> sfxSources; 

    public Slider bgmSlider; 
    public Slider sfxMasterSlider;

    public TextMeshProUGUI bgmPercentageText; 
    public TextMeshProUGUI sfxMasterPercentageText; 

    void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", .5f);
        SetBGMVolume(bgmSlider.value);

        sfxMasterSlider.value = PlayerPrefs.GetFloat("SFXMasterVolume", .7f);
        SetSFXMasterVolume(sfxMasterSlider.value);
    }

    void Update()
    {
        bgmPercentageText.text = (bgmSlider.value * 100).ToString("F0") + "%";
        sfxMasterPercentageText.text = (sfxMasterSlider.value * 100).ToString("F0") + "%";

        bgmSource.volume = bgmSlider.value;

        foreach (var source in sfxSources)
        {
            source.volume = sfxMasterSlider.value;
        }
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();

        bgmSource.volume = volume;
    }

    public void SetSFXMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXMasterVolume", volume);
        PlayerPrefs.Save();

        foreach (var source in sfxSources)
        {
            source.volume = volume;
        }
    }

    public void SetQuality(int qual)
    {
        QualitySettings.SetQualityLevel(qual);
    }
}
