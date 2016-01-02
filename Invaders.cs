using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    class Invaders 
    {

        int xMax, xMin, yMax, yMin;
        List<Invader> invaders;


        private int invadersLeft;

        public int InvadersLeft { get { return invadersLeft; } set { invadersLeft += value;  } }

        const int pictureWidth=50;
        const int pictureHeight = 50;
        private const int HorizontalInterval = 10;
        private const int VerticalInterval = 40;

        private bool moveDown = false;
        private bool moveRight = true;

        private enum Direction { Left, Right, Up, Down };

        protected enum ShipType { Bug, Saucer, Satellite, Spaceship, Star };

        //Keep an instance variable that keeps track of the shots fired
        private Shots playerShots;
        private Shots enemyShots;

        Random random;
        Display scoreBoard;


        Rectangle boundaries;
        //Constructor for the Invaders class taken the boundaries and the list of shots to be fired
        public Invaders(Rectangle boundaries, Shots playerShots, Shots enemyShots,Random random, Display scoreBoard)
        {

                       
            //Initializing the distribution of the invaders to centralize them in
            //starting in the middle
            this.xMax = boundaries.Right;
            this.xMin = boundaries.Left;
            this.yMax = boundaries.Top;
            this.yMin = boundaries.Bottom;

            this.invaders=new List<Invader>();

            //Initializing the shots
            this.playerShots = playerShots;
            this.enemyShots = enemyShots;


            this.random = random;
            this.boundaries = boundaries;
            this.invadersLeft = 30;
 


            for (int i = 0; i < 6; i++)
            {
               // int xcoordinate=(xMax+xMin)/2-(3+i)*(HorizontalInterval+pictureWidth);
                int xcoordinate = 200+(i) * (HorizontalInterval + pictureWidth);



                invaders.Add(new Invader("bug", new Point(xcoordinate, 0), 10));
                invaders.Add(new Invader("flyingsaucer", new Point(xcoordinate, pictureHeight+10), 20));
                invaders.Add(new Invader("satellite", new Point(xcoordinate, pictureHeight+70), 30));
                invaders.Add(new Invader("watchit", new Point(xcoordinate, pictureHeight+130), 40));
                invaders.Add(new Invader("star", new Point(xcoordinate, pictureHeight+190), 50));
                
            }

        }


        //Cycles through the list of invaders and places them out on the screen
        public void Draw(Graphics g, int animationCell)
        {
            foreach (Invader invader in invaders)
            {

                invader.Draw(g, animationCell);
            }

        }




        //Check if shots fired have hit anything
         private void CheckForInvaderCollisions(List<Invader> invaders){

        }
        

        //Move the invaders to the right and left respectively when they are within 100 pixels from the wall
        //Move down and change direction
        public void MoveInvaders(){

            MoveInvadersBooleans();

            if (moveDown == true)
            {
                foreach (Invader invader in invaders)
                {
                    invader.Move((int)Direction.Down);

                }
            }
            if (moveRight == true)
            {
                foreach (Invader invader in invaders)
                {
                    invader.Move((int)Direction.Right);

                }
                
                
            }
            else if (moveRight == false)
            {
                foreach (Invader invader in invaders)
                {
                    invader.Move((int)Direction.Left);

                }

            }
        
        }


        //Detects if any of the invaders is close to the border where they should start moving down and left
        public void MoveInvadersBooleans()
        {
            int numberOfInvaders = (from v in invaders
                         where v.Location.X > xMax - 50 || v.Location.X < 0
                         select v).Count<Invader>();

           
            if (numberOfInvaders == 0)
            {
                moveDown = false;
                
            }

            else
            {
                moveDown = true;
                
                if (moveRight == false)
                {
                    moveRight = true;
                }
                else
                {
                    moveRight = false;
                }
            }

        }

       


        //Use link to figure out which invaders should be able to return fire
         public void ReturnFire(int wave, Random random){

             
             if (wave + 1 == enemyShots.numberOfShots())
             {
                 return;
             }

             if (random.Next(10) < 10 - wave)
             {
                 return;
             }

             //Linq query to extract the invader to return fire
             var theShootingInvaders = (from invader in invaders
                                        group invader by invader.Location.X
                                            into invaderGroup
                                            orderby invaderGroup.Key descending
                                            select invaderGroup);


             if (invaders.Count() > 0)
             {
                 //Gets the first invader in each column last will take the closest invaders while first while take the invaders in the back
                 Invader theShooter = theShootingInvaders.ElementAt(random.Next(theShootingInvaders.Count())).Last<Invader>();

                 Fire(theShooter);
             }
        }

         private void Fire(Invader theShooter)
         {

             //Centralize the origin of the shot to the center front of the ship
             Point gunOfShip = new Point((theShooter.Area.Right+theShooter.Area.Left)/2,theShooter.Area.Bottom);
             enemyShots.Add(new Shot(gunOfShip, false, boundaries));
         }
        
         public void CheckForCollisions(Display scoreBoard)
         {

             for (int i = 0; i < playerShots.numberOfShots(); i++)
             {
                 for (int j = 0; j < invaders.Count();j++ )
                 {

                     if (invaders.ElementAt<Invader>(j).Area.Contains(playerShots.ElementAt(i).Area) == true)
                     {
                         playerShots.Remove(i);
                         scoreBoard.Score=invaders.ElementAt<Invader>(j).Score;
                         invaders.RemoveAt(j);
                         InvadersLeft = -1;
                        
                         break;

                         
                     }
                 }



             }


         }

    }
}
