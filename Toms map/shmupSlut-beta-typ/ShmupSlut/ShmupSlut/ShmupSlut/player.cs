using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShmupSlut
{
    class player:objects
    {
        public bool inputActive;
        public bool respawnCutScene;

        public int gunType;
        public int powerDownCount;
        public int fireRate;
        public int score;
        public int lives;
        public int respawnDelay;
        public int addHealthDelay;
        public int bleedDelay;
        public int kills;
        public int totalKills;

        public bool vulnerable;

        public Rectangle playerC;

        KeyboardState keyboard;
        GamePadState gamepad;

        public player()
        {
            vulnerable = true;
            SetSize(24, 24);
            pos = new Vector2(320, 240);
            gunType = 1;
            inputActive = true;
            hp = 10;
            lives = 5;
            score = 0;
            powerDownCount = 128 * 10;
        }

        public void CheckHealth(healthbar healthbar, List<particle> particles)
        {
            Random random = new Random();
            if (hp <= 3)
            {
                bleedDelay += 1;
                if (bleedDelay >= 5)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        particles.Add(new particle(new Vector2(pos.X + 12, pos.Y + 12 - 5), 1, "smoke", random.Next(3, 5), 0.01f, random.Next(-300, -250)));
                        particles.Add(new particle(new Vector2(pos.X + 12, pos.Y + 12 - 5), 1, "fire smoke", random.Next(3, 5), 0.01f, random.Next(-300, -250)));
                    }
                    bleedDelay = 0;
                }
            }
            healthbar.height = hp * 10;
            if (hp < 0)
            {
                hp = 0;
            }
            if (hp > 10)
            {
                hp = 10;
            }
            if (hp <= 0)
            {
                gunType = 1;
                powerDownCount = 128 * 10;
                imx = 1;
                if (pos.X > 640 || pos.Y > 480)
                {
                    vulnerable = false;
                    if (respawnDelay <= 0)
                    {
                        lives -= 1;
                    }
                    respawnDelay = 1;
                    pos.Y = 480;
                }
                else
                {
                    imx = Frame(2);
                    pos.X += 3;
                    pos.Y += 3;
                }
                inputActive = false;
            }
            if (respawnCutScene)
            {
                bleedDelay = 0;
                imx = 1;
                pos.Y -= 1;
                if (hp <= 9)
                {
                    addHealthDelay += 1;
                    if (addHealthDelay >= 16)
                    {
                        hp += 1;
                        addHealthDelay = 0;
                    }
                }
                if (pos.Y <= 240)
                {
                    vulnerable = true;
                    inputActive = true;
                    respawnDelay = 0;
                    powerDownCount = 128 * 10;
                    respawnCutScene = false;
                }
            }
            if (respawnDelay >= 1)
            {
                pos.X = 320;
                respawnDelay += 1;
                respawnCutScene = true;
            }
        }

        public void Update()
        {
            playerC = new Rectangle((int)pos.X, (int)pos.Y, 24, 24);
            if (fireRate >= 1)
            {
                fireRate += 1;
                if (fireRate >= 64+32 && gunType == 4)
                {
                    fireRate = 0;
                }
                if (fireRate >= 32 && gunType != 4)
                {
                    fireRate = 0;
                }
            }
            if (gunType != 1)
            {
                powerDownCount -= 1;
                if (powerDownCount <= 0)
                {
                    gunType = 1;
                    powerDownCount = 128 * 10;
                }
            }
        }

        public void Input(List<projectile> projectiles, List<particle> particles)
        {
            Random random = new Random();
            KeyboardState prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            GamePadState prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            if (keyboard.IsKeyDown(Keys.F1) && prevKeyboard.IsKeyUp(Keys.F1))
            {
                hp -= 1;
            }
            if (inputActive)
            {
                if (keyboard.IsKeyDown(Keys.Space) || gamepad.Buttons.A == ButtonState.Pressed)
                {
                    if (gunType == 5 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(4, new Vector2(pos.X + 5, pos.Y + 12), random.Next(-125,-45)));
                        fireRate = 25;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || gamepad.Buttons.A == ButtonState.Pressed && prevGamepad.Buttons.A == ButtonState.Released)
                {
                    if (gunType == 1 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        fireRate = 1;
                    }
                    if (gunType == 2 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -80));
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -100));
                        fireRate = 1;
                    }
                    if (gunType == 3 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        fireRate = 1;
                    }
                    if (gunType == 4 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(2, new Vector2(pos.X + 8, pos.Y + 6), -90));
                        fireRate = 1;
                    }
                    if (gunType == 6 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(3, new Vector2(pos.X + 8, pos.Y + 6), -90));
                        fireRate = 1;
                    }
                    if (gunType == 7 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(5, new Vector2(pos.X + 8, pos.Y + 6), -90));
                        fireRate = 1;
                    }
                }
                if (gunType == 3 && fireRate == 7)
                {
                    projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                }
                if (gunType == 3 && fireRate == 14)
                {
                    projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                }
                if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A) || gamepad.ThumbSticks.Left.X <= -0.5f)
                {
                    if(pos.X >= 0)
                    {
                        pos.X -= 3;
                    }
                    imx = Frame(1);
                }
                if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D) || gamepad.ThumbSticks.Left.X >= 0.5f)
                {
                    if (pos.X <= 640 - 26)
                    {
                        pos.X += 3;
                    }
                    imx = Frame(2);
                }
                if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.A) && keyboard.IsKeyUp(Keys.D) && gamepad.ThumbSticks.Left.X == 0)
                {
                    imx = 1;
                }
                if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W) || gamepad.ThumbSticks.Left.Y >= 0.5f)
                {
                    if (pos.Y >= 0)
                    {
                        pos.Y -= 3;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S) || gamepad.ThumbSticks.Left.Y <= -0.5f)
                {
                    if (pos.Y <= 480 - 26)
                    {
                        pos.Y += 3;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.A) && keyboard.IsKeyDown(Keys.D))
                {
                    imx = 1;
                }
            }
        }
    }
}
