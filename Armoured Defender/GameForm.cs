using Armoured_Defender.Entities.Enemy;
using Armoured_Defender.Entities.Player;
using Armoured_Defender.Entities.Utils;
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
using System.Collections.Specialized;

namespace Armoured_Defender
{
    public partial class GameForm : Form
    {
        private Tank player;
        private EntityManager entityManager;

        public static int ticks;
        public static bool GAME_OVER;

        public static readonly int LANES = 10;

        public static readonly double unitWidth = 1.0 / 31.0 * Screen.PrimaryScreen.Bounds.Width;
        public static readonly double unitHeight = 1.0 / 11.0 * Screen.PrimaryScreen.Bounds.Height;

        private bool keyPress = false;
        
        public GameForm()
        {
            InitializeComponent();

            ticks = 0;
            GAME_OVER = false;

            player = new Tank();
            Controls.Add(player.tankGraphic);

            entityManager = new EntityManager();

            EntityManager.laserCollection.CollectionChanged += LaserCollection_CollectionChanged;
            EntityManager.alienCollection.CollectionChanged += AlienCollection_CollectionChanged;

            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
        }

        private void LaserCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Controls.Add(EntityManager.laserCollection.Last().ballGraphic);
            }
        }

        private void AlienCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Controls.Add(EntityManager.alienCollection.Last().alienGraphic);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Controls.Remove(EntityManager.alienToRemove.alienGraphic);
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(!keyPress)
            {
                keyPress = true;

                if(e.KeyCode == Keys.Space)
                {
                    player.Shoot();
                }
                else
                {
                    player.UpdatePosition(e.KeyCode);
                }
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            keyPress = false;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if(GAME_OVER)
            {
                new GameOverForm().Show();
                Close();
            }

            ticks++;

            player.canShoot = ticks % player.shootDelayTicks == 0 || player.canShoot;

            EntityManager.UpdateEntities();
            ScoreText.Text = EntityManager.score.ToString();
        }
    }
}
