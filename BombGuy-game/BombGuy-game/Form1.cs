using Timer = System.Windows.Forms.Timer;

namespace BombGuy_game
{
    //Game form
    public partial class Game : Form
    {
        //Brick is a destroyable barrier on map
        class Brick
        {
            public int x;
            public int y;
            Rectangle rect;
            Graphics g;
            public Brick(int x, int y, Rectangle rect, Graphics g)
            {
                this.x = x;
                this.y = y;
                this.rect = rect;
                this.g = g;
            }
            //Function that destroys brick
            public void Destroy()
            {
                g.DrawRectangle(new Pen(Color.DimGray), this.rect);
                g.FillRectangle(Brushes.DimGray, this.rect);
                Map.map[y, x] = '.';
            }
        }
        //class for visualiuing bomb explosion
        class Explosion : PictureBox
        {
            PictureBox pictureBox;
            int x;
            int y;
            Timer timer = new Timer();
            public Explosion(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.Image = Properties.Resources.explosion;
                this.Location = new Point(this.x, this.y + 2);
                this.Size = new Size(50, 50);
                this.BackColor = Color.DimGray;
                timer.Start();
                timer.Interval = 1000;
                timer.Tick += new EventHandler(this.tick);
            }
            void tick(Object o, EventArgs e)
            {
                timer.Stop();
                timer.Dispose();
                this.Dispose();
            }
        }
        //class for visualizing bomb
        class Bomb : PictureBox
        {
            public int x;
            public int y;
            public bool explosed = false;
            int Range = 4;

