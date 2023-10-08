using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GörevSistemi : MonoBehaviour
{
    [SerializeField] private GameObject[] yunanAskerleri;
    [SerializeField] private GameObject[] yunanAskerleri2;
    [SerializeField] private GameObject[] panaller;
    [SerializeField] private Image targetImage;

    private Animator[] _yunanAskerAnimators;
    private Animator[] _yunanAskerAnimators2;
    private float transparencyIncreaseAmount = 0.1f;
    private bool isObjAActive = true;

    private void Start()
    {
        _yunanAskerAnimators = new Animator[yunanAskerleri.Length];
        _yunanAskerAnimators2 = new Animator[yunanAskerleri2.Length];

        for (int i = 0; i < yunanAskerleri.Length; i++)
        {
            _yunanAskerAnimators[i] = yunanAskerleri[i].GetComponent<Animator>();
        }

        for (int i = 0; i < yunanAskerleri2.Length; i++)
        {
            _yunanAskerAnimators2[i] = yunanAskerleri2[i].GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isObjAActive)
            {
                panaller[7].SetActive(true);
                panaller[8].SetActive(false);
                panaller[9].SetActive(false);

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                panaller[9].SetActive(true);
                panaller[8].SetActive(true);
                panaller[7].SetActive(false);

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
            isObjAActive = !isObjAActive;
        }

        bool allDead = true;
        bool allDead2 = true;

        foreach (Animator yunanAnim in _yunanAskerAnimators)
        {
            if (!yunanAnim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            StartCoroutine(YokEt());
            panaller[1].SetActive(true);
            panaller[2].SetActive(true);
            StartCoroutine(Görev2Bilgi());
        }


        foreach (Animator yunanAnim in _yunanAskerAnimators2)
        {
            if (!yunanAnim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead2 = false;
                break;
            }
        }

        if (allDead2)
        {
            panaller[4].SetActive(true);
            StartCoroutine(Winner());
        }
    }

    IEnumerator Görev2Bilgi()
    {
        yield return new WaitForSeconds(12f);
        panaller[3].SetActive(true);
        panaller[2].SetActive(false);
    }

    IEnumerator Winner()
    {
        yield return new WaitForSeconds(4f);
        Color currentColor = targetImage.color;
        StopAllSounds();

        float newAlpha = Mathf.Clamp01(currentColor.a + transparencyIncreaseAmount);
        currentColor.a = newAlpha;

        targetImage.color = currentColor;

        yield return new WaitForSeconds(5.3f);
        panaller[5].SetActive(false);
        panaller[6].SetActive(true);
    }

    private void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }

    private IEnumerator YokEt()
    {
        yield return new WaitForSeconds(15);
        panaller[0].SetActive(false);
    }
}
