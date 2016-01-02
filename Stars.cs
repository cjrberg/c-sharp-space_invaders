using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    class Stars
    {
        //Need the random element that can be used to generate the stars in Random colors upon initiailization

        Random random= new Random();
        //Create a list to populate with the stars
        List<Star> stars; 
        //Create an enum that can safely keep all the colors of the stars
        private enum Colors { Red, Blue, Green, Purple, Yellow };
        String[] colorStrings = Enum.GetNames(typeof(Colors));

        //Gives an area to determines the area of the form area to draw upon
        private Rectangle boundaries;
        private int xMax;
        private int yMax;

        //Determines the number of stars
        private int numberOfStars=300;
        private int numberOfStarsReplace = 1;
        //Gives a property to determine where the star will be located
        Point location;

        private struct Star
        {
            public Point point;
            public Pen pen;

            public Star(Point point, Pen pen)
            {
                this.point = point;
                this.pen = pen;

            }
        }

        
        public Stars(Rectangle boundaries)
        {
            this.boundaries = boundaries;
            this.stars = new List<Star>(numberOfStars);
            this.xMax = boundaries.X;
            this.yMax = boundaries.Y;
                        
            for (int i = 0; i <numberOfStars; i++)
            {
                //Initialises the location of a star within the drawing area [0,xMax] [0,yMax]
                this.location = new Point(random.Next(0, xMax), random.Next(0, yMax));
                stars.Add(new Star(location, RandomPen()));
                          
            }

        }


        private Pen RandomPen()
        {
        
            //Randomly chooses a color and assigns it to a string
            String randomColor=colorStrings[random.Next(0,colorStrings.Length-1)];
         
            return new Pen(Color.FromName(randomColor), 1);
         
        }

          
        public void Draw(Graphics g)
        {

            //Loop through the collection of stars and plot them with one pixel
            foreach(Star star in stars)
            {
                g.FillRectangle(star.pen.Brush, star.point.X, star.point.Y, 1, 1);
            }



        }

        //
        public void Twinkle()
        {
            for (int i = 0; i < numberOfStarsReplace; i++)
            {
                stars.RemoveAt(random.Next(0, stars.Count));
                location = new Point(random.Next(0, xMax), random.Next(0, yMax));
                stars.Add(new Star(location, RandomPen()));
            }

        }

    }
}