            public Bomb(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.Image = Properties.Resources.bomb;
                this.BackColor = Color.DimGray;
                this.Location = new Point(x, y);
                this.Size = new Size(50, 50);
            }
        }
        //player class
        class Player : PictureBox
        {
            int last_bomb = 0;
            int speed;
            int lives = 3;
            public string text;
            public bool living = true;
            bool unhittable = false;
            int tick = 0;
            Bitmap image_left;
            Bitmap image_right;
            Label label = new Label();
            bool turned_right;
            Timer hitTimer;
            List<Timer> timers = new List<Timer>();
            public List<Bomb> l_bombs = new List<Bomb>();
            public List<Explosion> explosions = new List<Explosion>();
            int bombs = 5;
            public bool goLeft, goRight, goUp, goDown, makeBomb = false;
            Timer t = new Timer();
            public Player(int x, int y, string name, Bitmap image_left, Bitmap image_right, bool turned_right, Label label, string text)
            {
                this.label = label;
                this.text = text;
                this.label.Text = text + ":" + lives.ToString();
                this.Location = new Point(x, y);
                this.Name = name;
                this.Size = new Size(48, 48);
                this.Image = image_left;
                this.BackColor = Color.DimGray;
                this.image_left = image_left;
                this.image_right = image_right;
                this.turned_right = turned_right;
                if (turned_right)
                    this.Image = image_right;
                this.speed = 5;
            }
            //moving functions
            public void GoLeft()
            {
                this.Image = this.image_left;
                if (this.Location.Y % 50 == 2 && this.Location.X > 150 && (this.Location.X % 50 != 0 || Game.Map.map[this.Location.Y / 50, this.Location.X / 50 - 4] == '.'))
                {
                    if (!this.turned_right)
                        this.Left -= this.speed;
                    else
                        this.turned_right = false;
                }
            }
            public void GoRight()
            {
                this.Image = this.image_right;
                if (this.Location.Y % 50 == 2 && this.Location.X < 850 && (this.Location.X % 50 != 0 || Game.Map.map[this.Location.Y / 50, this.Location.X / 50 - 2] == '.'))
                {
                    if (this.turned_right)
                        this.Left += this.speed;
                    else
                        this.turned_right = true;
                }
            }
            public void GoUp()
            {
                if (this.Location.X % 50 == 0 && this.Location.Y > 2 && (this.Location.Y % 50 != 2 || Game.Map.map[this.Location.Y / 50 - 1, this.Location.X / 50 - 3] == '.'))
                    this.Top -= this.speed;
            }
            public void GoDown()
            {
                if (this.Location.X % 50 == 0 && this.Location.Y < 502
                    && (this.Location.Y % 50 != 2 || Game.Map.map[this.Location.Y / 50 + 1, this.Location.X / 50 - 3] == '.'))
                    this.Top += this.speed;
            }
            public void MakeMove()
            {
                if (living)
                {
                    if (this.goLeft)
                        this.GoLeft();
                    if (this.goRight)
                        this.GoRight();
                    if (this.goUp)
                        this.GoUp();
                    if (this.goDown)
                        this.GoDown();
                }
            }
            //function that is called if player was hit by bomb
            public void Hit()
            {
                if (!unhittable)
                {
                    lives--;
                    this.label.Text = this.text + ":" + lives.ToString();
                    if (lives == 0)
                        this.Die();
                    else
                    {
                        unhittable = true;
                        hitTimer = new Timer();
                        hitTimer.Interval = 200;
                        hitTimer.Start();
                        hitTimer.Tick += new EventHandler(this.HitTick);
                        tick = 0;
                    }
                }
            }
            void HitTick(Object o, EventArgs e)
            {
                tick++;
                if (tick == 10)
                {
                    hitTimer.Stop();
                    this.Show();
                    unhittable = false;
                }
                else if (tick % 2 == 1)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }
            }
            //this function kills player
            void Die()
            {
                living = false;
                Map.results[Map.Deaths] = this.text;
                Map.Deaths++;
                if (turned_right)
                    this.Image = Properties.Resources.ghost_right;
                else
                    this.Image = Properties.Resources.ghost_left;
                t.Interval = 2000;
                t.Start();
                t.Tick += new EventHandler(this.Destroy);
            }
            //this function removes death player from map
            void Destroy(Object o, EventArgs e)
            {
                t.Stop();
                t.Dispose();
                this.Dispose();
            }
            //function for creating a bomb by player
            public void MakeBomb()
            {
                if (this.living && bombs > 0 && Map.map[this.Location.Y / 50, this.Location.X / 50 - 3] == '.')
                {
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Interval = 3000;
                    timer.Start();
                    Bomb b = new Bomb(this.Location.X - this.Location.X % 50, this.Location.Y - this.Location.Y % 50 + 2);
                    Map.map[b.y / 50, b.x / 50 - 3] = 'b';
                    timer.Tick += new EventHandler(this.Boom);
                    this.timers.Add(timer);
                    this.l_bombs.Add(b);
                    bombs--;
                }
            }
            //function that starts bomb explosion
            private void Boom(Object MyObject, EventArgs myEventArgs)
            {

                Bomb bomb = l_bombs[last_bomb];
                timers[last_bomb].Stop();
                timers[last_bomb].Dispose();
                bomb.Dispose();
                Map.map[bomb.y / 50, bomb.x / 50 - 3] = '.';
                Game.Map.Explode(bomb);
                last_bomb++;
                this.bombs++;
            }
        }
        //class that knows everything about map
        static class Map
        {
            static public List<Explosion> explosions = new List<Explosion>();
            static public List<Brick> bricks = new List<Brick>();
            static public List<Player> players = new List<Player>();
            static int range = 4;
            static public int playersCount = 0;
            static public string[] results = new string[4];
            static public int Deaths = 0;
            static public char[,] o_map = {
            {'.','.','.','.','.','.','B','B','B','.','.','.','.','.','.'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'.','B','.','B','B','.','.','.','.','.','B','B','.','B','.'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'B','.','.','.','.','.','.','.','.','.','.','.','.','.','B'},
            {'B','W','.','W','.','W','.','W','.','W','.','W','.','W','B'},
            {'B','.','.','.','.','.','.','.','.','.','.','.','.','.','B'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'.','B','.','B','B','.','.','.','.','.','B','B','.','B','.'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'.','.','.','.','.','.','B','B','B','.','.','.','.','.','.'},
            };
            static public char[,] map = {
            {'.','.','.','.','.','.','B','B','B','.','.','.','.','.','.'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'.','B','.','B','B','.','.','.','.','.','B','B','.','B','.'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'B','.','.','.','.','.','.','.','.','.','.','.','.','.','B'},
            {'B','W','.','W','.','W','.','W','.','W','.','W','.','W','B'},
            {'B','.','.','.','.','.','.','.','.','.','.','.','.','.','B'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'.','B','.','B','B','.','.','.','.','.','B','B','.','B','.'},
            {'.','W','B','W','.','W','.','W','.','W','.','W','B','W','.'},
            {'.','.','.','.','.','.','B','B','B','.','.','.','.','.','.'},
            };
            //function that makes explosion of bombs and destroy everything in bomb radius
            static public void Explode(Bomb b)
            {
                int column = b.x / 50 - 3;
                int row = b.y / 50;
                int i = 0;
                (int, int, int, int) explosionArea = (0, 0, 0, 0);
                while (column + i < 15 && i <= range && (Map.map[row, column + i] == '.' || Map.map[row, column + i] == 'b'))
                {
                    explosions.Add(new Explosion((column + i + 3) * 50, row * 50));
                    i++;
                }
                if (i <= range && column + i < 15 && Map.map[row, column + i] == 'B')
                {
                    explosionArea.Item1 = i;
                    explosions.Add(new Explosion((column + i + 3) * 50, row * 50));
                    foreach (var brick in bricks)
                    {
                        if (brick.y == row && brick.x == column + i)
                        {
                            bricks.Remove(brick);
                            brick.Destroy();
                            break;
                        }
                    }
                }
                else
                    explosionArea.Item1 = i - 1;
                i = 0;
                while (column - i >= 0 && i <= range && (Map.map[row, column - i] == '.' || Map.map[row, column - i] == 'b'))
                {
                    explosions.Add(new Explosion((column - i + 3) * 50, row * 50));
                    i++;
                }
                if (i <= range && column - i >= 0 && Map.map[row, column - i] == 'B')
                {
                    explosionArea.Item2 = i;
                    explosions.Add(new Explosion((column - i + 3) * 50, row * 50));
                    foreach (var brick in bricks)
                    {
                        if (brick.y == row && brick.x == column - i)
                        {
                            bricks.Remove(brick);
                            brick.Destroy();
                            break;
                        }
                    }
                }
                else
                    explosionArea.Item2 = i - 1;
                i = 0;
                while (row + i < 11 && i <= range && (Map.map[row + i, column] == '.' || Map.map[row + i, column] == 'b'))
                {
                    explosions.Add(new Explosion((column + 3) * 50, (row + i) * 50));
                    i++;
                }
                if (i <= range && row + i < 11 && Map.map[row + i, column] == 'B')
                {
                    explosionArea.Item3 = i;
                    explosions.Add(new Explosion((column + 3) * 50, (row + i) * 50));
                    foreach (var brick in bricks)
                    {
                        if (brick.y == row + i && brick.x == column)
                        {
                            bricks.Remove(brick);
                            brick.Destroy();
                            break;
                        }
                    }
                }
                else
                    explosionArea.Item3 = i - 1;
                i = 0;
                while (row - i >= 0 && i <= range && (Map.map[row - i, column] == '.' || Map.map[row - i, column] == 'b'))
                {
                    explosions.Add(new Explosion((column + 3) * 50, (row - i) * 50));
                    i++;
                }
                if (i <= range && row - i >= 0 && Map.map[row - i, column] == 'B')
                {
                    explosionArea.Item4 = i;
                    explosions.Add(new Explosion((column + 3) * 50, (row - i) * 50));
                    foreach (var brick in bricks)
                    {
                        if (brick.y == row - i && brick.x == column)
                        {
                            bricks.Remove(brick);
                            brick.Destroy();
                            break;
                        }
                    }
                }
                else
                    explosionArea.Item4 = i - 1;
                foreach (var player in players)
                {
                    if (player.living && player.Location.Y < b.y + 50 && player.Location.Y > b.y - 50 && player.Location.X < b.x + 50 * (explosionArea.Item1 + 1) && player.Location.X > b.x - 50 * (explosionArea.Item2 + 1))
                        player.Hit();
                    else if (player.living && player.Location.Y < b.y + 50 * (explosionArea.Item3 + 1) && player.Location.Y > b.y - 50 * (explosionArea.Item4 + 1) && player.Location.X < b.x + 50 && player.Location.X > b.x - 50)
                        player.Hit();
                }
            }
            //function that reset map to original statement 
            static public void ResetMap()
            {
                Map.Deaths = 0;
                Map.players = new List<Player>();
                Map.bricks = new List<Brick>();
                Map.explosions = new List<Explosion>();
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        map[i, j] = o_map[i, j];
                    }
                }
            }
            //function for creating players
            static public void CreatePlayers(int playersCount, Label[] labels)
            {
                Map.playersCount = playersCount;
                Map.players.Add(new Player(150, 2, "captain", Properties.Resources.captain_left, Properties.Resources.captain_right, true, labels[0], "P1"));
                Map.players.Add(new Player(850, 502, "ironman", Properties.Resources.ironman_left, Properties.Resources.ironman_right, false, labels[1], "P2"));
                if (playersCount > 2)
                {
                    Map.players.Add(new Player(150, 502, "thor", Properties.Resources.thor_left, Properties.Resources.thor_right, true, labels[2], "P3"));
                }
                if (playersCount > 3)
                    Map.players.Add(new Player(850, 2, "hawkey", Properties.Resources.hawkey_left, Properties.Resources.hawkey_right, false, labels[3], "P4"));
            }
        }
        public Game(int playersCount)
        {
            InitializeComponent();
            Map.ResetMap();
            GameOver = false;
            Label[] labels = { lPlayer1, lPlayer2, lPlayer3, lPlayer4};
            Map.CreatePlayers(playersCount, labels);
            brush1 = Brushes.Orange;
            brush2 = Brushes.Yellow;
            bmp = new Bitmap(pMap.Width, pMap.Height);
            pMap.Image = bmp;
            g = Graphics.FromImage(bmp);
        }
        int time = 0;
        bool GameOver = false;
        string winner = "";
        Bitmap bmp;
        Graphics g;
        Brush brush1;
        Brush brush2;
        void Form1_Load(object sender, EventArgs e)
        { 
            StartGame();
        }
        void StartGame()
        {
            CreateMap();
            foreach (var player in Map.players)
            {
                this.Controls.Add(player);
                player.BringToFront();
            }
        }
        //function that fill map with bricks and walls
        void CreateMap()
        {
            int sx = 50;
            int sy = 50;
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    Rectangle rect = new Rectangle(x * sx, y * sy, sx, sy);
                    if (Map.map[y, x] == 'B')
                    {
                        g.FillRectangle(brush1, rect);
                        g.DrawRectangle(Pens.Black, rect);
                        Map.bricks.Add(new Brick(x, y, rect, g));
                    }
                    else if (Map.map[y, x] == 'W')
                    {
                        g.FillRectangle(brush2, rect);
                        g.DrawRectangle(Pens.Black, rect);
                    }
                }
            }
            pMap.Refresh();
        }
        //function for updating game time
        private void timer_Tick(object sender, EventArgs e)
        {
            time++;
            int minutes = time / 60;
            int seconds = time % 60;
            if (seconds < 10)
                lTime.Text = minutes.ToString() + ":0" + seconds.ToString();
            else
                lTime.Text = minutes.ToString() + ":" + seconds.ToString();
        }
        //key I/O functions
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Map.players[0].makeBomb = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                Map.players[0].goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                Map.players[0].goRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                Map.players[0].goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                Map.players[0].goDown = true;
            }
            if (e.KeyCode == Keys.A)
            {
                Map.players[1].goLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                Map.players[1].goRight = true;
            }
            if (e.KeyCode == Keys.W)
            {
                Map.players[1].goUp = true;
            }
            if (e.KeyCode == Keys.S)
            {
                Map.players[1].goDown = true;
            }
            if (e.KeyCode == Keys.E)
            {
                Map.players[1].makeBomb = true;
            }
            if (Map.playersCount > 2)
            { 
                if (e.KeyCode == Keys.NumPad4)
                {
                    Map.players[2].goLeft = true;
                }
                if (e.KeyCode == Keys.NumPad6)
                {
                    Map.players[2].goRight = true;
                }
                if (e.KeyCode == Keys.NumPad8)
                {
                    Map.players[2].goUp = true;
                }
                if (e.KeyCode == Keys.NumPad2)
                {
                    Map.players[2].goDown = true;
                }
                if (e.KeyCode == Keys.NumPad0)
                {
                    Map.players[2].makeBomb = true;
                }
            }
            if (Map.playersCount > 3)
            {
                if (e.KeyCode == Keys.J)
                {
                    Map.players[3].goLeft = true;
                }
                if (e.KeyCode == Keys.L)
                {
                    Map.players[3].goRight = true;
                }
                if (e.KeyCode == Keys.I)
                {
                    Map.players[3].goUp = true;
                }
                if (e.KeyCode == Keys.K)
                {
                    Map.players[3].goDown = true;
                }
                if (e.KeyCode == Keys.M)
                {
                    Map.players[3].makeBomb = true;
                }
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Map.players[0].makeBomb = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                Map.players[0].goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                Map.players[0].goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                Map.players[0].goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                Map.players[0].goDown = false;
            }
            if (e.KeyCode == Keys.A)
            {
                Map.players[1].goLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                Map.players[1].goRight = false;
            }
            if (e.KeyCode == Keys.W)
            {
                Map.players[1].goUp = false;
            }
            if (e.KeyCode == Keys.S)
            {
                Map.players[1].goDown = false;
            }
            if (e.KeyCode == Keys.E)
            {
                Map.players[1].makeBomb = false;
            }
            if (Map.playersCount > 2)
            {
                if (e.KeyCode == Keys.NumPad4)
                {
                    Map.players[2].goLeft = false;
                }
                if (e.KeyCode == Keys.NumPad6)
                {
                    Map.players[2].goRight = false;
                }
                if (e.KeyCode == Keys.NumPad8)
                {
                    Map.players[2].goUp = false;
                }
                if (e.KeyCode == Keys.NumPad2)
                {
                    Map.players[2].goDown = false;
                }
                if (e.KeyCode == Keys.NumPad0)
                {
                    Map.players[2].makeBomb = false;
                }
            }
            if (Map.playersCount > 3)
            {
                if (e.KeyCode == Keys.J)
                {
                    Map.players[3].goLeft = false;
                }
                if (e.KeyCode == Keys.L)
                {
                    Map.players[3].goRight = false;
                }
                if (e.KeyCode == Keys.I)
                {
                    Map.players[3].goUp = false;
                }
                if (e.KeyCode == Keys.K)
                {
                    Map.players[3].goDown = false;
                }
                if (e.KeyCode == Keys.M)
                {
                    Map.players[3].makeBomb = false;
                }
            }
        }
        //main timer
        private void Main_timer_Tick(object sender, EventArgs e)
        {
            if (!GameOver)
            {
                if (Map.Deaths >= Map.players.Count - 1)
                {
                    GameOver = true;
                    foreach (var player in Map.players)
                    {
                        if (player.living)
                        {
                            Map.results[Map.Deaths] = player.text;
                        }
                    }
                }
                pMap.Refresh();
                foreach (var player in Map.players)
                {
                    player.MakeMove();
                    if (player.makeBomb)
                        player.MakeBomb();
                    foreach (var bomb in player.l_bombs)
                    {
                        this.Controls.Add(bomb);
                        bomb.BringToFront();
                    }
                }
                foreach (var explosion in Map.explosions)
                {
                    this.Controls.Add(explosion);
                    explosion.BringToFront();
                }
            }
            else
            {
                Main_timer.Stop();
                string result = "First: " + Map.results[Map.players.Count - 1] + "\nSecond: " + Map.results[Map.players.Count - 2];
                if (Map.playersCount > 2)
                    result += "\nThird: " + Map.results[Map.playersCount - 3];
                if (Map.playersCount > 3)
                    result += "\nFourth: " + Map.results[Map.playersCount - 4];
                MessageBox.Show(result, "GAME OVER");
                Program.menu.SetState(GameMenu.State.MAIN_MENU);
                Program.menu.Show();
                this.Close();
            }
        }
        //function that opens menu if game is closed
        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.menu.SetState(GameMenu.State.MAIN_MENU);
            Program.menu.Show();
        }
    }
}