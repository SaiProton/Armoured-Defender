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

        private Point velocity;

        private int shiftXConstant;
        private int shiftYConstant;

        private const int scaleDownFactor = 2;
        private const string ballGraphicPath = "../../Resources/Entities/Player/Ball.png";

        public Ball(Point cannonLocation, int cannonWidth, Point mouseLocation, int initialSpeed)
        {
            ballGraphic = DefineBallGraphic(cannonWidth);

            shiftXConstant = (int)((cannonWidth - ballGraphic.Width) / 2.0);
            shiftYConstant = (int)(-ballGraphic.Height / 2.0);

            ballGraphic.Location = new Point(cannonLocation.X + shiftXConstant, cannonLocation.Y + shiftYConstant);

            velocity = SetVelocity(mouseLocation, initialSpeed);
        }

        private Point SetVelocity(Point mouseLocation, int initialSpeed)
        {
            double angle = Math.Atan2(ballGraphic.Location.Y - mouseLocation.Y, (double)mouseLocation.X - ballGraphic.Location.X);

            int xVel = (int)(initialSpeed * Math.Cos(angle));
            int yVel = (int)(initialSpeed * Math.Sin(angle));

            Console.WriteLine(angle);

            return new Point(xVel, yVel);
        }

        private PictureBox DefineBallGraphic(int cannonWidth)
        {
            //Image tempImg = Image.FromFile(ballGraphicPath);
            //double aspectRatio = (double)tempImg.Width / tempImg.Height;

            double aspectRatio = 1;

            return new PictureBox
            {
                Name = "Ball",
                Size = new Size(cannonWidth / scaleDownFactor, (int)((cannonWidth / scaleDownFactor) / aspectRatio)),
                //ImageLocation = ballGraphicPath,
                //SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Gray,
            };
        }

        public void Update()
        {
            ballGraphic.Left += velocity.X;
            ballGraphic.Top -= velocity.Y;
        }

        public bool OutBoundsCheck()
        {
            return ballGraphic.Location.X > Screen.PrimaryScreen.Bounds.Width || ballGraphic.Location.X < 0 ||
                   ballGraphic.Location.Y > Screen.PrimaryScreen.Bounds.Height || ballGraphic.Location.Y < 0;
        }
    }
}
