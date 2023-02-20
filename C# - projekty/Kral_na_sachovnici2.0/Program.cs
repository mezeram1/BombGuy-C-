using System;

namespace cviko_2
{
    class Ctecka
    {
        static public int PrectiCislo()
        {
            int znak = Console.Read();
            int znam = 1;
            while ((znak < '0')||(znak > '9'))
            {
                if (znak == '-')
                    znam = -1;
                else
                    znam = 1;
                znak = Console.Read();
            }
            int vysledek = 0;
            while ((znak >= '0')&&(znak <= '9'))
            {
                vysledek = 10*vysledek + znak - '0';
                znak = Console.Read();
            }
            return znam*vysledek;
        }
    }
    class Sachovnice
    {
        public int[,] Pole = new int[10, 10];
        (int,int)[] tahy = {(1,1),(1,0),(1,-1),(0,1),(0,-1),(-1,1),(-1,-1),(-1,0)};
        (int,int,(int,int))[] fronta = new (int,int,(int,int))[64];
        public int zacatekFronty = 0;
        public int konecFronty = 0;

        public Sachovnice()
        {
            for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (i < 1 || i > 8 || j < 1 || j > 8)
                    this.Pole[i,j] = -1;
                else
                    this.Pole[i,j] = 0;
            }
        }
        }

        public void PridejPrekazku(int radek, int sloupec)
        {
            this.Pole[radek,sloupec] = -1;
        }

        public void PolozCil(int radek, int sloupec)
        {
            this.Pole[radek,sloupec] = 100;
        }
        void PridejDoFronty(int radek,int sloupec,(int,int) predchudce)
        {
            this.fronta[this.konecFronty] = (radek,sloupec,predchudce);
            this.konecFronty++;
        }

         public (int,int) VyzvedniZFronty()
        {
            (int,int) vrat = (this.fronta[this.zacatekFronty].Item1,this.fronta[this.zacatekFronty].Item2);
            this.zacatekFronty++;
            return vrat;
        }

        public void UmistiKrale(int radek,int sloupec)
        {
            this.Pole[radek,sloupec]++;
        }

        public bool Kraluj(int radek,int sloupec)
        {
            if (this.Pole[radek,sloupec] > 99)
                return false;
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (this.Pole[radek + this.tahy[i].Item1,sloupec + this.tahy[i].Item2] == 0 || this.Pole[radek + this.tahy[i].Item1,sloupec + this.tahy[i].Item2] == 100)
                    {
                        this.Pole[radek + this.tahy[i].Item1,sloupec + this.tahy[i].Item2] += this.Pole[radek,sloupec] + 1;
                        this.PridejDoFronty(radek + this.tahy[i].Item1,sloupec + this.tahy[i].Item2,(radek,sloupec));
                    }
                }
                return true;
            }
        }
        public (int,int)[] VyrobCestu((int,int) pred, int poc)
        {
            (int,int)[] cesta = new (int, int)[poc];
            int x = this.konecFronty - 1;
            for (int i = poc -1; i >= 0; i--)
            {
                cesta[i] = pred;
                for (int j = x; j >= 0; j--)
                {
                    if (this.fronta[j].Item1 == pred.Item1 && this.fronta[j].Item2 == pred.Item2)
                    {
                        pred = (this.fronta[j].Item3);
                        x = j;
                        break;
                    }
                }

            }
            return cesta;
        }
        public void VypisCestu((int,int)[] cesta, int delka)
        {
            for (int i = 0; i < delka; i++)
            {
                Console.WriteLine("{0} {1}",cesta[i].Item1, cesta[i].Item2);
            }
        }
        public void VypisSe()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write("{0} ",this.Pole[i,j]);
                }
                Console.WriteLine("");
            }
        }
    }
    class KralNaSachovnici
    {
        static void Main(string[] args)
        {
            Sachovnice sachovnice = new Sachovnice();
            int pocetPrekazek = Ctecka.PrectiCislo();
            for (int i = 0; i < pocetPrekazek; i++)
            {
                sachovnice.PridejPrekazku(Ctecka.PrectiCislo(),Ctecka.PrectiCislo());
            }
            int radek = Ctecka.PrectiCislo();
            int sloupec = Ctecka.PrectiCislo();
            sachovnice.PolozCil(Ctecka.PrectiCislo(),Ctecka.PrectiCislo());
            sachovnice.UmistiKrale(radek,sloupec);
            (int,int) pole;
            bool delej = true;
            while (sachovnice.Kraluj(radek,sloupec) && delej)
            {
                if (sachovnice.zacatekFronty != sachovnice.konecFronty)
                {
                    pole = sachovnice.VyzvedniZFronty();
                    radek = pole.Item1;
                    sloupec = pole.Item2;
                }
                else
                    delej = false;
            }
            if (delej)
            {
                sachovnice.VypisCestu(sachovnice.VyrobCestu((radek,sloupec), sachovnice.Pole[radek,sloupec] - 100),sachovnice.Pole[radek,sloupec] - 100);
            }
            else
                Console.WriteLine("{0}",-1);
        }
    }
}