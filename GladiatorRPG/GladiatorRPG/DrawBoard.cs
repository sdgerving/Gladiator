using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladiatorRPG
{
    public class DrawBoard
    {
        public Vector2 m_mousePos;
        public List<Rectangle> clickbox = new List<Rectangle>();
        public List<Rectangle> gameboard = new List<Rectangle>();

        public string hover = "null", clickstate = "null",hovercha="null";
        public  int x, y, loci, locj;
        public static MouseState currentMouseState;
        public static MouseState previousMouseState;

        public static bool mousetf;
        public void checkposition()
        {
            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    if (Game1.MapPieceNum[i, j] == 3)
                    {
                        AnimatedSprite.stop = true;
                    }
                }
            }
        }
        
        


    }         
}
