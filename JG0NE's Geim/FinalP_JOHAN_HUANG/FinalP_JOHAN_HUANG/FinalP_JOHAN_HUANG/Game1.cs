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

namespace FinalP_JOHAN_HUANG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //The texture of the player
        Texture2D playerTexture;
        Texture2D playerShadow;
        //The position of the player
        Vector2 playerPosition = new Vector2(100, 100);
        //Player Rectangle
        Rectangle playerRectangle;
        //Shot Text
        Texture2D shotTexture;
        Texture2D shotShadow;
        //Shot Position
        List<Vector2> shotPositions;
        //Shot Rectangles
        Rectangle shotRectangles;
        //Enemy textures
        Texture2D enemyTexture1;
        Texture2D enemyShadow1;
        Texture2D enemyTexture2;
        Texture2D enemyShadow2;
        //Enemy Positions
        List<Vector2> enemyPositions1;
        List<Vector2> enemyPositions2;
        //Enemy Rectangles
        Rectangle enemyRectangles1;
        Rectangle enemyRectangles2;
        //Enemy Speed
        List<double> enemySpeed1;
        List<double> enemySpeed2;
        //Enemy2 angle
        List<float> enemyAngle;
        //Variable used to check the keyboardState
        KeyboardState keyboardState;
        //Variable used to check the previous keyboardState
        KeyboardState previousKeyboardState;
        //Fonts
        SpriteFont Font1;
        //Random
        Random RandomNumber = new Random();
        //Timer Variable
        float Timer;
        float Timer2;
        //Score Variable
        int score;
        //Kill count
        int killCount;
        //Background Textures
        Texture2D nightSkyText;
        Texture2D cityAText;
        Texture2D cityBText;
        //Background Positions
        Vector2 BGnightSky1Pos;
        Vector2 BGnightSky2Pos;
        Vector2 BGcityA1Pos;
        Vector2 BGcityA2Pos;
        Vector2 BGcityB1Pos;
        Vector2 BGcityB2Pos;
        //Background Speeds
        float scrollspeed1 = 0.25f;
        float scrollspeed2 = 2.0f;
        float scrollspeed3 = 3.0f;
        enum GameState { Main, Game, Pause, GameOver }
        GameState gameState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Changes the window size to 800 x 600
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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
            gameState = GameState.Main;
            shotPositions = new List<Vector2>();
            enemyPositions1 = new List<Vector2>();
            enemySpeed1 = new List<double>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("PlayerShip");
            playerShadow = Content.Load<Texture2D>("PlayerShipShadow");
            shotTexture = Content.Load<Texture2D>("shot");
            shotShadow = Content.Load<Texture2D>("shotShadow");
            enemyTexture1 = Content.Load<Texture2D>("enemy");
            enemyShadow1 = Content.Load<Texture2D>("enemyShadow");
            nightSkyText = Content.Load<Texture2D>("BGnightSky");
            cityAText = Content.Load<Texture2D>("BGcityA");
            cityBText = Content.Load<Texture2D>("BGcityB");
            Font1 = Content.Load<SpriteFont>("Font1");
            //Asign the background positions
            BGnightSky1Pos = Vector2.Zero;
            BGnightSky2Pos = new Vector2(nightSkyText.Width, 0);
            BGcityA1Pos = Vector2.Zero;
            BGcityA2Pos = new Vector2(cityAText.Width, 0);
            BGcityB1Pos = Vector2.Zero;
            BGcityB2Pos = new Vector2(cityBText.Width, 0);
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

            // TODO: Add your update logic here
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            switch (gameState)
            {
                case GameState.Main:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.Game;
                    }
                    break;
                case GameState.Game:
                    if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                    {
                        gameState = GameState.Pause;
                    }
                    if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                    {
                        playerPosition.X += -3;
                    }
                    if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                    {
                        playerPosition.X += 7;
                    }
                    if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                    {
                        playerPosition.Y += -5;
                    }
                    if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                    {
                        playerPosition.Y += 5;
                    }
                    if (keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        shotPositions.Add(new Vector2(playerPosition.X + playerTexture.Width - shotTexture.Width, playerPosition.Y + (playerTexture.Height / 2) - (shotTexture.Height / 2)));
                    }
                    //Update Rectangles
                    playerRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerTexture.Width, playerTexture.Height);
                    for (int i = 0; i < shotPositions.Count; i++)
                    {
                        shotRectangles = new Rectangle((int)shotPositions[i].X, (int)shotPositions[i].Y, shotTexture.Width, shotTexture.Height);
                        for (int j = 0; j < enemyPositions1.Count; j++)
                        {
                            enemyRectangles1 = new Rectangle((int)enemyPositions1[j].X, (int)enemyPositions1[j].Y, enemyTexture1.Width, enemyTexture1.Height);
                            //Check if shot and enemy collide
                            if (shotRectangles.Intersects(enemyRectangles1))
                            {
                                score += 100;
                                killCount++;
                                shotPositions.RemoveAt(i);
                                enemyPositions1.RemoveAt(j);
                            }
                        }
                    }

                    //Move shots
                    for (int i = 0; i < shotPositions.Count; i++)
                    {
                        shotPositions[i] += new Vector2(7, 0);
                    }
                    //Spawn Enemies
                    Timer += 0.01f;
                    Timer2 += 0.01f;
                    if (killCount >= 20)
                    {
                        if (Timer2 >= 0.8f)
                        {
                            enemyPositions1.Add(new Vector2(GraphicsDevice.Viewport.Width + enemyTexture1.Width, RandomNumber.Next(0, GraphicsDevice.Viewport.Height - enemyTexture1.Height)));
                            enemySpeed1.Add(RandomNumber.Next(5, 11));
                            Timer2 = 0.0f;
                        }
                    }
                    if (Timer >= 1.0f)
                    {
                        enemyPositions1.Add(new Vector2(GraphicsDevice.Viewport.Width + enemyTexture1.Width, RandomNumber.Next(0, GraphicsDevice.Viewport.Height - enemyTexture1.Height)));
                        enemySpeed1.Add(RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble() + RandomNumber.NextDouble());
                        Timer = 0.0f;
                    }
                    //Move enemies
                    for (int i = 0; i < enemyPositions1.Count; i++)
                    {
                        enemyPositions1[i] += new Vector2((float)-enemySpeed1[i], 0);
                    }
                    // Loop the backgrounds
                    if (BGnightSky1Pos.X < -nightSkyText.Width)
                    {
                        BGnightSky1Pos.X = BGnightSky2Pos.X + nightSkyText.Width;
                    }
                    if (BGnightSky2Pos.X < -nightSkyText.Width)
                    {
                        BGnightSky2Pos.X = BGnightSky1Pos.X + nightSkyText.Width;
                    }
                    BGnightSky1Pos.X -= scrollspeed1;
                    BGnightSky2Pos.X -= scrollspeed1;
                    if (BGcityA1Pos.X < -cityAText.Width)
                    {
                        BGcityA1Pos.X = BGcityA2Pos.X + cityAText.Width;
                    }
                    if (BGcityA2Pos.X < -cityAText.Width)
                    {
                        BGcityA2Pos.X = BGcityA1Pos.X + cityAText.Width;
                    }
                    BGcityA1Pos.X -= scrollspeed2;
                    BGcityA2Pos.X -= scrollspeed2;
                    if (BGcityB1Pos.X < -cityBText.Width)
                    {
                        BGcityB1Pos.X = BGcityB2Pos.X + cityBText.Width;
                    }
                    if (BGcityB2Pos.X < -cityBText.Width)
                    {
                        BGcityB2Pos.X = BGcityB1Pos.X + cityBText.Width;
                    }
                    BGcityB1Pos.X -= scrollspeed3;
                    BGcityB2Pos.X -= scrollspeed3;
                    playerPosition.X -= scrollspeed2;
                    break;
                case GameState.Pause:
                    if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                    {
                        gameState = GameState.Game;
                    }
                    break;
                case GameState.GameOver:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.Main;
                    }
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (gameState == GameState.Main)
            {
                spriteBatch.DrawString(Font1, "Press Enter to Play", new Vector2(250, 300), Color.Red);
            }
            if (gameState == GameState.Game)
            {
                spriteBatch.Draw(nightSkyText, BGnightSky1Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(nightSkyText, BGnightSky2Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(cityAText, BGcityA1Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(cityAText, BGcityA2Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                for (int i = 0; i < shotPositions.Count; i++)
                {
                    spriteBatch.Draw(shotTexture, shotPositions[i], Color.White);
                }
                for (int i = 0; i < enemyPositions1.Count; i++)
                {
                    spriteBatch.Draw(enemyTexture1, enemyPositions1[i], Color.White);
                }
                spriteBatch.Draw(playerTexture, playerPosition, Color.White);
                spriteBatch.Draw(cityBText, BGcityB1Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(cityBText, BGcityB2Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(playerShadow, playerPosition, Color.White);
                for (int i = 0; i < enemyPositions1.Count; i++)
                {
                    spriteBatch.Draw(enemyShadow1, enemyPositions1[i], Color.White);
                }
                for (int i = 0; i < shotPositions.Count; i++)
                {
                    spriteBatch.Draw(shotShadow, shotPositions[i], Color.White);
                }
                spriteBatch.DrawString(Font1, "Score: " + score, new Vector2(16, 16), Color.Red);
            }
            if (gameState == GameState.Pause)
            {
                spriteBatch.DrawString(Font1, "Press Esc to Continue Playing", new Vector2(200, 300), Color.Red);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
