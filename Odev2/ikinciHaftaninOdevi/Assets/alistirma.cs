using System;
using System.Collections.Generic;
using UnityEngine;

public class alistirma : MonoBehaviour
{
    void BolenleriBul(int sayi1, int sayi2)
    {
        List<int> b�t�ndegerler = new List<int>();
        List<int> ikiyeB�l�nenler = new List<int>();
        List<int> �ceB�l�nenler = new List<int>();
        List<int> dordeB�l�nenler = new List<int>();
        List<int> beseB�l�nenler = new List<int>();

        if(sayi1 < sayi2)
        {
            for (int i = sayi1; i <= sayi2; i++)
            {
                b�t�ndegerler.Add(i);

                if (i % 2 == 0)
                {
                    ikiyeB�l�nenler.Add(i);
                }

                if (i % 3 == 0)
                {
                    �ceB�l�nenler.Add(i);
                }

                if (i % 4 == 0)
                {
                    dordeB�l�nenler.Add(i);
                }

                if (i % 5 == 0)
                {
                    beseB�l�nenler.Add(i);
                }
            }

            String eleman = String.Join(",", b�t�ndegerler.ToArray());
            String eleman2 = String.Join(",", ikiyeB�l�nenler.ToArray());
            String eleman3 = String.Join(",", �ceB�l�nenler.ToArray());
            String eleman4 = String.Join(",", dordeB�l�nenler.ToArray());
            String eleman5 = String.Join(",", beseB�l�nenler.ToArray());

            print("�ki say� aras�ndaki de�erler: " + eleman);
            print("�ki' ye b�l�nebilen de�erler: " + eleman2);
            print("��e b�l�nebilen de�erler: " + eleman3);
            print("D�rde b�l�nebilen de�erler: " + eleman4);
            print("Be�e b�l�nebilen de�erler: " + eleman5);
        }
        else
        {
            for (int i = sayi1; i >= sayi2; i--)
            {
                b�t�ndegerler.Add(i);

                if (i % 2 == 0)
                {
                    ikiyeB�l�nenler.Add(i);
                }

                if (i % 3 == 0)
                {
                    �ceB�l�nenler.Add(i);
                }

                if (i % 4 == 0)
                {
                    dordeB�l�nenler.Add(i);
                }

                if (i % 5 == 0)
                {
                    beseB�l�nenler.Add(i);
                }
            }

            b�t�ndegerler.Reverse();
            ikiyeB�l�nenler.Reverse();
            �ceB�l�nenler.Reverse();
            dordeB�l�nenler.Reverse();
            beseB�l�nenler.Reverse();

            String eleman = String.Join(",", b�t�ndegerler.ToArray());
            String eleman2 = String.Join(",", ikiyeB�l�nenler.ToArray());
            String eleman3 = String.Join(",", �ceB�l�nenler.ToArray());
            String eleman4 = String.Join(",", dordeB�l�nenler.ToArray());
            String eleman5 = String.Join(",", beseB�l�nenler.ToArray());

            print("�ki say� aras�ndaki de�erler: " + eleman);
            print("�ki' ye b�l�nebilen de�erler: " + eleman2);
            print("��e b�l�nebilen de�erler: " + eleman3);
            print("D�rde b�l�nebilen de�erler: " + eleman4);
            print("Be�e b�l�nebilen de�erler: " + eleman5);
        }

       
    }

    private void Start()
    {
        BolenleriBul(15, 6);
    }
}
