using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace Invaders
{
    class Game

    {
          
        private int livesLeft = 4;

        public int LivesLeft { get { return livesLeft; } set { livesLeft += value; if (livesLeft == 0) { OnGameOver(null); } } }
        
        //Controls the rate of movement of the playership

        private int wave = 2;
        private int framesSkipped = 0;

        private enum ShipType { Bug, Saucer, Satellite, Spaceship, Star };

        

        private Rectangle boundaries;

        private Invaders invaders;
        public PlayerShip playerShip;
        private Shots playerShots;
        private Shots enemyShots;
        private Display display;


        private Random random;
        private static int animationCounter = 0;
        private Graphics g;

        public Stars stars;


        public enum Direction { Left, Right, Up, Down };




        DateTime initialScreen;
        Bitmap initialImage;


        public Game(Graphics g, Rectangle boundaries){
            //Initialize the stars to plot a black background and to be able to add the stars to the
            //Canvas;
            this.g = g;
            this.boundaries.X=0;
            this.boundaries.Y =0;
            this.boundaries.Height = boundaries.Y;
            this.boundaries.Width = boundaries.X;



            this.random = new Random();
          
            
            
            //Initializes two collections that keeps track of the shots fired respectively
            this.playerShots = new Shots();
            this.enemyShots = new Shots();


            this.stars=new Stars(boundaries);
            Point initialPosition = new Point(boundaries.X / 2, boundaries.Y);
            this.playerShip = new PlayerShip(initialPosition, boundaries,playerShots,enemyShots, livesLeft);


            this.invaders = new Invaders(boundaries,playerShots,enemyShots,random,display);
            invaders.MoveInvaders();

            this.display = new Display(boundaries);



            //Plays the music for the life force
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation=@"C:\GameMusic\LifeForce.wav";
            player.Load();
            player.Play();



            this.initialScreen= DateTime.Now;
            this.initialImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("LifeForce1");
        
           
        }

        public event EventHandler GameOver;

        public void OnGameOver(EventArgs e)
        {
            EventHandler handler = GameOver;

            if (handler != null)
            {
                handler(this,e);
            }


        }

        public void newWave()
        {
            this.invaders = new Invaders(boundaries, playerShots, enemyShots, random, display);
            
            wave += 1;
        }
             

        //Main draw function
        public void Draw(Graphics g, int animationCell){

        

            //Total seconds takes the absolute time difference and not only the second part of the difference
            if ((DateTime.Now - initialScreen).TotalSeconds < 5)
            {

               

                Point initialPosition=new Point((boundaries.Right + boundaries.Left-2*initialImage.Size.Width) /2,(boundaries.Top + boundaries.Bottom-2*initialImage.Size.Height) /2);
                g.DrawImage(initialImage,initialPosition.X,initialPosition.Y, initialImage.Size.Width*2, initialImage.Size.Height*2);

            }
            else
            {

              
                    stars.Draw(g);
                    playerShip.Draw(g);
                    invaders.Draw(g, GetAnimationCell());
                    playerShots.Draw(g);
                    enemyShots.Draw(g);
                    display.DrawScore(g,0,0);
                    display.DrawNumberOfLives(g, LivesLeft);
                }
               

            

        }

        //Need to slow down the frequency of calls to the current counter to make it less aggressive
    
        public int GetAnimationCell()
        {


            if (animationCounter > 38)
            {
                animationCounter = animationCounter / 39;
            }
            animationCounter++;


            return animationCounter / 10 + 1;
        }




        
        public void Go()

        {
            //Main method check if there are more invaders
            
            
            //If no more invaders call the newWave method;
            if(invaders.InvadersLeft==0){
                newWave();
            }

            //Check if the initial picture should be displayed
            if ((DateTime.Now - initialScreen).TotalSeconds < 5)
            {


            }
            else
            {
                if (playerShip.Alive == false)
                {

                    //Pauses the game function to run the animation of the ship when it dies and decreases
                    //The number of lives
                    if ((DateTime.Now - playerShip.timeOfDeath).TotalSeconds > 3)
                    {
                        playerShip.Alive = true;
                        LivesLeft =-1;

                    }


                }
                else
                {

                    invaders.MoveInvaders();
                    invaders.ReturnFire(wave, random);
                    enemyShots.Move(boundaries);
                    playerShots.Move(boundaries);
                    invaders.CheckForCollisions(display);
                    playerShip.CheckForCollisions();
                }

            }

        }
    }
}
