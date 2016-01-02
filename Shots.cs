using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;


namespace Invaders
{
    class Shots :IEnumerable
    {
        List<Shot> shots;

        System.Media.SoundPlayer player; 

        public Shots()
        {
            this.shots = new List<Shot>();
            this.player = new System.Media.SoundPlayer();
            player.SoundLocation = @"C:\GameMusic\NES-13-13.wav";
            player.Load();

            

        }
        
        //Method to draw out the collection of shots
        public void Draw(Graphics g)
        {
            foreach (Shot shot in shots)
            {
                shot.Draw(g);
                
            }

        }


        //Property to be able to add shots triggered from form when shots are fired
        public void Add(Shot shot)
        {
            shots.Add(shot);
            if (shot.isPlayer == true)
            {
                player.Play();
            }
        }

        //Moves both of the shots for player and invaders respectively and ensures that the number of shots
        //stays limited by deleting them once they leave the screen
        public void Move(Rectangle boundaries)
        {
            

            for (int i = 0; i < shots.Count();i++ )
            {
                shots[i].Move();

                //Keeps track of that once the shots leave the boundaries they get deleted
                //preventing that the array of shots stack up
                if (boundaries.Contains(shots[i].Area) == false)
                {
                   shots.RemoveAt(i);
                    
                }
               
            }


        }

        //Private function that keeps track of the number of shots that are currently within the boundaries
        //For each player respectively.
        public int numberOfShots()
        {

            return shots.Count();
        }


        public IEnumerator GetEnumerator()
        {
            return shots.GetEnumerator();

        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

       public void Remove(int i)
        {
            shots.RemoveAt(i);

        }


       public Shot ElementAt(int i)
       {
           return shots[i];
           
       }
    }
}
