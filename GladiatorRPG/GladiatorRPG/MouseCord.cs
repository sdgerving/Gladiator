using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladiatorRPG
{
    public class MouseCord
    {
        public  Vector2 m_mousePos;
        public List<Rectangle> clickbox = new List<Rectangle>();

      
        public string hover = "null", clickstate = "null", hovercha = "null";
        public int x, y, loci, locj;
        public static MouseState currentMouseState;
        public static MouseState previousMouseState;

        public static bool mousetf;

        public void myMouse(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            m_mousePos.X = mouseState.X;
            m_mousePos.Y = mouseState.Y;
            MouseState currentMouseState = Mouse.GetState(); //Get the state
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                previousMouseState.LeftButton == ButtonState.Released) //Will be true only if the user is currently clicking, but wasn't on the previous call.
            {
                mousetf = !mousetf; //Toggle the state between true and false.
            }
            previousMouseState = currentMouseState;
        }

        public void checkmouse(Rectangle rollbutton)
        {

            Point p = new Point(Convert.ToInt32(m_mousePos.X), Convert.ToInt32(m_mousePos.Y));
            
            foreach (Rectangle rect in clickbox)
            {



                if (rollbutton.Contains(p))
                {
                    hover = "roll";
                    if (mousetf == true)
                    {
                        clickstate = "roll";

                    }
                }



                else
                {
                    hover = "null";
                    mousetf = false;
                }
            }
        }
        public void calclocation(int loci, int locj)
        {

            x = Game1.mapstartlocx - 50 + (locj * 50);
            y = Game1.mapstartlocy - 50 + (loci * 50);
            if(locj==0 )
            {
                x = 0;
            }
            if (loci == 0)
            {
                y = 0;
            }
        }
      
        public void checkboard(Rectangle[,] maparray)
        {
            Point character = new Point(Convert.ToInt32(AnimatedSprite.position.X), Convert.ToInt32(AnimatedSprite.position.Y));
            Point p = new Point(Convert.ToInt32(m_mousePos.X), Convert.ToInt32(m_mousePos.Y));

            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    if (maparray[i, j].Contains(p))
                    {
                        loci = i;
                        locj = j;
                        calclocation(loci, locj);
                        hover = "num";
                        if (mousetf == true)
                        {
                            clickstate = "num";
                        }

                    }
                    
                    if (maparray[i, j].Contains(character))
                    {
                        loci = i;
                        locj = j;
                        calclocation(loci, locj);
                        hovercha = "char";


                        if (mousetf == true && maparray[i, j].Contains(p))
                        {
                            clickstate = "char";
                        }

                    }
                    


                }
            }
           
            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    
                }
            }

        }




    }
        
    
}
