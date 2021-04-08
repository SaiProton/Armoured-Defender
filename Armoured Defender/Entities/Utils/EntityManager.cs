using Armoured_Defender.Entities.Enemy;
using Armoured_Defender.Entities.Player;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Armoured_Defender.Entities.Utils
{
    // The Entity Manager.
    // This class is mostly a static class that other classes will have constant access to
    // It is responsible for handling all moving objects, minus the player
    public class EntityManager
    {
        //keeps the score of the game
        public static int score = 0;

        /* 
         * the two collections that will be used for moving objects
         * ObservableCollection is used so we can dynamically view when objects are removed or added
         * there is also no defined size, which mean their size is dynamic as well, similar to List<>
         */
         
        // the laser collection stores all existing lasers
        public static ObservableCollection<Laser> laserCollection { get; private set; }

        // the alien collection stores all existing aliens
        public static ObservableCollection<Alien> alienCollection { get; private set; }

        // these two store the lasers and aliens to be removed in the next tick update from the form
        public static Laser laserToRemove;
        public static Alien alienToRemove;

        // random number generator
        private static Random randGen;

        public EntityManager()
        {
            // instantiates collections
            laserCollection = new ObservableCollection<Laser>();
            alienCollection = new ObservableCollection<Alien>();

            // instantiate random number generator
            randGen = new Random();
        }

        // updates every entity every tick
        public static void UpdateEntities()
        {
            // first, update the aliens
            UpdateAliens();

            // second, spawn more aliens
            SpawnAliens();

            // lastly, update the lasers
            UpdateLasers();
        }

        private static void UpdateAliens()
        {
            // loops through each alien (backwards is important!)
            for (int i = alienCollection.Count - 1; i >= 0; i--)
            {
                // if the alien has been hit by a laser, (if yes, sets laser to remove)
                if (alienCollection[i].CheckAlienHit())
                {
                    // commence alien death
                    alienCollection[i].Death();
                    // remove laser from collection
                    laserCollection.Remove(laserToRemove);
                    // sets the alien to remove 
                    alienToRemove = alienCollection[i];
                    // removes alien
                    alienCollection.RemoveAt(i);

                    // backwards looping is important since if forward looping is used, you cannot remove items from a collection
                    // while iterating through it
                }
                // the the alien is at the last lane, being the bottom of the screen,
                else if (alienCollection[i].position.Y >= GameForm.LANES - 1)
                {
                    // GAME OVER!
                    GameForm.GAME_OVER = true;
                }
                else
                {
                    // update the alien if it's fine :)
                    alienCollection[i].Update();
                }
            }
        }

        private static void SpawnAliens()
        {
            // if the alien spawn delay is met
            if (GameForm.ticks % Alien.alienSpawnRate == 0)
            {
                // generate a random spawnpoint
                // Y is -1, so the alien starts 1 space outside the screen on top
                // randGen is used to generate an X between 0 and 9 to select a random spawning lane
                Point spawnPoint = new Point(randGen.Next(0, GameForm.LANES - 1), -1);

                // spawn the alien ONLY if there is not an alien already at the designated spawn point
                // this prevents clutter, as well as aliens encompassing the same space
                if (!Alien.AlienAtPoint(spawnPoint) && !Alien.AlienAtPoint(new Point(spawnPoint.X, spawnPoint.Y + 1)))
                {
                    // add a "random" alien if true
                    alienCollection.Add(Alien.RandomAlienType(spawnPoint, randGen.NextDouble()));
                }
            }
        }

        private static void UpdateLasers()
        {
            // loops through each laser
            for (int i = laserCollection.Count - 1; i >= 0; i--)
            {
                // if the laser goes out and above the screen,
                if (laserCollection[i].position.Y < 0)
                {
                    // set it as the laser to be removed by the form
                    laserToRemove = laserCollection[i];
                    // remove the laser from the collection
                    laserCollection.RemoveAt(i);
                }
                else
                {
                    // update the laser if it's fine :)
                    laserCollection[i].Update();
                }
            }
        }
    }
}
