using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Armoured_Defender.Entities.Player
{
    class Cannon
    {
        public PictureBox cannonGraphic;
        public int maxBalls = 3;

        private int cannonShootSpeed = 10;

        private int shiftXConstant;
        private int shiftYConstant;

        private const int scaleDownFactor = 5;
        private string cannonGraphicPath = "../../Resources/Entities/Player/TURRET-DEFAULT.png";

        public Cannon(Point tankLocation, int tankWidth)
        {
            cannonGraphic = DefineCannonGraphic(tankWidth);

            shiftXConstant = (int)((tankWidth - cannonGraphic.Width) / 2.0);
            shiftYConstant = (int)(-cannonGraphic.Height / 2.0);

            cannonGraphic.Location = new Point(tankLocation.X + shiftXConstant, tankLocation.Y + shiftYConstant);
        }

        private PictureBox DefineCannonGraphic(int tankWidth)
        {
            Image tempImg = Image.FromFile(cannonGraphicPath);
            double aspectRatio = (double)tempImg.Width / tempImg.Height;

            return new PictureBox
            {
                Name = "Cannon",
                
                Size = new Size(tankWidth / scaleDownFactor, (int)((tankWidth / scaleDownFactor) / aspectRatio)),
                ImageLocation = cannonGraphicPath,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
            };
        }

        public void Shoot(Point mousePoint)
        {
            GameForm.existingBalls.Add(new Ball(cannonGraphic.Location, cannonGraphic.Width, mousePoint, cannonShootSpeed));
        } 

        public void Update(int tankPos)
        {
            cannonGraphic.Left = tankPos + shiftXConstant;
        }
    }
}
