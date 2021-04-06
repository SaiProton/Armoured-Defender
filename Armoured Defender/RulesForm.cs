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
    public partial class RulesForm : Form
    {
        //This is used to play sound 
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public RulesForm()
        {
            InitializeComponent();
            //Identifies the location of the wav file that is to be played 
            player.SoundLocation = "..\\..\\Resources\\Music and Sounds\\Vibe Mountain - Operatic 3 NO COPYRIGHT 8-bit Music.wav";
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
            //Terminates all operations --> Closes all active forms down 
            Application.Exit();
        }

        private void RulesForm_Load(object sender, EventArgs e)
        {
            //When the rules form loads the music plays 
            player.Load();
            player.Play();
        }
    }
}
