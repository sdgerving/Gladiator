using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GladiatorRPG;

namespace GladiatorRPG
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        
        float timer = 0f;
        float interval = 150f;
        KeyboardState currentKBState;
        KeyboardState previousKBState;
        KeyboardState keyCurrent;
        KeyboardState keyLast;
        public static Vector2 position, destination;
        Vector2 origin;
        public static bool stop = true;
        int spriteSpeed = 10;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public bool Stop
        {
            get { return stop; }
            set { stop = value; }
        }
        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

       


        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }



        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            
            
        }
        public void HandleSpriteMovement(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();
            if (currentKBState.IsKeyDown(Keys.Right))
            {
                if (currentKBState != previousKBState)
                {
                    currentFrame = 6;
                }
                if (timer > interval)
                {

                    currentFrame++;
                    position.X += spriteSpeed;
                    if (currentFrame == 9)
                    {
                        currentFrame = 6;
                        
                    }
                    timer = 0f;
                }

            }
            else if (currentKBState.IsKeyDown(Keys.Left))
            {
                if (currentKBState != previousKBState)
                {
                    currentFrame = 3;
                }
                if (timer > interval)
                {
                    currentFrame++;
                    position.X -= spriteSpeed;
                    if (currentFrame == 6)
                    {
                        currentFrame = 3;
                        
                    }
                    timer = 0f;
                }


            }
            else if (currentKBState.IsKeyDown(Keys.Up))
            {
                if (currentKBState != previousKBState)
                {
                    currentFrame = 9;
                }
                if (timer > interval)
                {
                    currentFrame++;
                    position.Y -= spriteSpeed;
                    if (currentFrame >= 12)
                    {
                        currentFrame = 9;
                        
                    }
                    timer = 0f;
                }

            }
            else if (currentKBState.IsKeyDown(Keys.Down))
            {
                if (currentKBState != previousKBState)
                {
                    currentFrame = 0;
                }
                if (timer > interval)
                {
                    currentFrame++;
                    position.Y += spriteSpeed;
                    if (currentFrame >= 3)
                    {
                        currentFrame = 0;
                        
                    }
                    timer = 0f;
                }
            }

        }



    }
}
