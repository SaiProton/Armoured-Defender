using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Armoured_Defender
{
    public partial class StartForm : Form
    {
        //This is used to play sound 
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public StartForm()
        {
            InitializeComponent();
            //Identifies the location of the wav file that is to be played 
            player.SoundLocation = "..\\..\\Resources\\Music and Sounds\\Density  Time - MAZE NO COPYRIGHT 8-bit Music.wav";
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            //When the main menu form is loaded music is played 
            player.Load();
            player.Play();

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            player.Stop();

            //Shows the game form and hides the current form 
            new GameForm().Show();
            Hide();
        }

        private void LeaderboardButton_Click(object sender, EventArgs e)
        {
            player.Stop();

            //Shows the leaderboard form and hides the current form 
            new LeaderForm().Show();
            Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            //All operations are terminated --> All forms close down
            Application.Exit();
        }

        private void lblRules_Click(object sender, EventArgs e)
        {
            player.Stop();

            //Shows the rules form and hides the current form 
            new RulesForm().Show();
            Hide();
        }
    }
}