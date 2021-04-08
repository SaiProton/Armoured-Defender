using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Armoured_Defender.Entities.Utils;

namespace Armoured_Defender.Entities.Player
{
    class Tank
    {
        public int position = 4;

        public bool canShoot = true;
        public int shootDelayTicks = 4;

        public PictureBox tankGraphic;
        private const string tankGraphicPath = "../../Resources/Entities/Player/TANK-03.png";

        public Tank() 
        {
            tankGraphic = DefineTankGraphic();
        }

        private PictureBox DefineTankGraphic()
        {
            PictureBox graphic = new PictureBox
            {
                Name = "Tank",
                Size = new Size((int)(2 * GameForm.unitWidth), (int)GameForm.unitHeight),
                ImageLocation = tankGraphicPath,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            graphic.Location = new Point((int)(position * 3 * GameForm.unitWidth + GameForm.unitWidth), Screen.PrimaryScreen.Bounds.Height - graphic.Height);

            return graphic;
        }

        public void UpdatePosition(Keys keyPress)
        {
            if(keyPress == Keys.D && position != GameForm.LANES - 1)
            {
                position++;
            }
            else if (keyPress == Keys.A && position != 0)
            {
                position--;
            }

            tankGraphic.Left = (int)(position * 3 * GameForm.unitWidth + GameForm.unitWidth);
        }

        public void Shoot()
        {
            if(canShoot)
            {
                EntityManager.laserCollection.Add(new Ball(position));
                canShoot = false;
            }
        }
    }
}
