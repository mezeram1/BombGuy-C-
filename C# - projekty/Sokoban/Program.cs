using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Program
    {
        static int[,] VyrobSkladiste()
        {
            int[,] skladiste =new int[12,12];
            for (int i = 0; i < 12; i++)
            {
                skladiste[0,i] = -1;
                skladiste[11,i] = -1;
                skladiste[i,0] = -1;
                skladiste[i,11] = -1;
            }
            return skladiste;
        }
        static List<(int, int)> VyplnSkladiste(int[,] skladiste)
        {
            List<(int, int)> info = new List<(int, int)>();
            (int,int) skladnik = (0,0);
            (int,int) bedna = (0,0);
            (int,int) cil = (0,0);
            for (int i = 1; i < 11; i++)
            {
                string radek = Console.ReadLine();
                for (int j = 0; j < 10; j++)
                {
                    if (radek[j] == 'S')
                    {
                        skladnik = (i,j+1);
                    }
                    else if (radek[j] == 'B')
                    {
                        bedna = (i,j+1);
                    }
                    else if (radek[j] == 'C')
                    {
                        skladiste[i,j+1] = 'C';
                        cil = (i,j+1);
                    }
                    else if (radek[j] == 'X')
                    {
                        skladiste[i, j+1] = -1;
                    }
                }
            }
            info.Add(skladnik);
            info.Add(bedna);
            info.Add(cil);
            return info;
        }
        static void VypisSkladiste(int[,] skladiste, (int,int) skladnik, (int,int) bedna)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if ((i,j) == skladnik)
                        Console.Write("S ");
                    else if ((i,j) == bedna)
                        Console.Write("B ");
                    else if (skladiste[i,j] == -1)
                        Console.Write("1 ");
                    else                    
                       Console.Write("{0} ", skladiste[i,j]); 
                }
                Console.WriteLine();
            }
        }
        static bool JsemVohu(int[,] skladiste, (int, int) bedna)
        {
            if (skladiste[bedna.Item1 + 1,bedna.Item2] == -1 || skladiste[bedna.Item1 - 1,bedna.Item2] == -1)
            {
                if (skladiste[bedna.Item1,bedna.Item2 + 1] == -1 || skladiste[bedna.Item1,bedna.Item2 - 1] == -1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        static int MuzuTahnout(int[,] skladiste, (int,int) skladnik, (int,int) bedna, (int,int) prirustek, (int,int) cil)
        {
            if (skladiste[skladnik.Item1 + prirustek.Item1,skladnik.Item2 + prirustek.Item2] != -1)
            {
                if ((skladnik.Item1 + prirustek.Item1,skladnik.Item2 + prirustek.Item2) == bedna)
                {
                    if (skladiste[bedna.Item1 + prirustek.Item1, bedna.Item2 + prirustek.Item2] != -1)
                    {
                        if (skladnik.Item1 == bedna.Item1)
                        {
                            if (! JsemVohu(skladiste, (bedna.Item1, bedna.Item2 + prirustek.Item2)) || (bedna.Item1, bedna.Item2 + prirustek.Item2) ==  cil)
                                return 1;
                            else
                                return -1;
                        }
                        else
                        {
                            if (! JsemVohu(skladiste, (bedna.Item1 + prirustek.Item1, bedna.Item2)) || (bedna.Item1 + prirustek.Item1, bedna.Item2) ==  cil)
                                return 1;
                            else
                                return -1;
                        }
                    }
                    else
                        return -1;
                }
                else
                    return 0;
            }
            else
                return -1;
        }
        static void Hledej(int[,] skladiste, (int, int) skladnik, (int, int) bedna, (int, int) cil)
        {
            ((int,int),(int,int),int)[] fronta = new ((int, int), (int, int), int)[10000];
            Dictionary<((int,int),(int,int)),int> vyzkousene = new Dictionary<((int, int), (int, int)), int>();
            (int,int)[] tahy = {(1,0),(-1,0),(0,1),(0,-1)};
            vyzkousene.Add((skladnik, bedna), 0);
            int kroky = 0;
            int zacatek_fronty = -1;
            int konec_fronty = 0;
            while (bedna != cil && bedna != (0,0))
            {
                zacatek_fronty++;
                foreach (var tah in tahy)
                {
                    int muzu = MuzuTahnout(skladiste, skladnik, bedna, tah, cil);
                    if (muzu == 0)
                    {
                        if (! vyzkousene.ContainsKey(((skladnik.Item1 + tah.Item1, skladnik.Item2 + tah.Item2), bedna)))
                        {
                            vyzkousene.Add(((skladnik.Item1 + tah.Item1, skladnik.Item2 + tah.Item2), bedna),0);
                            fronta[konec_fronty] = ((skladnik.Item1 + tah.Item1, skladnik.Item2 + tah.Item2), bedna,kroky + 1);
                            konec_fronty++;
                        }
                    }
                    else if (muzu == 1)
                    {
                        if (! vyzkousene.ContainsKey(((skladnik.Item1 + tah.Item1, skladnik.Item2 + tah.Item2), (bedna.Item1 + tah.Item1, bedna.Item2 + tah.Item2))))
                        {
                            vyzkousene.Add(((skladnik.Item1 + tah.Item1, skladnik.Item2 + tah.Item2), (bedna.Item1 + tah.Item1, bedna.Item2 + tah.Item2)),0);
                            fronta[konec_fronty] = ((skladnik.Item1 + tah.Item1, skladnik.Item2 + tah.Item2), (bedna.Item1 + tah.Item1, bedna.Item2 + tah.Item2),kroky + 1);
                            konec_fronty++;
                        }
                    }
                }    
                skladnik = fronta[zacatek_fronty].Item1;
                bedna = fronta[zacatek_fronty].Item2;
                kroky = fronta[zacatek_fronty].Item3;
            }
            if (bedna == (0,0))
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(kroky);
            }
        }
        static void Main(string[] args)
        {
            int[,] skladiste = VyrobSkladiste();
            List<(int, int)> polozky = VyplnSkladiste(skladiste);
            (int, int) skladnik = polozky[0];
            (int, int) bedna = polozky[1];
            (int, int) cil = polozky[2];
            Hledej(skladiste, skladnik, bedna, cil);
        }
    }
}