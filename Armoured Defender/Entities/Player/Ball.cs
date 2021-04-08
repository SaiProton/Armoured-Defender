using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Armoured_Defender.Entities.Player
{
    // The Laser.
    // This shoots out of the tank, and will hit one alien along its way (if aimed well!)
    public class Laser
    {
        // picturebox for the image of the laser to be shown on the form
        public PictureBox laserGraphic;

        // position of the laser on the grid
        public Point position;

        // path to the laser image
        private const string laserGraphicPath = "../../Resources/Entities/Player/LASER-01.png";

        // constructor has the tank position as a parameter since the laser will share the same X position as it
        public Laser(int tankPosition)
        {
            // instantiates the position,
            // setting its X value to the tank position,
            // and its Y value to the bottom of the grid
            position = new Point(tankPosition, GameForm.LANES - 1);

            // defines the laser picturebox
            laserGraphic = DefineLaserGraphic();
        }

        // defines the laser picturebox.
        // does so by creating a new picturebox and outlining all of its parameters to make it an effective laser image
        private PictureBox DefineLaserGraphic()
        {
            // creates new picturebox called "graphic"
            PictureBox graphic = new PictureBox 
            {
                // sets the name of the laser as reference
                Name = "Laser",
                // sets the size of the laser, which will change based on the screen size as indicated by the unit height/widths being referenced
                // it is 1/2 units wide and 1 unit tall
                Size = new Size((int)(GameForm.unitWidth / 2.0), (int)GameForm.unitHeight),
                // sets the image to the image as indicated by the path
                ImageLocation = laserGraphicPath,
                // contorts the image to fit within the designated size
                SizeMode = PictureBoxSizeMode.StretchImage,
                // makes the background transparent
                BackColor = Color.Transparent,
            };

            /*
             * the location is set outside the graphic instantiation since the image size is needed to determine initial location
             * the location is defined as a point
             * the X value is = (3Px + 1.75)(ŵ) where Px is the X position, and ŵ is the unit width
             * P is scaled by 3 since the laser must be aligned with the tank, which moves 3 units each time
             * 1.75 is added on since the laser is offset from the left by 1.75 units
             * and of course, it is all multiplied by ŵ to scale it to the unit width
             */

            /*
             * the Y value is = (ĥ * Py) where ĥ is the unit height, and Py is the Y position
             * each time Py is decremented, it will go up by one height unit
             */

            graphic.Location = new Point((int)(GameForm.unitWidth * (3 * position.X + 1.75)), (int)(GameForm.unitHeight * position.Y));

            // returns the graphic to be set as the laser picture box
            return graphic;
        }

        // updates the laser every tick
        public void Update()
        {
            // the position will be decremented, causing the laser to go up by one height unit
            position.Y--;
            
            // the pixel value for the image must also be adjusted; being adjusted using the same definition as outlined in DefineLaserGraphic
            laserGraphic.Top = (int)(GameForm.unitHeight * position.Y);
        }
    }
}
