using Armoured_Defender.Entities.Player;
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
    public partial class GameForm : Form
    {
        Tank player;

        public GameForm()
        {
            InitializeComponent();

            player = new Tank(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Controls.Add(player.tankGraphic);
            Controls.Add(player.cannon.cannonGraphic);

            KeyPress += GameForm_KeyPress;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            player.Update();
            
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            player.SetAcceleration(e.KeyChar);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
