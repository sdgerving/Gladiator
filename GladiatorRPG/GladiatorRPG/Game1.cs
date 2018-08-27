using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Security.Cryptography;

namespace GladiatorRPG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static AnimatedSprite sprite;
        Random random = new Random();
        public static Vector2 currentcorrd = new Vector2();
        public static string currenttile,mapid;
        public static MouseCord mouse = new MouseCord();
        public static DrawBoard drawbrd = new DrawBoard();
        public static SpriteFont Corsiva31,Corsiva16;
        public static Rectangle empireRec, rollbutton, mapcheck;
        public static Texture2D pixel,eastwest,northeast,northsouth,northwest,southwest,enemy;
        public static Rectangle[,] maparray = new Rectangle[11,11];
        public static int[,] MapPieceNum = new int[11, 11];
        public static string[,] MapPieceName = new string[11, 11];
        public static List<Rectangle> reclist = new List<Rectangle>();
        public static int mapstartlocx, mapstartlocy;
        public static int gameboardposx, gameboardposy;
        public static bool test;
        string fuck ;
        private static Random rnd = new Random(Environment.TickCount);
        public static bool mousetf, barrier;
        // public static List<AnimatedSprite> sprite = new List<AnimatedSprite>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 980;   // set this value to the desired height of your window
            graphics.IsFullScreen = false;
        
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Corsiva31 = Content.Load<SpriteFont>("Corsiva31");
            Corsiva16 = Content.Load<SpriteFont>("Corsiva16");
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            mouse.clickbox.Add(rollbutton);
            populatemap();
             eastwest = Content.Load<Texture2D>("eastwest");
            northeast = Content.Load<Texture2D>("northeast");
            northsouth= Content.Load<Texture2D>("northsouth");
            northwest = Content.Load<Texture2D>("northwest");
            southwest = Content.Load<Texture2D>("southwest");

            Texture2D texture = Content.Load<Texture2D>("devil");
            sprite = new AnimatedSprite(texture, 4, 3);
            //sprite = new AnimatedSprite(Content.Load<Texture2D>("knightlong"), 0, 32, 32);

            gameboardposx =500;
            gameboardposy =200;
            sprite.Position = new Vector2(gameboardposx+1 , gameboardposy-3 );
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            mouse.myMouse(gameTime);
            //drawbrd.myMouse(gameTime);
           ;
            sprite.HandleSpriteMovement(gameTime);
            mouse.checkmouse(rollbutton);
            mouse.checkboard(maparray);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
       
        public int randomnumber(int minnum,int maxnum)
        {
            int numtemp=0;
            numtemp = (int)Math.Round(random.NextDouble() * (maxnum - minnum) + minnum);
            return numtemp;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.DrawString(Corsiva31, "mouse x:" + mouse.m_mousePos.X.ToString(), new Vector2(1400, 25), Color.Red);
            spriteBatch.DrawString(Corsiva31, "mouse y:" + mouse.m_mousePos.Y.ToString(), new Vector2(1400, 55), Color.Red);
            spriteBatch.DrawString(Corsiva31, "Current Click:" + fuck, new Vector2(1400, 85), Color.Red);
            spriteBatch.DrawString(Corsiva31, "Current Tile:" + MapPieceName[mouse.loci, mouse.locj], new Vector2(1400, 115), Color.Red);
            spriteBatch.DrawString(Corsiva31, "Click character" + AnimatedSprite.stop, new Vector2(1400, 145), Color.Red);
            
            DrawMap(gameboardposx , gameboardposy);
            
            sprite.Draw(spriteBatch, new Vector2(sprite.Position.X, sprite.Position.Y));
            
            //mouse.checkcharacter(maparray);
            drawhovormap();
            drawChaMap();
            drawrollbtn(960, 800);
            
            
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void drawrollbtn(int rollboxX, int rollboxY)
        {
            DrawBorder(rollbutton = new Rectangle(rollboxX, rollboxY, 170, 70), 1, Color.Yellow);
            spriteBatch.DrawString(Corsiva31, "Roll", new Vector2(rollboxX + 37, rollboxY + 6), Color.LawnGreen);

            if (mouse.hover == "roll")
            {
                DrawBorder(rollbutton, 50, Color.Ivory);
                spriteBatch.DrawString(Corsiva31, "Roll", new Vector2(rollboxX + 37, rollboxY + 6), Color.Blue);

            }
           
            if (mouse.clickstate == "roll")
            {
                populatemap();
                mouse.clickstate = "null";
                MouseCord.mousetf = false;
            }

            if (mouse.hover == "num")
            {
                DrawBorder(mapcheck = new Rectangle((int)mouse.m_mousePos.X - 25, (int)mouse.m_mousePos.Y - 25, 50, 50), 3, Color.Red);

            }
            if (mouse.clickstate == "num")
            {
                currentcorrd = new Vector2(mouse.x, mouse.y);
                fuck = "fuck Yeah!!!!";
                mouse.clickstate = "null";
                MouseCord.mousetf = false;


            }
        }
        public void drawhovormap()
        {
            
        }
        public void drawChaMap()
        {
            if (mouse.hovercha == "char")
            {
                DrawBorder(mapcheck = new Rectangle((int)AnimatedSprite.position.X + 3, (int)AnimatedSprite.position.Y + 3, 50, 50), 1, Color.Ivory);

            }
            if (mouse.clickstate == "char")
            {
                mouse.clickstate = "null";
                MouseCord.mousetf = false;
                currentcorrd = new Vector2(mouse.x, mouse.y);
                
            }
        }
        public void populatemap()
        {
            int i, j;
            
            for (i = 1; i <= 10; i++)
            {
                for (j = 1; j <= 10; j++)
                {
                    MapPieceNum[i, j] = RandomNum(0, 4);
                }
            }
            
        }
        private int RandomNum(int Lower, int Upper)
        {

            return rnd.Next(Lower, Upper);

        }
        public void DrawMap(int mapboxX, int mapboxY)
        {

            int tempx, tempy,i=0,j=0;
            DrawBorder(empireRec = new Rectangle(mapboxX, mapboxY, 506, 506), 3, Color.Red);

            mapstartlocx = mapboxX;
            mapstartlocy = mapboxY;
            tempx = mapboxX;
            tempy = mapboxY;
            DrawBorder(maparray[i, j] = new Rectangle(tempx + 3, tempy + 3, 500, 500), 500, Color.Green);
            for ( i=1;i<=10;i++)
            {
                for ( j=1;j<=10;j++)
                {
                    DrawBorder(maparray[i, j] = new Rectangle(tempx + 3, tempy + 3, 50, 50), 1, Color.Yellow);
                    reclist.Add(maparray[i, j]);
                    spriteBatch.DrawString(Corsiva31, MapPieceNum[i,j].ToString(),new Vector2(tempx + 3, tempy + 3), Color.Blue);
                    if(MapPieceNum[i,j]==0)
                    {
                        MapPieceName[i,j] = "eastwest";
                        spriteBatch.Draw(eastwest, new Rectangle(tempx + 3, tempy + 3, 50, 50), Color.White);
                    }
                    if (MapPieceNum[i, j] == 1)
                    {
                        MapPieceName[i, j] = "northeast";
                        spriteBatch.Draw(northeast, new Rectangle(tempx + 3, tempy + 3, 50, 50), Color.White);
                    }
                    if (MapPieceNum[i, j] == 2)
                    {
                        MapPieceName[i, j] = "northsouth";
                        spriteBatch.Draw(northsouth, new Rectangle(tempx + 3, tempy + 3, 50, 50), Color.White);
                    }
                    if (MapPieceNum[i, j] == 3)
                    {
                        MapPieceName[i, j] = "northwest";
                        spriteBatch.Draw(northwest, new Rectangle(tempx + 3, tempy + 3, 50, 50), Color.White);
                    }
                    if (MapPieceNum[i, j] == 4)
                    {
                        MapPieceName[i, j] = "southwest";
                        spriteBatch.Draw(southwest, new Rectangle(tempx + 3, tempy + 3, 50, 50), Color.White);
                    }

                    if (j == 10)
                    {
                        tempx = mapboxX;
                        tempy += 50;
                    }
                    else tempx += 50;
                }
            }
           







        }
        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);
            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), borderColor);
        }

    }
}
