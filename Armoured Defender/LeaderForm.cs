using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Armoured_Defender
{
    public partial class LeaderForm : Form
    {
        //This is used to play music 
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public LeaderForm()
        {
            InitializeComponent();
            //Identifies the location of the wav file that is to be played 
            player.SoundLocation = "..\\..\\Resources\\Music and Sounds\\Joshua McLean - Mountain Trials NO COPYRIGHT 8-bit Music.wav";
        }

        private void LeaderForm_Load(object sender, EventArgs e)
        {
            //Each time the form loads the music plays 
            player.Load();
            player.Play();

            //This is the path of the text file that stores all the top 10 usernames
            string sUsernamesFilePath = "..\\..\\Saved Data -- DO NOT EDIT\\usernames.txt";
            //This is the path of the text file that stores all the top 10 scores
            string sUserscoresFilePath = "..\\..\\Saved Data -- DO NOT EDIT\\userscores.txt";
            
            string[] arrUserscore = File.ReadAllLines(sUserscoresFilePath);
            string[] arrUsernames = File.ReadAllLines(sUsernamesFilePath);


            //Prints the leader board statistics to the label 
            for (int i = 0; i < arrUserscore.Length; i++)
            {
                //Prints the name and the score that is associated with that username
                lblleaderboard.Text += arrUsernames[i] + "          " + arrUserscore[i] + "\n";
            }
        }

        private void btnGoToMain_Click(object sender, EventArgs e)
        {
            player.Stop();

            //Shows the main menu form and hides the current form 
            new StartForm().Show();
            Hide();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Terminates all operations and forms 
            Application.Exit();
        }
    }
}




