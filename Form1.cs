using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Invaders
{
    public partial class Form1 : Form
    {
        //Boolean to keep track of whether the game is over
        private bool gameOver = false;
        private Game game;
        private Display display;


        //Use the CreateGraphics method to create a Graphics object. 
        Graphics formGraphics;
        Rectangle boundaries;


       private enum Direction { Left, Right, Up, Down };

        //Constructor that will set the form up initialize the game object and handle the animation
        //Forst step would be to add the stars to see that it works
        //Then the player ship and last the invaders to see if everything works
        public Form1()
        {
            InitializeComponent();

            this.formGraphics=this.CreateGraphics();
            //Set the dimensions of the grid
            this.boundaries.X=this.ClientRectangle.Width;
            this.boundaries.Y=this.ClientRectangle.Height;

           
          this.game = new Game(formGraphics, boundaries);
          this.display = new Display(boundaries);

          animationTimer.Enabled = true;
          animationTimer.Start();

          gameTimer.Enabled = true;
          gameTimer.Start();




          game.GameOver += new EventHandler(game_GameOver);
            this.Invalidate();

            
        }


        //Event Handler used to stop the timers if the player looses the lives
        void game_GameOver(object sender, EventArgs e)
        {
            gameTimer.Stop();
            animationTimer.Stop();
            
            
            
            gameOver = true;
            this.Refresh();
           
            
        }

        
        List<Keys> keyspressed = new List<Keys>();

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            
            if (e.KeyCode == Keys.Q)
                Application.Exit();

            if (gameOver)
                if (e.KeyCode == Keys.S)
                {
                        //Renables the game if the game is finished                                    
           
                        this.game = new Game(formGraphics, boundaries);
                        this.display = new Display(boundaries);

                        animationTimer.Start();

                        gameTimer.Start();
                        gameOver = false;
                        game.GameOver += new EventHandler(game_GameOver);

                }

            //Fires shots from the ship
            if (e.KeyCode == Keys.Space)
                game.playerShip.Fire();

            if (keyspressed.Contains(e.KeyCode))
                keyspressed.Remove(e.KeyCode);

            keyspressed.Add(e.KeyCode);
        
             }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keyspressed.Contains(e.KeyCode))
                keyspressed.Remove(e.KeyCode);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
        game.Go();


            

            foreach (Keys key in keyspressed)
            {

                if (key == Keys.Left)
                {
                    game.playerShip.Move((int)Direction.Left);
                   
                    return;
                }

                else if (key == Keys.Right)
                {
                   
                    game.playerShip.Move((int)Direction.Right);
                    return;
                }
            }
            
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            game.stars.Twinkle();
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Initializes the Graphics object so that double buffering can be utilized i.e. to prevent
            //flickering of the graphics

            formGraphics = e.Graphics;
            if (gameOver == false)
            {
                game.Draw(formGraphics, 1);
            }
            if (gameOver == true)
            {

                BackgroundImage = null;
                //formGraphics.FillRectangle(Brushes.Black, boundaries);


                //Sets the initial position of the display of the Game Over text on screen
               
                 int x=this.boundaries.X/2-50;
                 int y=this.boundaries.Y/2;
               
               
                display.DrawGameOver(formGraphics,x, y);
              

            }

        }
      
          
    }
}