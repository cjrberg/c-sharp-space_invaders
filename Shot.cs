using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invaders
{
    class Shot
    {

        private const int moveInterval = 10;
        private const int width = 3;
        private const int height=15;

        public bool isPlayer;

        public Point location;
        public Point Location { get { return location; }  set { location = value; } }

        public Rectangle Area { get { return new Rectangle(location.X, location.Y,width,height); } }
        private Rectangle boundaries;


        Bitmap shotImage;
      


        public Shot(Point location, bool isPlayer, Rectangle boundaries){
            this.location = location;
            this.isPlayer = isPlayer;
            this.boundaries = boundaries;
            this.shotImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("PlayerShot");

        }


        //Need to figure out how the drawing will work,, most likely will need to add some 
        //Yellow color to this one as well
        public void Draw(Graphics g)
        {

            //Plots the shot in yellow according to the dimensions and location
            g.DrawImage(shotImage,location.X,location.Y,width,height);
      
            
        }

        //Need to add code to ensure that the shot is within the boundary to see if should be drawn out as well
        
        public bool Move()
        {
            if(isPlayer==true)
            location.Y-=moveInterval;

            else
            location.Y+=moveInterval;


            return boundaries.Contains(this.Area);
        }
        
        

    }
}
