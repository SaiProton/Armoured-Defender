using Armoured_Defender.Entities.Player;
using Armoured_Defender.Entities.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Armoured_Defender.Entities.Enemy
{
    // The Alien.
    
    /*
     * There are multiple different kinds of aliens, all outlined in this file within the same namespace
     * all of which extend off the base alien class here
     * the base alien class is abstract, meaning you cannot directly instantiate it;
     * rather you can only instantiate classes which are children of this class
     * This class is used to outline the features that all the aliens will share, so it will not have to
     * be written for each one multiple times
     * This keeps each alien class with its own unique features, rather than a bunch of reapeated features
     */

    public abstract class Alien
    {
        // the initial spawn rate
        protected static readonly int initialAlienSpawnRate = 15;
        
        // the spawn rate dictates the tick delay between each alien spawning; the one that will be used for delaying
        public static int alienSpawnRate = initialAlienSpawnRate;

        // amount of points needed before spawn delay decreases by 1 tick
        private static int spawnPointDelimeter = 2000;

        // the point value of an alien
        protected int pointValue;

        // the grid position of an alien
        public Point position;

        // the picturebox for holding the image to be shown to the form of an alien
        public PictureBox alienGraphic;

        // the path to the alien image
        protected string alienGraphicPath;

        // the delay between alien activity in ticks
        protected double moveFrequencyTicks;

        // random number generator
        protected static Random randGen = new Random();

        // the alien is given a spawn point by the entity manager
        // the alien is give an image path by the child class
        public Alien(Point spawnPoint, string path)
        {
            // sets given variables
            position = spawnPoint;
            alienGraphicPath = path;

            // defines alien picturebox
            alienGraphic = DefineAlienGraphic();
        }

        // checks if alien is at a given point
        public static bool AlienAtPoint(Point point)
        {
            // goes through each alien in the entitymanager's collection
            foreach(Alien alien in EntityManager.alienCollection)
            {
                // the the point matches with an alien's position,
                if(point == alien.position)
                {
                    return true;
                }
            }

            // if there are no aliens at the given point,
            return false;
        }

        // this is where new aliens are to be instantiated
        // aliens are spawned using probability, outlined here:
        public static Alien RandomAlienType(Point spawnPoint, double randnum)
        {
            // 50% chance to spawn Grounder
            if (randnum < 0.5) return new Grounder(spawnPoint);
            // 25% chance to spawn Waver
            if (randnum < 0.75) return new Waver(spawnPoint);
            // 15% chance to spawn Avoider
            if (randnum < 0.9) return new Avoider(spawnPoint);
            // 10% chance to spawn Avoider
            else return new Avoider(spawnPoint);
        }

        // function for checking if the spaces next to an alien are free given a left/right direction
        protected bool CheckNextTo(int direction)
        {
            // if one of these is true, the direction in question is not feasable to travel in
            return (AlienAtPoint(new Point(position.X + direction, position.Y))
                    // if there is an alien at its future horizontal displacement that is one unit below
                    || AlienAtPoint(new Point(position.X + direction, position.Y + 1))
                    // if there is an alien at its future horizontal displacement that is one unit above
                    || AlienAtPoint(new Point(position.X + direction, position.Y - 1))
                    // if it is against either end of the grid 
                    || (position.X + direction) < 0
                    || (position.X + direction) > GameForm.LANES - 1);
        }

        // checks if the alien has been hit
        public bool CheckAlienHit()
        {
            // goes through each laser in the entity manager's collection
            foreach(Laser laser in EntityManager.laserCollection)
            {
                // if the laser position matches with the aliens, or is one unit above, (so they don't trade spots upon simultaneous movement)
                if(laser.position == position || laser.position == new Point(position.X, position.Y + 1))
                {
                    // sets the laser to remove as the laser that hit the alien
                    EntityManager.laserToRemove = laser;
                    return true;
                }
            }

            // if the alien is fine, return false
            return false;
        }
        
        protected PictureBox DefineAlienGraphic()
        {
            // "graphic" is instantiated as a new picturebox
            PictureBox graphic = new PictureBox
            {
                // named for reference
                Name = "Alien",
                // sized to 2 units by 1 unit
                Size = new Size((int)(2 * GameForm.unitWidth), (int)(2 * GameForm.unitHeight)),
                // image location is given as the image path
                ImageLocation = alienGraphicPath,
                // image is stretched to fit the size
                SizeMode = PictureBoxSizeMode.StretchImage,
                // background color is made to be transparent
                BackColor = Color.Transparent,
            };

            /*
             * the graphic's X value is defined as (3Px + 1)(ŵ) -- same as the tank
             * scaled by 3 since the alien moves 3 spaces when moving horizontally
             * add 1 since it is shifted by 1 unit from the left
             * scale all by ŵ 
             * 
             * the graphic's Y value is defined as (Py * ĥ) -- same as the laser
             * simply will be the Y position scaled by the unit height 
             */
            graphic.Location = new Point((int)((3 * position.X + 1) * GameForm.unitWidth), (int)(position.Y * GameForm.unitHeight));

            return graphic;
        }

        // any last commands the alien will execute before being deleted
        public void Death()
        {
            // the score is incremented by the point value of the alien
            EntityManager.score += pointValue;

            alienSpawnRate = Math.Max(5, (int)Math.Ceiling(initialAlienSpawnRate - EntityManager.score / (double)spawnPointDelimeter));
        }

        // updates the alien depending on its behaviour, which is why it is an abstract method here,
        // since it will be different for every alien
        public abstract void Update();
    }

    // The Avoider: inherits everything from the base alien class
    // Is extremely shy and avoids you at all costs, very difficult to hit
    public class Avoider : Alien
    {
        public Avoider(Point spawnPoint) : base(spawnPoint, "../../Resources/Entities/Enemy/Avoider.png")
        {
            // will move every 6-9 ticks
            moveFrequencyTicks = randGen.Next(6, 9);

            pointValue = 300;
        }

        public override void Update()
        {
            // if cooldown is met
            if (GameForm.ticks % moveFrequencyTicks == 0)
            {
                // inDanger tracks if there is a laser heading this alien's way
                bool inDanger = false;

                // checks each laser if its X position lines up with the aliens
                foreach (Laser laser in EntityManager.laserCollection)
                {
                    inDanger = laser.position.X == position.X || inDanger;
                }

                if (inDanger)
                {
                    // picks a random direction to check first
                    int direction = randGen.NextDouble() > 0.5 ? 1 : -1;

                    // if direction is available, move there
                    if (!CheckNextTo(direction))
                    {
                        position.X += direction;
                    }
                    // if other direction is available instead, move there
                    else if (!CheckNextTo(-direction))
                    {
                        position.X -= direction;
                    }
                }
                else
                {
                    // if not in danger and nothing is below it, move down
                    if (!AlienAtPoint(new Point(position.X, position.Y + 2)))
                    {
                        position.Y++;
                    }
                }

                // update the pixel values for the picturebox
                alienGraphic.Left = (int)((3 * position.X + 1) * GameForm.unitWidth);
                alienGraphic.Top = (int)(position.Y * GameForm.unitHeight);
            }
        }
    }

    // The Shooter: inherits everything from the base alien class
    // Stays in one spot and blasts away
    public class Shooter : Alien
    {
        public Shooter(Point spawnPoint) : base(spawnPoint, "../../Resources/Entities/Enemy/Shooter.png") { }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    // The Grounder: inherits everything from the base alien class
    // goes for the ground real fast
    public class Grounder : Alien
    {
        public Grounder(Point spawnPoint) : base(spawnPoint, "../../Resources/Entities/Enemy/Grounder.png")
        {
            // will move every 6-8 ticks
            moveFrequencyTicks = randGen.Next(2, 5);

            // is worth 100 points
            pointValue = 100;
        }

        public override void Update()
        {
            // if the cooldown is met and there is no alien where it wants to go,
            if (GameForm.ticks % moveFrequencyTicks == 0 && !AlienAtPoint(new Point(position.X, position.Y + 2)))
            {
                // go down by 1
                position.Y++;
                
                // update the pixel value
                alienGraphic.Top = (int)(position.Y * GameForm.unitHeight);
            }
        }
    }

    // The Waver: inherits everything from the base alien class
    // goes across the grid and moves down periodically
    public class Waver : Alien
    {
        // either -1 or 1
        // represents whether it is moving left or right
        private int direction;

        public Waver(Point spawnPoint) : base(spawnPoint, "../../Resources/Entities/Enemy/Waver.png")
        {
            // randomly chooses initial direction
            direction = randGen.NextDouble() < 0.5 ? -1 : 1;

            // moves every 7-10 ticks
            moveFrequencyTicks = randGen.Next(3, 6);

            // worth 200 points
            pointValue = 200;
        }

        public override void Update()
        {
            // if cooldown is met
            if (GameForm.ticks % moveFrequencyTicks == 0)
            {
                // if any of these 5 cases are met, the waver will move down instead of moving horizontally
                       // if there is an alien at its future horizontal displacement,
                if (CheckNextTo(direction))
                {
                    // move down 1
                    position.Y++;
                    // flip direction
                    direction *= -1;
                }
                else
                {
                    // move horizontally if the space is open
                    position.X += direction;
                }
            }

            // update the pixel positions for the imagebox
            alienGraphic.Left = (int)((3 * position.X + 1) * GameForm.unitWidth);
            alienGraphic.Top = (int)(position.Y * GameForm.unitHeight);
        }
    }
}
