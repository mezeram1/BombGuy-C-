using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombGuy_game
{
    //Menu form
    public partial class GameMenu : Form
    {
        public enum State { MAIN_MENU, GAME_MENU, GAME };
        State game_state = State.MAIN_MENU;
        int players = 0;
        public GameMenu()
        {
            InitializeComponent();
            game_state = State.MAIN_MENU;
        }
        //function that changes state of game
        public void SetState(State new_state)
        {
            game_state = new_state;
            switch (game_state)
            {
                case State.MAIN_MENU:
                    bNewGame.Visible = true;
                    bExit.Visible = true;
                    bTwoPlayers.Visible = false;
                    bThreePlayers.Visible = false;
                    bFourPlayers.Visible = false;
                    break;
                case State.GAME_MENU:
                    bNewGame.Visible = false;
                    bExit.Visible = false;
                    bTwoPlayers.Visible = true;
                    bThreePlayers.Visible = true;
                    bFourPlayers.Visible = true;
                    break;
                case State.GAME:
                    this.Hide();
                    Program.game = new Game(players);
                    Program.game.ShowDialog();
                    break;
                default:
                    break;
            }
        }
        //exit button function
        private void bExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        //function for preparing game
        private void bNewGame_Click(object sender, EventArgs e)
        {
            SetState(State.GAME_MENU);
        }
        //functions that starts game with different number of players
        private void bTwoPlayers_Click(object sender, EventArgs e)
        {
            players = 2;
            SetState(State.GAME);
        }
        private void bThreePlayers_Click(object sender, EventArgs e)
        {
            players = 3;
            SetState(State.GAME);
        }
        private void bFourPlayers_Click(object sender, EventArgs e)
        {
            players=4;
            SetState(State.GAME);
        }
        private void GameMenu_Load(object sender, EventArgs e)
        {
            SetState(State.MAIN_MENU);
        }
    }
}
