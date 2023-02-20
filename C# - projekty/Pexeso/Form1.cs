namespace WinFormsApp1
{
    class Karticka : System.Windows.Forms.Button
    {

        public enum STAVKARTY { TEXT_NAHORU, PEXESO, UHADNUTA };
        public STAVKARTY stav = STAVKARTY.PEXESO;
    }
    public partial class Form1 : Form
    {
        int dvojice = 18;
        int pocetTahu = 0;
        int[] hodnoty = new int[36];
        List<Karticka> dve_otocene = new List<Karticka>();
        Karticka[] karticky = new Karticka[36];
        bool NovaHra = false;
        enum STAV { START, HRA, JEDNA, DVE, VYHRA };
        STAV stav;
        public Form1()
        {
            for (int i = 0; i < 36; i++)
                hodnoty[i] = i % 18 + 1;
            Shuffle(hodnoty);
            InitializeComponent();
            NastavStav(STAV.START);
        }
        private static Random rng = new Random();
        // Tato funkce je zkopírovaná z: https://www.codegrepper.com/code-examples/csharp/c%23+how+to+shuffle+a+list

        public static void Shuffle(int[] list)
        {
            int n = 36;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        void NastavStav(STAV novyStav)
        {
            switch (novyStav)
            {
                case STAV.START:
                    lUvod.Visible = true;
                    lSkore.Visible = false;
                    lTahy.Visible = false;
                    bStart.Visible = true;
                    bNovaHra.Visible = false;
                    stav = novyStav;
                    break;
                case STAV.HRA:
                    lUvod.Visible = false;
                    lSkore.Visible = false;
                    lTahy.Visible = true;
                    bStart.Visible = false;
                    bNovaHra.Visible = true;
                    stav = novyStav;
                    break;
                case STAV.JEDNA:
                    if (NovaHra)
                    {
                        PripravHru();
                    }
                    lUvod.Visible = false;
                    lSkore.Visible = false;
                    lTahy.Visible = true;
                    bStart.Visible = false;
                    bNovaHra.Visible = true;
                    stav = novyStav;
                    NovaHra = false;
                    break;
                case STAV.DVE:
                    lUvod.Visible = false;
                    lSkore.Visible = false;
                    lTahy.Visible = true;
                    bStart.Visible = false;
                    bNovaHra.Visible = true;
                    stav = novyStav;
                    break;
                case STAV.VYHRA:
                    ZnicKarticky();
                    bNovaHra.Location = new System.Drawing.Point(250, 225);
                    bNovaHra.Size = new System.Drawing.Size(258, 98);
                    lUvod.Text = "VYHÁL JSTE";
                    lUvod.Location = new System.Drawing.Point(125, 50);
                    lUvod.Visible = true;
                    lSkore.Visible = true;
                    lTahy.Visible = false;
                    bStart.Visible = false;
                    bNovaHra.Visible = true;
                    stav = novyStav;
                    break;
                default:
                    break;
            }
        }

        private void PripravHru()
        {
            Shuffle(hodnoty);
            pocetTahu = -1;
            dvojice = 18;
            lSkore.Text = "SKÓRE: ";
            ZvysPocetTahu();
            bNovaHra.Location = new System.Drawing.Point(618, 22);
            bNovaHra.Size = new System.Drawing.Size(147, 41);
            ZnicKarticky();
            VytvorKarticky();
        }
        private void bStart_Click(object sender, EventArgs e)
        {
            NovaHra = true;
            NastavStav(STAV.HRA);
        }

        private void bNovaHra_Click(object sender, EventArgs e)
        {
            NovaHra = true;
            NastavStav(STAV.JEDNA);
        }
        private void Karticka_Click(object sender, EventArgs e)
        {
            Karticka k = (Karticka)sender;
            switch (dve_otocene.Count)
            {
                case 0:
                    if (k.stav == Karticka.STAVKARTY.PEXESO)
                    {
                        k.Text = k.Tag.ToString();
                        k.stav = Karticka.STAVKARTY.TEXT_NAHORU;
                        dve_otocene.Add(k);
                    }
                    NastavStav(STAV.JEDNA);
                    break;
                case 1:
                    if (k.stav == Karticka.STAVKARTY.PEXESO)
                    {
                        ZvysPocetTahu();
                        k.Text = k.Tag.ToString();
                        k.stav = Karticka.STAVKARTY.TEXT_NAHORU;
                        dve_otocene.Add(k);
                        NastavStav(STAV.DVE);
                        if (dve_otocene[0].Text == dve_otocene[1].Text)
                        {
                            dvojice--;
                            dve_otocene[0].stav = Karticka.STAVKARTY.UHADNUTA;
                            dve_otocene[1].stav = Karticka.STAVKARTY.UHADNUTA;
                            if (dvojice == 0)
                            { 
                                lSkore.Text += pocetTahu.ToString();
                                NastavStav(STAV.VYHRA);
                            }
                        }
                        else
                        {
                            wait(500);
                            dve_otocene[0].stav = Karticka.STAVKARTY.PEXESO;
                            dve_otocene[1].stav = Karticka.STAVKARTY.PEXESO;
                            dve_otocene[0].Text = "PEXESO";
                            dve_otocene[1].Text = "PEXESO";
                            NastavStav(STAV.JEDNA);
                        }
                        dve_otocene = new List<Karticka>();
                    }
                    break;

            }
        }

        // Tato funkce je zkopírovaná z: https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        void ZvysPocetTahu()
        {
            pocetTahu++;
            lTahy.Text = pocetTahu.ToString();
        }

        void VytvorKarticky()
        {
            int N = 6;
            int sx = ClientRectangle.Width / N;
            int y0 = lTahy.Top + lTahy.Height;
            int sy = (ClientRectangle.Height - y0) / N;

            for (int x = 0; x < N; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    Karticka b = new Karticka();
                    b.Width = sx;
                    b.Height = sy;
                    b.Left = x * sx;
                    b.Top = y0 + y * sy;
                    b.Text = "PEXESO";
                    b.Parent = this;
                    b.stav = Karticka.STAVKARTY.PEXESO;
                    b.Click += Karticka_Click;
                    b.Tag = hodnoty[N * x + y];
                    karticky[N * x + y] = b;
                }
            }
        }
        void ZnicKarticky()
        {
            for (int x = 0; x < 36; x++)
            {
                Controls.Remove(karticky[x]);
            }
        }
    }
}