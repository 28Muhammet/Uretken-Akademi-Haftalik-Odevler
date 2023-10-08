using System.Collections;
using UnityEngine;

public class GörevManager : MonoBehaviour
{
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

        for(int i = 0; i <= ilkGörev.Length; i++)
        {
            ilkGörev[i] = Enemys1[i].GetComponent<Animator>();
        }

        for (int i = 0; i <= ikinciGörev.Length; i++)
        {       
            ikinciGörev[i] = Enemys2[i].GetComponent<Animator>();
        }

        for (int i = 0; i <= ucuncuGörev.Length; i++)
        {
            ucuncuGörev[i] = Enemys3[i].GetComponent<Animator>();
        }
    }

    private void Update()
    {
        bool allDead = true;
        bool allDead2 = true;
        bool allDead3 = true;

        foreach(Animator ilkGorev in ilkGörev)
        {
            if (!ilkGorev.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            paneller[0].SetActive(false);
            StartCoroutine(Görev1Win());
        }


        foreach (Animator görevİki in ikinciGörev)
        {
            if (!görevİki.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead2 = false;
                break;
            }
        }

        if (allDead2)
        {
            StartCoroutine(Görev2Bilgi());
        }



        foreach (Animator görevUc in ucuncuGörev)
        {
            if (!görevUc.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                allDead3 = false;
                break;
            }
        }

        if (allDead3)
        {
            StartCoroutine(Görev3Bilgi());
        }
    }

    IEnumerator Görev1Win()
    {
        yield return new WaitForSeconds(12f);
        paneller[1].SetActive(false);

        yield return new WaitForSeconds(9f);
        paneller[1].SetActive(false);
    }

    IEnumerator Görev2Bilgi()
    {
        yield return new WaitForSeconds(12f);
        paneller[2].SetActive(true);

        yield return new WaitForSeconds(9f);
        paneller[2].SetActive(false);
    }

    IEnumerator Görev2Win()
    {
        yield return new WaitForSeconds(12f);
        paneller[3].SetActive(true);

        yield return new WaitForSeconds(9f);
        paneller[3].SetActive(false);
    }

    IEnumerator Görev3Bilgi()
    {
        yield return new WaitForSeconds(12f);
        paneller[4].SetActive(true);

        yield return new WaitForSeconds(9f);
        paneller[4].SetActive(false);
    }

    private IEnumerator YokEt()
    {
        yield return new WaitForSeconds(15);
        paneller[0].SetActive(false);
    }
}
