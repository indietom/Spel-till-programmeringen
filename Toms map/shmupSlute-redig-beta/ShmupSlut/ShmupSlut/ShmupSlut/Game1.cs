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
        paused,
        levelTransition
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
        List<powerUp> powerUps = new List<powerUp>();
        List<hitEffect> hitEffects = new List<hitEffect>();
        List<boss> bosses = new List<boss>();
        List<gib> gibs = new List<gib>();
        protected override void Initialize()
        {
            level = 2;
            startedGame = false;
            spawnManager = new spawnManager(backgroundObjects, enemies, level);
            projectiles.Clear();
            enemies.Clear();
            particles.Clear();
            enemyProjectiles.Clear();
            explosions.Clear();
            powerUps.Clear();
            hitEffects.Clear();
            gibs.Clear();
            player = new player();
            //bosses.Add(new boss(new Vector2(500,0), 2));
            base.Initialize();
        }

        public void resetLevel()
        {
            player.kills = 0;
            particles.Clear();
            projectiles.Clear();
            enemies.Clear();
            enemyProjectiles.Clear();
            explosions.Clear();
            powerUps.Clear();
            gibs.Clear();
            bosses.Clear();
            hitEffects.Clear();
            backgroundObjects.Clear();
            player.pos = new Vector2(320, 240);
            player.hp = 10;
            player.powerDownCount = 128 * 10;
            player.gunType = 1;
            spawnManager.difficulty = 0;
            spawnManager.difficultyCount = 0;
            gui.nextDialogCount = 0;
            gui.currentDialog = 0;
            gui.dialogCount = 0;
        }

        Texture2D spritesheet;
        Texture2D levelCompleted;
        Texture2D gameOver;
        SpriteFont font;
        SpriteFont fontBmp;
        SpriteFont fontSmall;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritesheet = Content.Load<Texture2D>("spritesheet");
            levelCompleted = Content.Load<Texture2D>("levelCompleted");
            gameOver = Content.Load<Texture2D>("gameOver");
            font = Content.Load<SpriteFont>("font");
            fontBmp = Content.Load<SpriteFont>("bitmapFont");
            fontSmall = Content.Load<SpriteFont>("fontSmall");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            GamePadState prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            Random random = new Random();

            if (keyboard.IsKeyDown(Keys.F1) && prevKeyboard.IsKeyUp(Keys.F1) || gamepad.Buttons.Back == ButtonState.Pressed && prevGamepad.Buttons.Back == ButtonState.Released)
            {
                graphics.ToggleFullScreen();
            }

            switch (gameState)
            {
                case GameState.startScreen:
                    if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || keyboard.IsKeyDown(Keys.Enter) && prevKeyboard.IsKeyUp(Keys.Enter) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
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
                    if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || keyboard.IsKeyDown(Keys.Enter) && prevKeyboard.IsKeyUp(Keys.Enter)||gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
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
                case GameState.levelTransition:
                    if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || keyboard.IsKeyDown(Keys.Enter) && prevKeyboard.IsKeyUp(Keys.Enter) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
                    {
                        resetLevel();
                        gameState = GameState.game;
                    }
                    break;
                case GameState.gameOver:
                    gibs.Clear();
                    if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || keyboard.IsKeyDown(Keys.Enter) && prevKeyboard.IsKeyUp(Keys.Enter) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
                    {
                        resetLevel();
                        Initialize();
                        gameState = GameState.menu;
                    }
                    break;
                case GameState.game:

                    if (!this.IsActive)
                    {
                        gameState = GameState.menu;
                    }
                    player.Input(projectiles, particles);
                    player.Update();
                    if (player.lives <= -1)
                    {
                        gameState = GameState.gameOver;
                    }
                    player.CheckHealth(healthbar, particles);
                    spawnManager.Spawn(level, backgroundObjects, enemies, powerUps, bosses);
                    gui.Update(player, level);

                    foreach (gib g in gibs)
                    {
                        g.movment();
                    }
                    foreach (boss b in bosses)
                    {
                        b.Attack(enemyProjectiles, player);
                        b.Movment(player);
                        b.checkHealth(projectiles, particles, ref player, ref level);
                        b.Update();
                        if (b.nextLevelCount >= 128 * 2)
                        {
                            gameState = GameState.levelTransition;
                        }
                    }
                    foreach (enemyProjectile ep in enemyProjectiles)
                    {
                        ep.Movment(player);
                        ep.Update(player, projectiles, gibs);
                    }
                    foreach (hitEffect he in hitEffects)
                    {
                        he.Update();
                    }
                    foreach (enemy e in enemies)
                    {
                        e.Attack(enemyProjectiles, player);
                        e.Movment(player);
                        e.Animation();
                        e.CheckHealth(projectiles, explosions, hitEffects, gibs, ref player);
                    }
                    foreach (explosion ex in explosions)
                    {
                        ex.Animation();
                    }
                    foreach (powerUp pu in powerUps)
                    {
                        pu.Update(ref player);
                        pu.Movment();
                    }
                    foreach (backgroundObject bo in backgroundObjects)
                    {
                        bo.Movment();
                    }
                    foreach (projectile p in projectiles)
                    {
                        p.Movment();
                        p.Update(particles);
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
                    for (int i = 0; i < gibs.Count; i++)
                    {
                        if (gibs[i].destroy)
                        {
                            gibs.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < hitEffects.Count; i++)
                    {
                        if (hitEffects[i].destroy)
                        {
                            hitEffects.RemoveAt(i);
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
                    for (int i = 0; i < powerUps.Count; i++)
                    {
                        if (powerUps[i].destroy)
                        {
                            powerUps.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < explosions.Count; i++)
                    {
                        if (explosions[i].destroy)
                        {
                            explosions.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < bosses.Count; i++)
                    {
                        if (bosses[i].destroy)
                        {
                            bosses.RemoveAt(i);
                        }
                    }
                    break;
                case GameState.howTo:
                    if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || keyboard.IsKeyDown(Keys.Enter) && prevKeyboard.IsKeyUp(Keys.Enter) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
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
                    spriteBatch.DrawString(fontBmp, "PRESS SPACE OR A TO START", new Vector2(320 - 10 * 20, 240), Color.White);
                    break;
                case GameState.menu:
                    menu.Draw(spriteBatch, spritesheet,fontBmp, startedGame);
                    break;
                case GameState.levelTransition:
                    spriteBatch.Draw(levelCompleted, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(fontBmp, "Kills this level: " + player.kills.ToString(), new Vector2(20, 140),Color.White);
                    spriteBatch.DrawString(fontBmp, "Score: " + player.score.ToString(), new Vector2(20, 240), Color.White);
                    break;
                case GameState.game:
                    foreach (backgroundObject bo in backgroundObjects) { if(bo.type == "back" ) bo.DrawSprite(spriteBatch, spritesheet); }
                    foreach (projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
                    player.DrawSprite(spriteBatch, spritesheet);
                    if(!player.vulnerable) {spriteBatch.Draw(spritesheet, player.pos, new Rectangle(1, 26, 24, 24), Color.White);}
                    foreach (enemyProjectile ep in enemyProjectiles) { if (ep.type == 3) ep.DrawSprite(spriteBatch, spritesheet, 1); else ep.DrawSprite(spriteBatch, spritesheet); }
                    foreach (enemy e in enemies) { if (!e.rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, 1.0f); }
                    foreach (boss b in bosses) { b.DrawSprite(spriteBatch, spritesheet); }
                    foreach (hitEffect he in hitEffects) { he.DrawSprite(spriteBatch, spritesheet); }
                    foreach (particle p in particles) { p.DrawSprite(spriteBatch, spritesheet); }
                    foreach (gib g in gibs) { spriteBatch.Draw(spritesheet, g.pos, new Rectangle(g.imx, g.imy, g.width, g.height), Color.White, g.rotation, new Vector2(g.width / 2, g.height / 2), 1.0f, SpriteEffects.None, 0); }
                    foreach (powerUp pu in powerUps) { pu.DrawSprite(spriteBatch, spritesheet); }
                    foreach (explosion ex in explosions) { ex.DrawSprite(spriteBatch, spritesheet); }
                    foreach (backgroundObject bo in backgroundObjects) { if (bo.type == "fore") bo.DrawSprite(spriteBatch, spritesheet, bo.zoom); }
                    gui.drawGui(spriteBatch, fontSmall, spritesheet, healthbar, player, level);
                    break;
                case GameState.howTo:
                    if (gamepad.IsConnected)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(30, 30), new Rectangle(26, 376, 15, 13), Color.White);
                        spriteBatch.DrawString(fontBmp, " Left joystick to move \n a to shoot \n b to use the special power \n start to pause and unpause and select to \n toggle fullscreen \n \n Press a To Return", new Vector2(320 - 14 * 22, 180), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(30, 30), new Rectangle(51, 376, 15, 7), Color.White);
                        spriteBatch.DrawString(fontBmp, " W,A,S,D or the arrow keys to move \n Space to shoot \n Left shift to use the special power \n Escape to pause and unpause and F1 to \n toggle fullscreen \n \n Press Space To Return", new Vector2(320 - 14 * 22, 180), Color.White);
                    }
                    break;
                case GameState.gameOver:
                    spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
