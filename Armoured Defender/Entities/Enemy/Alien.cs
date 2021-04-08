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
    public abstract class Alien
    {
        public static int alienSpawnRate = 20;
        protected int pointValue;

        public Point position;
        public PictureBox alienGraphic;

        protected string alienGraphicPath;
        protected double moveFrequencyTicks;

        private static Random randGen = new Random();

        public Alien(Point spawnPoint, string path)
        {
            position = spawnPoint;
            alienGraphicPath = path;
            alienGraphic = DefineAlienGraphic();
        }

        public static bool AlienAtPoint(Point point)
        {
            foreach(Alien alien in EntityManager.alienCollection)
            {
                if(point == alien.position)
                {
                    Console.WriteLine(point + " = " + alien.position);
                    return true;
                }
            }

            return false;
        }

        public bool CheckAlienHit()
        {
            foreach(Ball laser in EntityManager.laserCollection)
            {
                if(laser.position == position || laser.position == new Point(position.X, position.Y + 1))
                {
                    return true;
                }
            }

            return false;
        }

        protected PictureBox DefineAlienGraphic()
        {
            PictureBox graphic = new PictureBox
            {
                Name = "Alien",
                Size = new Size((int)(2 * GameForm.unitWidth), (int)(2 * GameForm.unitHeight)),
                ImageLocation = alienGraphicPath,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
            };

            graphic.Location = new Point((int)(3 * position.X * GameForm.unitWidth + GameForm.unitWidth), (int)(position.Y * GameForm.unitHeight));

            return graphic;
        }

        public void Death()
        {
            EntityManager.score += pointValue;
        }

        public abstract void Update();
    }

    public class Avoider : Alien
    {
        public Avoider(Point spawnPoint) : base(spawnPoint, "") { }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    public class Shooter : Alien
    {
        public Shooter(Point spawnPoint) : base(spawnPoint, "") { }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    public class Grounder : Alien
    {
        public Grounder(Point spawnPoint) : base(spawnPoint, "../../Resources/Entities/Enemy/Grounder.png")
        {
            moveFrequencyTicks = 6;
            pointValue = 100;
        }

        public override void Update()
        {
            if (GameForm.ticks % moveFrequencyTicks == 0)
            {
                position.Y++;
                alienGraphic.Top = (int)(position.Y * GameForm.unitHeight);
                Console.WriteLine(alienGraphic.Location);
            }
        }
    }

    public class Waver : Alien
    {
        public Waver(Point spawnPoint) : base(spawnPoint, "") { }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
