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
    public class EntityManager
    {
        public static int score = 0;

        public static ObservableCollection<Ball> laserCollection { get; private set; }
        public static ObservableCollection<Alien> alienCollection { get; private set; }

        public static int laserIndexToRemove = 0;
        public static Alien alienToRemove;

        private static Random randGen;

        public EntityManager()
        {
            laserCollection = new ObservableCollection<Ball>();
            alienCollection = new ObservableCollection<Alien>();

            randGen = new Random();
        }

        public static void UpdateEntities()
        {
            for (int i = alienCollection.Count - 1; i >= 0; i--)
            {
                if (alienCollection[i].CheckAlienHit())
                {
                    alienCollection[i].Death();
                    alienToRemove = alienCollection[i];
                    alienCollection.RemoveAt(i);
                }
                else if(alienCollection[i].position.Y >= GameForm.LANES)
                {
                    // GAME OVER!
                    GameForm.GAME_OVER = true;
                }
                else
                {
                    alienCollection[i].Update();
                }
            }

            if (GameForm.ticks % Alien.alienSpawnRate == 0)
            {
                Point spawnPoint = new Point(randGen.Next(0, GameForm.LANES - 1), -1);

                if (!Alien.AlienAtPoint(spawnPoint) && !Alien.AlienAtPoint(new Point(spawnPoint.X, spawnPoint.Y + 1)))
                {
                    alienCollection.Add(new Grounder(spawnPoint));
                }
            }

            for (int i = laserCollection.Count - 1; i >= 0; i--)
            {
                if(laserCollection[i].position.Y < 0)
                {
                    laserCollection.RemoveAt(i);
                }
                else
                {
                    laserCollection[i].Update();
                }
            }
        }
    }
}
