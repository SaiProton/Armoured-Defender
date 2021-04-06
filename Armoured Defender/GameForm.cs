using Armoured_Defender.Entities.Enemy;
using Armoured_Defender.Entities.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Tank player;

        public static int gameScore = 0;

        public static ObservableCollection<Ball> existingBalls { get; private set; }
        public static ObservableCollection<Alien> existingAliens { get; private set; }
        
        public GameForm()
        {
            InitializeComponent();

            player = new Tank(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Controls.Add(player.tankGraphic);
            Controls.Add(player.cannon.cannonGraphic);

            KeyPress += GameForm_KeyPress;

            existingBalls = new ObservableCollection<Ball>();
            existingBalls.CollectionChanged += ExistingBalls_CollectionChanged;

            existingAliens = new ObservableCollection<Alien>();
            existingAliens.CollectionChanged += ExistingAliens_CollectionChanged;
        }

        private void ExistingAliens_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Controls.Add(existingAliens.Last().alienGraphic);
            }
        }

        private void ExistingBalls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Controls.Add(existingBalls.Last().ballGraphic);
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            player.Update();
            
            for(int i = existingBalls.Count -  1; i >= 0; i--)
            {
                if(existingBalls[i].OutBoundsCheck())
                {
                    existingBalls.RemoveAt(i);
                } else
                {
                    existingBalls[i].Update();
                }
            }

            for(int i = existingAliens.Count - 1; i >= 0; i--)
            {
                existingAliens[i].Update();
            }
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' && existingBalls.Count < player.cannon.maxBalls)
            {
                player.cannon.Shoot(Cursor.Position);
            }
            else
            {
                player.SetAcceleration(e.KeyChar);
            }
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

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
