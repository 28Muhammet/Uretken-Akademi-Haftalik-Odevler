using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menü : MonoBehaviour
{
    //Tutulacak Layoutlar
    public GameObject anaMenüLayout;
    public GameObject basvuruLayout;
    public GameObject gönderildiLayout;
    public GameObject bilgilerimLayout;

    //Tutulacak bilgiler
    private string mail;
    private string ad;
    private string soyad;
    private int il;
    private string telefon;
    private string metin;

    //Tutulan Textler
    public TextMeshProUGUI t1;
    public TextMeshProUGUI t2;
    public TextMeshProUGUI t3;
    public TextMeshProUGUI t4;
    public TextMeshProUGUI t5;
    public TextMeshProUGUI t6;

    //===================================================

    //Buton işlemleri
    public void basvuruPanelGiris()
    {
        SceneManager.LoadScene(1);
    }

    public void temizleVeCik()
    {
        SceneManager.LoadScene(0);
    }

    public void gönderildi()
    {    
        gönderildiLayout.SetActive(true);
        basvuruLayout.SetActive(false);
    }

    public void bilgilerimiGörüntüle()
    {
        bilgilerimLayout.SetActive(true);
        gönderildiLayout.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
        gönderildiLayout.SetActive(false);
        bilgilerimLayout.SetActive(false);
    }

    //============================================================

    //Girilen değerleri tutma ve yazdırma
   public void inputName(string ad) 
   {
        this.ad = ad;
        t1.text = ad;
   }

   public void inputSoyad(string soyad)
   {
        this.soyad = soyad;
        t2.text = soyad;
   }


    public void inputTelefon(string telefon)
    {
        this.telefon = telefon;
        t3.text = telefon;
    }

    public void inputİl(int il)
    {
        this.il = il;
        if(il == 1)
        {
            print("İstanbul");
        }
        else if (il == 2)
        {
            print("Ankara");
        }
        else if(il == 3)
        {
            print("Antalya");
        }
        else if(il == 4)
        {
            print("Giresun");
        }

        t4.text = il.ToString();
    }

    public void inputMail(string mail)
    {
        this.mail = mail;
        t5.text = mail;
    }

    public void inputMetin(string metin)
    {
        this.metin = metin;
        t6.text = metin;
    }
}
