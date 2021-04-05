using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Armoured_Defender.Entities.Player
{
    class Tank
    {
        public static int scaleFactor = 15;

        private int yPos;
        private double xPos;

        private double acceleration = 0;
        private double velocity = 0;

        private const double frictionFactor = -0.25;
        private const double bounciFactor = 0.25;

        public PictureBox tankGraphic;
        private const string tankGraphicPath = "../../Resources/Entities/Player/TANK-01.png";

        public Cannon cannon;

        public Tank(int formWidth, int formHeight) 
        {
            tankGraphic = DefineTankGraphic(formWidth, scaleFactor);

            yPos = formHeight - tankGraphic.Height;
            xPos = formWidth / 2;

            tankGraphic.Location = new Point((int)xPos, (int)yPos);

            cannon = new Cannon(tankGraphic.Location, tankGraphic.Width);
        }

        private PictureBox DefineTankGraphic(int formWidth, int scaleDownFactor)
        {
            Image tempImg = Image.FromFile(tankGraphicPath);
            double aspectRatio = tempImg.Width / tempImg.Height;

            return new PictureBox
            {
                Name = "Tank",
                Size = new Size((int)(aspectRatio * formWidth / scaleDownFactor), formWidth / scaleDownFactor),
                ImageLocation = tankGraphicPath,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
        }

        public void Update()
        {
            velocity += acceleration;
            xPos += velocity;
            wallCollisionCheck();

            tankGraphic.Left = (int)xPos;
 
            acceleration = velocity * frictionFactor;

            cannon.Update((int)xPos);
        }

        public void SetAcceleration(char pressed)
        {
            acceleration = (pressed == 'd' ? 1 : (pressed == 'a' ? -1 : acceleration));
        }

        private void wallCollisionCheck()
        {
            if(xPos + tankGraphic.Width >= Screen.PrimaryScreen.Bounds.Width || xPos <= 0)
            {
                xPos = Math.Min(Math.Max(xPos, 0), Screen.PrimaryScreen.Bounds.Width - tankGraphic.Width);
                velocity = -velocity * bounciFactor;
                acceleration = 0;
            }
        }
    }
}
