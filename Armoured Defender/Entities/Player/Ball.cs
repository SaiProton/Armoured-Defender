using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Armoured_Defender.Entities.Player
{
    public class Ball
    {
        public PictureBox ballGraphic;

        public Point position;

        private const string ballGraphicPath = "../../Resources/Entities/Player/LASER-01.png";

        public Ball(int tankPosition)
        {
            position = new Point(tankPosition, GameForm.LANES - 1);
            ballGraphic = DefineBallGraphic();
        }

        private PictureBox DefineBallGraphic()
        {
            PictureBox graphic = new PictureBox 
            {
                Name = "Ball",
                Size = new Size((int)(GameForm.unitWidth / 2.0), (int)GameForm.unitHeight),
                ImageLocation = ballGraphicPath,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
            };

            graphic.Location = new Point((int)(position.X * 3 * GameForm.unitWidth + 2 * GameForm.unitWidth - graphic.Width / 2.0), (int)(GameForm.unitHeight * position.Y));

            return graphic;
        }

        public void Update()
        {
            position.Y--;
            ballGraphic.Top = (int)(GameForm.unitHeight * position.Y);
        }
    }
}
