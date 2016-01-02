using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Invaders
{
    class Invader 
    {
        private const int shipSpeed = 3;
               
        private enum Direction { Left, Right, Up, Down };
        private string invaderType;
        private Bitmap image;
        //Accessor and variable set
        public Point Location { get{return location;} set{location=value;} }
        private Point location;

        //Accessor used to detect collision with border and shot detection
        public Rectangle Area { get { return new Rectangle(this.location, image.Size); } }

        //Keeps the score from killing the enemy
        public int Score { get { return score; } private set { score=value;} }
        private int score;
        
        private Rectangle boundaries;


        //Accessor and variable set
        public Invader(String invaderType, Point location, int score)
        {

            this.invaderType = invaderType;
            this.location = location;
            this.score = score;
            image = InvaderImage(1);

        }

       

        //Move function that is able to move the invader ship left/right and down
        public void Move(int direction)
        {
            if (direction == (int)Direction.Left)
            {
                location.X -= shipSpeed;
            }

            else if (direction == (int)Direction.Right)
            {
                location.X += shipSpeed;
            }

            else if (direction == (int)Direction.Down)
            {
                location.Y += shipSpeed;
            }
          
        }

        public void Draw(Graphics g, int animationCell)
        {
        
            //Given a certain animation cell this function will display the correct picture for the 
            //Enemy
            g.DrawImage(InvaderImage(animationCell), location.X, location.Y, image.Size.Width, image.Size.Height);
        }

        
        private Bitmap InvaderImage(int animationCell)
        {

            //Gets the current picture as a concatentation of the ship type and the animation cell index
            string currentPicture = invaderType + animationCell.ToString();
            
            this.image = (Bitmap)Properties.Resources.ResourceManager.GetObject(currentPicture);
            return this.image;
            
        }



         
    }

}