using System;
using System.Collections.Generic;
using UnityEngine;

public class alistirma : MonoBehaviour
{
    void BolenleriBul(int sayi1, int sayi2)
    {
        List<int> bütündegerler = new List<int>();
        List<int> ikiyeBölünenler = new List<int>();
        List<int> üceBölünenler = new List<int>();
        List<int> dordeBölünenler = new List<int>();
        List<int> beseBölünenler = new List<int>();

        if(sayi1 < sayi2)
        {
            for (int i = sayi1; i <= sayi2; i++)
            {
                bütündegerler.Add(i);

                if (i % 2 == 0)
                {
                    ikiyeBölünenler.Add(i);
                }

                if (i % 3 == 0)
                {
                    üceBölünenler.Add(i);
                }

                if (i % 4 == 0)
                {
                    dordeBölünenler.Add(i);
                }

                if (i % 5 == 0)
                {
                    beseBölünenler.Add(i);
                }
            }

            String eleman = String.Join(",", bütündegerler.ToArray());
            String eleman2 = String.Join(",", ikiyeBölünenler.ToArray());
            String eleman3 = String.Join(",", üceBölünenler.ToArray());
            String eleman4 = String.Join(",", dordeBölünenler.ToArray());
            String eleman5 = String.Join(",", beseBölünenler.ToArray());

            print("İki sayı arasındaki değerler: " + eleman);
            print("İki' ye bölünebilen değerler: " + eleman2);
            print("Üçe bölünebilen değerler: " + eleman3);
            print("Dörde bölünebilen değerler: " + eleman4);
            print("Beşe bölünebilen değerler: " + eleman5);
        }
        else
        {
            for (int i = sayi1; i >= sayi2; i--)
            {
                bütündegerler.Add(i);

                if (i % 2 == 0)
                {
                    ikiyeBölünenler.Add(i);
                }

                if (i % 3 == 0)
                {
                    üceBölünenler.Add(i);
                }

                if (i % 4 == 0)
                {
                    dordeBölünenler.Add(i);
                }

                if (i % 5 == 0)
                {
                    beseBölünenler.Add(i);
                }
            }

            bütündegerler.Reverse();
            ikiyeBölünenler.Reverse();
            üceBölünenler.Reverse();
            dordeBölünenler.Reverse();
            beseBölünenler.Reverse();

            String eleman = String.Join(",", bütündegerler.ToArray());
            String eleman2 = String.Join(",", ikiyeBölünenler.ToArray());
            String eleman3 = String.Join(",", üceBölünenler.ToArray());
            String eleman4 = String.Join(",", dordeBölünenler.ToArray());
            String eleman5 = String.Join(",", beseBölünenler.ToArray());

            print("İki sayı arasındaki değerler: " + eleman);
            print("İki' ye bölünebilen değerler: " + eleman2);
            print("Üçe bölünebilen değerler: " + eleman3);
            print("Dörde bölünebilen değerler: " + eleman4);
            print("Beşe bölünebilen değerler: " + eleman5);
        }

       
    }

    private void Start()
    {
        BolenleriBul(15, 6);
    }
}
