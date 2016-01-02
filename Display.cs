using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace Invaders
{
    class Display
    {


        private string drawString;
       
        Font drawFont;
        SolidBrush drawBrush;
        StringFormat drawFormat;
        Bitmap image;


        private int score;
        public int Score { get { return score; } set { score += value; } }
        Rectangle boundaries;



        public Display(Rectangle boundaries)
        {
            drawFont = new Font("Arial", 16);
            drawBrush = new SolidBrush(System.Drawing.Color.Red);
            drawFormat = new System.Drawing.StringFormat();
            this.image = (Bitmap)Properties.Resources.ResourceManager.GetObject("SpaceShip");
            this.boundaries = boundaries;
                              

        }
        
            
        //Draws the scoreBoard on the Game object
        public void DrawScore(Graphics g, int x, int y)
        {

            drawString = Score.ToString();
            g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            //g.DrawString("Hello", new Font("Arial", 16), new SolidBrush(System.Drawing.Color.Red), xPosition, 200, new System.Drawing.StringFormat());

        }


        public void DrawNumberOfLives(Graphics g, int livesLeft)
        {
            //Draws out the number of spaceships left
            for (int i = 0; i < livesLeft; i++)
            {
                g.DrawImage(image, boundaries.Right - image.Size.Width * (i + 1), 0, image.Size.Width, image.Size.Height);


            }

        }



        public void DrawGameOver(Graphics g, int x, int y)
        {
            drawString = "Game Over";
            g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

        }


       
    }
}
