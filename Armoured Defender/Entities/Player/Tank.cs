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
    // The Tank class.
    // The player controls the tank and shoots down the aliens from above
    // The movement is based off game-and-watch games
    class Tank
    {   
        // position keeps track of which lane the tank is in, from 0 to 9
        // default position: on a 10 lane system, 4 is one of the two middle lanes
        public int position = 4;

        // variable which keeps track of if the player can shoot or not, which changes based on the cooldown
        public bool canShoot = true;

        // the delay in ticks of how often the player should be able to shoot
        public int shootDelayTicks = 4;

        // the picturebox which will hold the tank image
        public PictureBox tankGraphic;

        // path to the tank image
        private const string tankGraphicPath = "../../Resources/Entities/Player/TANK-03.png";

        public Tank() 
        {
            // defines the tank picturebox
            tankGraphic = DefineTankGraphic();
        }

        // defines the tank picturebox.
        // does so by creating a new picturebox and outlining all of its parameters to make it an effective tank image
        private PictureBox DefineTankGraphic()
        {
            // creates new picturebox called "graphic"
            PictureBox graphic = new PictureBox
            {
                // sets the name of the tank as reference
                Name = "Tank",
                // sets the size of the tank, which will change based on the screen size as indicated by the unit height/widths being referenced
                // it is 2 units wide and 1 unit tall
                Size = new Size((int)(2 * GameForm.unitWidth), (int)GameForm.unitHeight),
                // sets the image to the image as indicated by the path
                ImageLocation = tankGraphicPath,
                // contorts the image to fit within the designated size
                SizeMode = PictureBoxSizeMode.StretchImage,
                // makes the background transparent
                BackColor = Color.Transparent
            };

            /*
             * the location is set outside the graphic instantiation since the image size is needed to determine initial location
             * the location is defined as a point
             * the X value is = (3P + 1)(ŵ) where P is position, and ŵ is the unit width
             * P is scaled by 3 since the tank moves 3 units each time
             * 1 is added on since the tank is offset from the left by 1 unit
             * and of course, it is all multiplied by ŵ to scale it to the unit width
             */

            /*
             * the Y value is = (H - h) where H is screen height and h is the tank height
             * this definition may seem strange if you're accustomed to traditional cartesian coordianates...
             * the way windows forms handles coordinates is like cartesian coordinates, though with one major difference,
             * being that the Y axis increases from TOP to BOTTOM
             * the top of the screen is defined as 0, and goes UP as you go DOWN the screen
             * that being said, if the tank were to be set to the screen size, it would be below the screen,
             * so subtracting the height of the tank must be done in order to have it sitting on the bottom
             */ 
            graphic.Location = new Point((int)((3 * position + 1) * GameForm.unitWidth), Screen.PrimaryScreen.Bounds.Height - graphic.Height);

            // returns the graphic so the tank's picturebox can be set to it
            return graphic;
        }

        // this method updates the position of the tank if specific keys are pressed
        public void UpdatePosition(Keys keyPress)
        {
            // if the key is D and the tank is not on the very right of the board,
            if(keyPress == Keys.D && position != GameForm.LANES - 1)
            {
                // move to the right
                position++;
            }
            // if the key is A and the tank is not on the very left of the board, 
            else if (keyPress == Keys.A && position != 0)
            {
                // move to the left
                position--;
            }

            // the positional values may have been updated, but the pixel values of the picturebox were not.
            // The Y value does not have to be updated, so the X value is set using the same definition shown in DefineTankGraphic
            // unfortunately we must approximate the value to an integer since we are working with integer pixel values -- not subpixels
            tankGraphic.Left = (int)((3 * position + 1) * GameForm.unitWidth);
        }

        // tank fires if space is pressed
        public void Shoot()
        {
            // if the cooldown has worn off,
            if(canShoot)
            {
                // access the entitymanager to add a new laser to its collection
                EntityManager.laserCollection.Add(new Laser(position));

                // enables cooldown
                canShoot = false;
            }
        }
    }
}
