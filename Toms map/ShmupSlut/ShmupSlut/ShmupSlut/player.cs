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

        public int gunType;
        public int powerDownCount;

        public int fireRate;
        public int score;
        public int lifes;
        public int respawnDelay;
        public int addHealthDelay;
        public int bleedDelay;

        KeyboardState keyboard;
        GamePadState gamepad;

        public player()
        {
            SetSpriteCoords(1, 1);
            SetSize(24, 24);
            pos = new Vector2(320, 240);
            gunType = 1;
            inputActive = true;
            hp = 10;
            score = 0;
            powerDownCount = 128 * 3;
        }

        public void CheckHealth(healthbar healthbar, List<particle> particles)
        {
            Random random = new Random();
            if (hp <= 3)
            {
                bleedDelay += 1;
                if (bleedDelay >= 16)
                {
                    particles.Add(new particle(new Vector2(pos.X + random.Next(20), pos.Y + random.Next(20)), 1, "grey", random.Next(3, 5), 0.01f, random.Next(-300, -210), 4));
                    bleedDelay = 0;
                }
            }
            healthbar.height = hp * 10;
            if (hp <= 0)
            {
                imx = 1;
                if (pos.X > 640)
                {
                    lifes -= 1;
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
            if (respawnDelay >= 1)
            {
                pos.X = 320;
                respawnDelay += 1;
                if (respawnDelay >= 32)
                {
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
                }
                if (respawnDelay >= 32 + 320)
                {
                    inputActive = true;
                    respawnDelay = 0;
                }
            }
        }

        public void Update()
        {
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
            if(gunType != 1)
            {
                powerDownCount -= 1;
                if (powerDownCount <= 0)
                {
                    gunType = 1;
                    powerDownCount = 128 * 3;
                }
            }
        }

        public void Input(List<projectile> projectiles)
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
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -50));
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -140));
                        fireRate = 1;
                    }
                    if (gunType == 3)
                    {
                        projectiles.Add(new projectile(1, new Vector2(pos.X + 8, pos.Y + 12), -90));
                    }
                    if (gunType == 4 && fireRate <= 0)
                    {
                        projectiles.Add(new projectile(2, new Vector2(pos.X + 8, pos.Y + 12), -90));
                        fireRate = 1;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A) || gamepad.ThumbSticks.Left.X == -1)
                {
                    if(pos.X >= 0)
                    {
                        pos.X -= 3;
                    }
                    imx = Frame(1);
                }
                if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D) || gamepad.ThumbSticks.Left.X == 1)
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
                if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W) || gamepad.ThumbSticks.Left.Y == 1)
                {
                    if (pos.Y >= 0)
                    {
                        pos.Y -= 3;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S) || gamepad.ThumbSticks.Left.Y == -1)
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
