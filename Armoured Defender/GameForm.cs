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
using System.Media;

namespace Armoured_Defender
{
    /*
     * The GameForm is the main "consortium" for the game
     * It keeps track of global game events, like the ticks, gamestate, and scale
     */ 
    public partial class GameForm : Form
    {
        // The player instance
        private Tank player;

        // The EntityManager instance, where all moving objects (except player) are managed
        private EntityManager entityManager;

        // keeps track of the amount of ticks the game has counted (0.25s per tick)
        public static int ticks;

        // boolean which keeps track of if the game has ended
        public static bool GAME_OVER;

        // keeps track of the amount of lanes (cols & rows) that there are in the game
        public static readonly int LANES = 10;

        // these keep track of how many pixels tall/wide each "unit" is
        // this is to scale every object accordingly to the exact same scale no matter the screen size
        // for instance, the tank is always 2 units wide and 1 unit tall; aliens are always 2 units wide and 2 units tall
        // the measure of a unit is shown by the horizontally aligned white bars in the game
        public static readonly double unitWidth = 1.0 / 31.0 * Screen.PrimaryScreen.Bounds.Width;
        public static readonly double unitHeight = 1.0 / 11.0 * Screen.PrimaryScreen.Bounds.Height;

        // detects whether a key is held down or not
        private bool keyPress = false;

        private SoundPlayer soundPlayer;
        
        public GameForm()
        {
            InitializeComponent();

            // sets to default values upon loading
            ticks = 0;
            GAME_OVER = false;

            // instantiates player
            player = new Tank();
            //adds player to form
            Controls.Add(player.tankGraphic);

            // instantiates the entity manager
            entityManager = new EntityManager();

            // Event assignments.
            // We "subscribe" the two methods on the right to the CollectionChanged event handler on the left by using the += operator 
            // This way, every time the Event manager's collections are changed, it will automatically call its designated method here
            EntityManager.laserCollection.CollectionChanged += LaserCollection_CollectionChanged;
            EntityManager.alienCollection.CollectionChanged += AlienCollection_CollectionChanged;

            // Similarly, we subscribe more methods for the Form's native KeyDown and KeyUp handlers to detect user input
            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;

            soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = "../../Resources/Music and Sounds/Crunch.wav";
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            soundPlayer.Load();
            soundPlayer.PlayLooping();
        }

        // calls when the laser collection is updated; this means it will call when any change occurs within the collection,
        // although it will not update if only the properties of its elements change, rather only when the elements themselves are changed
        private void LaserCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // checks if the collection has an addition
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // if it does, add the newly made laser to the form
                Controls.Add(EntityManager.laserCollection.Last().laserGraphic);
            }
            // checks if the collection has a removal
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // if it does, remove the laser from the form
                Controls.Remove(EntityManager.laserToRemove.laserGraphic);
            }
        }

        // similar to the above method;
        // calls when the alien collection is updated; this means it will call when any change occurs within the collection,
        // although it will not update if only the properties of its elements change, rather only when the elements themselves are changed
        private void AlienCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // checks if the collection has an addition
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // if it does, add the newly made alien to the form
                Controls.Add(EntityManager.alienCollection.Last().alienGraphic);
            }
            // checks if the collection has a removal
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // if it does, remove the alien from the form
                Controls.Remove(EntityManager.alienToRemove.alienGraphic);
            }
        }

        // calls when the user's key is down; continuous calling if key is held
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            // if the key is not already pressed,
            if(!keyPress)
            {
                // sets the key press to true
                keyPress = true;

                // if the key in question is space, have the player shoot
                if(e.KeyCode == Keys.Space)
                {
                    player.Shoot();
                }
                // if it is something else, send it to the player instance to check for positional updates
                else
                {
                    player.UpdatePosition(e.KeyCode);
                }
            }
        }

        // calls when the key is un-held
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            // sets the key press to false when key is let go
            keyPress = false;
        }

        // calls every tick; each tick is 0.25 seconds (defined by the GameTimer object)
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // if the player gets a game over, move to the game over form and close this one
            if(GAME_OVER)
            {
                new GameOverForm().Show();
                Close();
            }

            // increment tick count
            ticks++;

            // player shooting cooldown system:
            // if the tick count is a multiple of the defined tick delay, the player is set to be able to shoot again
            player.canShoot = ticks % player.shootDelayTicks == 0 || player.canShoot;

            // updates every entity (every moving object other than player)
            EntityManager.UpdateEntities();

            // updates the score text using EntityManager's score count
            ScoreText.Text = EntityManager.score.ToString();
        }

        // closes application if esc is pressed
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
