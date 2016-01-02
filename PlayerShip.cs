using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    class PlayerShip
    {


        
        private const int shipSpeed = 3;
         
        public enum Direction { Left, Right, Up, Down };


        private Bitmap image;
        
        //Controls the properties of the private field location
       // public Point Location { get { return location; } private set; }
        private Point location;
        public Rectangle Area { get { return new Rectangle(this.location, image.Size); } }
        private Rectangle boundaries;

        //Checking for Shots fired and returned

        Shots playerShots;
        Shots enemyShots;

        public bool Alive { get { return alive; } set { alive = value; this.timeOfDeath = DateTime.Now;  } }
        private bool alive;

        private int deadShipHeight;
        public DateTime timeOfDeath;

        public PlayerShip(Point location,Rectangle boundaries, Shots playerShots, Shots enemyShots, int livesLeft)
        {
            //Initialize the image
            this.image = (Bitmap)Properties.Resources.ResourceManager.GetObject("SpaceShip");
            //Initialize the location of the ship
            this.location.X = location.X;
            this.location.Y = location.Y - 30;// -image.Height;
            //Set the ship boolean to true for being alive
            this.Alive = true;
            

            //Initialize the original height
            deadShipHeight = image.Size.Height;


            this.boundaries = boundaries;


            //Shots fired

            this.playerShots = playerShots;
            this.enemyShots = enemyShots;
            this.timeOfDeath = timeOfDeath;
           
        }


        public void Move(int direction)
        {
       
            //Movement function for the spaceship
            if (direction == (int)Direction.Left && Area.Left > 0)
            {
                location.X -= shipSpeed;
            }

            else if (direction == (int)Direction.Right && Area.Right < boundaries.Right)
            {
                location.X += shipSpeed;
            }
          
        }


        public void Fire()
        {

            //Centralize the origin of the shot to the center front of the ship
            Point gunOfShip=new Point(location.X+image.Width/2-2,location.Y-image.Height);

            //Only allow addition of shots if the total number of shots by the player is less than 5
            if (playerShots.numberOfShots() < 5)
            {
                playerShots.Add(new Shot(gunOfShip, true, boundaries));
            }
        }


        public bool CheckForCollisions()
        {

            for (int i = 0; i < enemyShots.numberOfShots(); i++)
            {

                if (Area.Contains(enemyShots.ElementAt(i).Area) == true)
                {
                    Alive = false;
                    enemyShots.Remove(i);
                    return Alive;
                }

                else
                {
                    Alive = true;
                                       
                }

             }

            return Alive;

        


        }

                
        public void Draw(Graphics g)
        {



           



            if (Alive==true)
            {
            deadShipHeight = image.Size.Height;
            //Given a certain animation cell this function will display the correct picture for the 
            //Enemy
            
                    g.DrawImage(image, location.X, location.Y-image.Size.Height, image.Size.Width, image.Size.Height);
        
    
            }

            else
            {

                
                if (deadShipHeight > 0)
                {
                    deadShipHeight = deadShipHeight - 1;

                    g.DrawImage(image, location.X, location.Y - image.Size.Height, image.Size.Width, deadShipHeight);
                }
                

            }
 
 
        }
        
    }
}
