using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GörevManager : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    private float transparencyIncreaseAmount = 0.002f;

    [SerializeField] private GameObject[] Enemys1;
    [SerializeField] private GameObject[] Enemys2;
    [SerializeField] private GameObject[] Enemys3;
    [SerializeField] private GameObject[] paneller;

    private Animator[] ilkGörev;
    private Animator[] ikinciGörev;
    private Animator[] ucuncuGörev;

    private void Start()
    {
        ilkGörev = new Animator[Enemys1.Length];
        ikinciGörev = new Animator[Enemys2.Length];
        ucuncuGörev = new Animator[Enemys3.Length];

        for(int i = 0; i < ilkGörev.Length; i++)
        {
            ilkGörev[i] = Enemys1[i].GetComponent<Animator>();
        }

        for (int i = 0; i < ikinciGörev.Length; i++)
        {       
            ikinciGörev[i] = Enemys2[i].GetComponent<Animator>();
        }

        for (int i = 0; i < ucuncuGörev.Length; i++)
        {
            ucuncuGörev[i] = Enemys3[i].GetComponent<Animator>();
        }
    }

    private void Update()
    {
        bool allDead = true;
        bool allDead2 = true;
        bool allDead3 = true;

        StartCoroutine(Gorev1False());

        foreach (Animator ilkGorev in ilkGörev)
        {
            if (!ilkGorev.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            StartCoroutine(Görev1Win());
            StartCoroutine(Görev2Bilgi());
        }

        foreach (Animator görevİki in ikinciGörev)
        {
            if (!görevİki.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead2 = false;
                break;
            }

            if (allDead2)
            {
                StartCoroutine(Görev2Win());
                StartCoroutine(Görev3Bilgi());
            }
        }

        foreach (Animator görevUc in ucuncuGörev)
        {
            if (!görevUc.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead3 = false;
                break;
            }

            if (allDead3)
            {
                StartCoroutine(Winner());
            }
        }       
    }

    IEnumerator Görev1Win()
    {
        yield return new WaitForSeconds(12f);
        paneller[1].SetActive(true);
    }

    IEnumerator Görev2Bilgi()
    {
        yield return new WaitForSeconds(24f);
        paneller[2].SetActive(true);
        paneller[1].SetActive(false);
        StartCoroutine(Gorev2False());
    }

    IEnumerator Görev2Win()
    {
        yield return new WaitForSeconds(12f);
        paneller[3].SetActive(true);
    }

    IEnumerator Görev3Bilgi()
    {
        yield return new WaitForSeconds(24f);
        paneller[4].SetActive(true);
        paneller[3].SetActive(false);
        StartCoroutine(Gorev3False());
    }

    IEnumerator Winner()
    {
        yield return new WaitForSeconds(6f);
        Color currentColor = targetImage.color;
        paneller[5].SetActive(false);
        StopAllSounds();

        float newAlpha = Mathf.Clamp01(currentColor.a + transparencyIncreaseAmount);
        currentColor.a = newAlpha;

        targetImage.color = currentColor;

        yield return new WaitForSeconds(5f);
        paneller[6].SetActive(true);
    }

    private void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }

    private IEnumerator Gorev1False()
    {
        yield return new WaitForSeconds(9f);
        paneller[0].SetActive(false);
    }

    private IEnumerator Gorev2False()
    {
        yield return new WaitForSeconds(9f);
        paneller[2].SetActive(false);
    }

    private IEnumerator Gorev3False()
    {
        yield return new WaitForSeconds(9f);
        paneller[4].SetActive(false);
    }
}
