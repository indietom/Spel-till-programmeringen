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
        //The position of the player
        Vector2 playerPosition = new Vector2(100, 100);
        //Variable used to check the keyboardState
        KeyboardState keyboardState;
        //Variable used to check the previous keyboardState
        KeyboardState previousKeyboardState;
        //Background Textures
        Texture2D nightSkyText;
        Texture2D cityAText;
        Texture2D cityBText;
        //Texture2D nyanText;
        //Background Positions
        Vector2 BGnightSky1Pos;
        Vector2 BGnightSky2Pos;
        Vector2 BGcityA1Pos;
        Vector2 BGcityA2Pos;
        Vector2 BGcityB1Pos;
        Vector2 BGcityB2Pos;
        //Vector2 nyan1Pos;
        //Vector2 nyan2Pos;
        //Background Speeds
        float scrollspeed1 = 0.25f;
        float scrollspeed2 = 2.0f;
        float scrollspeed3 = 3.0f;
        //float scrollspeedNyan = 2.5f;
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
            playerTexture = Content.Load<Texture2D>("nyancat");
            nightSkyText = Content.Load<Texture2D>("BGnightSky");
            cityAText = Content.Load<Texture2D>("BGcityA");
            cityBText = Content.Load<Texture2D>("BGcityB");
            //nyanText = Content.Load<Texture2D>("nyan");
            //Asign the background positions
            BGnightSky1Pos = Vector2.Zero;
            BGnightSky2Pos = new Vector2(nightSkyText.Width, 0);
            BGcityA1Pos = Vector2.Zero;
            BGcityA2Pos = new Vector2(cityAText.Width, 0);
            BGcityB1Pos = Vector2.Zero;
            BGcityB2Pos = new Vector2(cityBText.Width, 0);
            //nyan1Pos = Vector2.Zero;
            //nyan2Pos = new Vector2(nyanText.Width, 0);
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
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                playerPosition.X += -5;
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                playerPosition.X += 5;
            }
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                playerPosition.Y += -5;
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                playerPosition.Y += 5;
            }
            switch (gameState)
            {
                case GameState.Main:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.Game;
                    }
                    break;
                case GameState.Game:
                    if (keyboardState.IsKeyDown(Keys.P))
                    {
                        gameState = GameState.Pause;
                    }
                    break;
                case GameState.Pause:
                    if (keyboardState.IsKeyDown(Keys.P))
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
            //if (nyan1Pos.X > nyanText.Width)
            //{
            //    nyan1Pos.X = nyan2Pos.X - nyanText.Width;
            //}
            //if (nyan2Pos.X > nyanText.Width)
            //{
            //    nyan2Pos.X = nyan1Pos.X - nyanText.Width;
            //}
            //nyan1Pos.X += scrollspeedNyan;
            //nyan2Pos.X += scrollspeedNyan;
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
            spriteBatch.Draw(nightSkyText, BGnightSky1Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(nightSkyText, BGnightSky2Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            //spriteBatch.Draw(nyanText, nyan1Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            //spriteBatch.Draw(nyanText, nyan2Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(cityAText, BGcityA1Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(cityAText, BGcityA2Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(playerTexture, playerPosition, Color.White);
            spriteBatch.Draw(cityBText, BGcityB1Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(cityBText, BGcityB2Pos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
