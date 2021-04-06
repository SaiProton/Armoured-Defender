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
        public int health;
        public int speed;

        private Point position;

        public PictureBox alienGraphic;
        private string alienGraphicPath;

        private const int scaleDownFactor = 20;

        public abstract void Update();

        protected PictureBox DefineAlienGraphic()
        {
            Image tempImg = Image.FromFile(alienGraphicPath);
            double aspectRatio = (double)tempImg.Width / tempImg.Height;

            return new PictureBox
            {
                Name = "Alien",

                Size = new Size(Screen.PrimaryScreen.Bounds.Width / scaleDownFactor, (int)((Screen.PrimaryScreen.Bounds.Width / scaleDownFactor) / aspectRatio)),
                ImageLocation = alienGraphicPath,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
            };
        }
    }

    class Seeker : Alien
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    class Shooter : Alien
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    class Grounder : Alien
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }

    class Waver : Alien
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
