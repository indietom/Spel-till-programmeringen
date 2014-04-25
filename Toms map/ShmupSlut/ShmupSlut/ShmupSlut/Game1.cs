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

namespace ShmupSlut
{
    public enum GameState
    {
        startScreen,
        menu,
        game,
        gameOver,
        howTo,
        paused
    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 640;
        }

        public bool startedGame;
        public int level;
        KeyboardState keyboard;
        GamePadState gamepad;
        player player = new player();
        menu menu = new menu();
        healthbar healthbar = new healthbar();
        GameState gameState = new GameState();
        spawnManager spawnManager;
        gui gui = new gui();
        List<particle> particles = new List<particle>();
        List<projectile> projectiles = new List<projectile>();
        List<backgroundObject> backgroundObjects = new List<backgroundObject>();
        List<enemy> enemies = new List<enemy>();
        List<enemyProjectile> enemyProjectiles = new List<enemyProjectile>();
        List<explosion> explosions = new List<explosion>();
        protected override void Initialize()
        {
            level = 1;
            spawnManager = new spawnManager(backgroundObjects, enemies, level);
            projectiles.Clear();
            player = new player();
            base.Initialize();
        }

        Texture2D spritesheet;
        SpriteFont font;
        SpriteFont fontSmall;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritesheet = Content.Load<Texture2D>("spritesheet");
            font = Content.Load<SpriteFont>("font");
            fontSmall = Content.Load<SpriteFont>("fontSmall");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            GamePadState prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            Random random = new Random();

            if (keyboard.IsKeyDown(Keys.F2) && prevKeyboard.IsKeyUp(Keys.F2))
            {
                particles.Add(new particle(new Vector2(320, 240), 1, "red", random.Next(5, 10), 0.1f, random.Next(360),4)); 
            }

            switch (gameState)
            {
                case GameState.startScreen:
                    if (keyboard.IsKeyDown(Keys.Space) || gamepad.Buttons.A == ButtonState.Pressed)
                    {
                        gameState = GameState.menu;
                    }
                    break;
                case GameState.menu:
                    menu.Update();
                    if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) || gamepad.Buttons.Start == ButtonState.Pressed && prevGamepad.Buttons.Start == ButtonState.Released)
                    {
                        if (startedGame)
                        {
                            gameState = GameState.game;
                        }
                    }
                    if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
                    {
                        switch (menu.selected)
                        {
                            case 1:
                                gameState = GameState.game;
                                if (!startedGame)
                                {
                                    Initialize();
                                    startedGame = true;
                                }
                                break;
                            case 2:
                                gameState = GameState.howTo;
                                break;
                            case 3:
                                this.Exit();
                                break;
                        }
                    }
                    break;
                case GameState.game:
                    player.Input(projectiles);
                    player.Update();
                    player.CheckHealth(healthbar, particles);
                    spawnManager.Spawn(level, backgroundObjects, enemies);
                    gui.update(player, level);
                    foreach (enemyProjectile ep in enemyProjectiles)
                    {
                        ep.Movment();
                    }
                    foreach (enemy e in enemies)
                    {
                        e.Attack(enemyProjectiles);
                        e.Movment(player);
                        e.Animation();
                        e.CheckHealth(projectiles, explosions, ref player);
                    }
                    foreach (explosion ex in explosions)
                    {
                        ex.Animation();
                    }
                    foreach (backgroundObject bo in backgroundObjects)
                    {
                        bo.Movment();
                    }
                    foreach (projectile p in projectiles)
                    {
                        p.Movment();
                    }
                    foreach (particle p in particles)
                    {
                        p.Movemnt();
                    }
                    if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) || gamepad.Buttons.Start == ButtonState.Pressed && prevGamepad.Buttons.Start == ButtonState.Released)
                    {
                        gameState = GameState.menu;
                        menu.selected = 1;
                    }
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        if (projectiles[i].destroy)
                        {
                            projectiles.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemyProjectiles.Count; i++)
                    {
                        if (enemyProjectiles[i].destroy)
                        {
                            enemyProjectiles.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < particles.Count; i++)
                    {
                        if (particles[i].destroy)
                        {
                            particles.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].destroy)
                        {
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < backgroundObjects.Count; i++)
                    {
                        if (backgroundObjects[i].destroy)
                        {
                            backgroundObjects.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < explosions.Count; i++)
                    {
                        if (explosions[i].destroy)
                        {
                            explosions.RemoveAt(i);
                        }
                    }
                    break;
                case GameState.howTo:
                    if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
                    {
                        gameState = GameState.menu;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Random random = new Random();
            gamepad = GamePad.GetState(PlayerIndex.One);
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.startScreen:
                    spriteBatch.DrawString(fontSmall, "Press space to start", new Vector2(320 - 10 * 10, 240), Color.White);
                    break;
                case GameState.menu:
                    menu.Draw(spriteBatch, font, startedGame);
                    break;
                case GameState.game:
                    switch (level)
                    {
                        case 2:
                            for(int x = 0; x < 24*7; x++)
                            {
                                for(int y = 0; y < 24*5; y++)
                                {
                                    spriteBatch.Draw(spritesheet, new Vector2(x*24,y*24), new Rectangle(551, 126, 24, 24), Color.White);
                                }
                            }
                            break;
                    }
                    foreach (backgroundObject bo in backgroundObjects) { if(bo.type == "back" ) bo.DrawSprite(spriteBatch, spritesheet); }
                    foreach (projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
                    player.DrawSprite(spriteBatch, spritesheet);
                    foreach (enemyProjectile ep in enemyProjectiles) { ep.DrawSprite(spriteBatch, spritesheet); }
                    foreach (enemy e in enemies) { if (!e.rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, 1.0f); }
                    foreach (particle p in particles) { p.DrawSprite(spriteBatch, spritesheet); }
                    foreach (explosion ex in explosions) { ex.DrawSprite(spriteBatch, spritesheet); }
                    foreach (backgroundObject bo in backgroundObjects) { if (bo.type == "fore") bo.DrawSprite(spriteBatch, spritesheet, bo.zoom); }
                    gui.drawGui(spriteBatch, fontSmall, spritesheet, healthbar, player, level);
                    break;
                case GameState.howTo:
                    if (gamepad.IsConnected)
                    {
                        spriteBatch.DrawString(font, " Left joystick to move \n a to shoot \n b to use the special power \n start to pause and unpause \n \n Press a To Return", new Vector2(320 - 14 * 10, 220), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, " W,A,S,D or the arrow keys to move \n Space to shoot \n Left shift to use the special power \n Escape to pause and unpause \n \n Press Space To Return", new Vector2(320 - 14 * 10, 220), Color.White);
                    }
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
