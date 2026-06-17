using System;
using System.IO;

public class GenerareAranjamente
{
    static int n, k;
    static int[] sol;
    static bool[] folosit;
    static StreamWriter sw;

    // Folosim parametrii 'pare' și 'impare' pentru a ține numărătoarea la fiecare pas
    static void Backtracking(int pas, int pare, int impare)
    {
        // Condiția de final: am pus k elemente în vector
        if (pas >= k)
        {
            // Scriem soluția în fișier
            for (int i = 0; i < k; i++)
            {
                sw.Write(sol[i] + " ");
            }
            sw.WriteLine();
            return;
        }

        // Luăm elemente din mulțimea {1, 2, ..., n}
        for (int i = 1; i <= n; i++)
        {
            if (!folosit[i]) // Dacă numărul nu a mai fost folosit în soluția curentă
            {
                int estePar = (i % 2 == 0) ? 1 : 0;
                int esteImpar = (i % 2 != 0) ? 1 : 0;

                // TRUC DE OPTIMIZARE (Pruning): 
                // Continuăm doar dacă nu depășim limita de k/2 pentru oricare dintre parități.
                // Asta face programul mult mai rapid, nearuncând timp pe ramuri moarte.
                if (pare + estePar <= k / 2 && impare + esteImpar <= k / 2)
                {
                    sol[pas] = i;
                    folosit[i] = true; // Marcăm elementul ca fiind luat

                    // Trecem la pasul următor, actualizând numărătoarea
                    Backtracking(pas + 1, pare + estePar, impare + esteImpar);

                    folosit[i] = false; // Eliberăm elementul la întoarcere
                }
            }
        }
    }

    public static void Main()
    {
        // Citirea de la tastatură
        Console.Write("n = ");
        n = int.Parse(Console.ReadLine());

        Console.Write("k = ");
        k = int.Parse(Console.ReadLine());

        // Inițializăm vectorii. 
        // 'folosit' este de mărime n + 1 pentru că lucrăm cu valorile de la 1 la n
        sol = new int[k];
        folosit = new bool[n + 1];

        // Inițializăm scrierea în fișier (trebuie adăugat using System.IO; la început)
        sw = new StreamWriter("aranjamente.txt");

        // Apelul inițial: suntem la pasul 0, cu 0 pare și 0 impare
        Backtracking(0, 0, 0);

        // Foarte important pe foaia de examen: să arăți că știi să închizi fluxul!
        sw.Close();
    }
}