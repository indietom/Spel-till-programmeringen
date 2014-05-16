using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class boss:objects
    {
        public int nextLevelCount;
        public int switchDirCount;
        public int maxSwitchDirCount;
        public int fireRate;
        public int fireRate2;
        public int type;
        public int bleedCount;

        public bool goUp;

        public Rectangle bossC;

        public boss(Vector2 pos2, int type2)
        {
            pos = pos2;
            type = type2;
            switch (type2)
            { 
                case 1:
                    hp = 16;
                    SetSize(124, 124);
                    SetSpriteCoords(Frame(18), Frame(8));
                    maxSwitchDirCount = 400;
                    direction = 3;
                    speed = 1;
                    break;
                case 2:
                    hp = 20;
                    SetSize(60, 50);
                    SetSpriteCoords(Frame(18), Frame(13));
                    maxSwitchDirCount = 400;
                    direction = 3;
                    speed = 4;
                    goUp = false;
                    break;
            }
        }
        public void checkHealth(List<projectile> projectiles, List<particle> particles, ref player player, ref int level)
        {
            Random random = new Random();
            if (player.playerC.Intersects(bossC))
            {
                player.hp = 0;
            }
            switch (type)
            { 
                case 2:
                    if (hp <= 10)
                    {
                        bleedCount += 1;
                        if (bleedCount >= 16)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                particles.Add(new particle(new Vector2(pos.X + random.Next(width), pos.Y + random.Next(height)), 1, "smoke", random.Next(1, 5), 0.01f, random.Next(-180, -45)));
                                particles.Add(new particle(new Vector2(pos.X + random.Next(width), pos.Y + random.Next(height)), 1, "fire smoke", random.Next(1, 5), 0.01f, random.Next(-180, -45)));
                            }
                            bleedCount = 0;
                        }
                    }
                    break;
            }
            foreach (projectile p in projectiles)
            {
                if (p.projectileC.Intersects(bossC))
                {
                    hp -= p.dm;
                    p.destroy = true;
                }
            }
            if (hp <= 0)
            {
                switch (type)
                { 
                    case 2:
                        imy = 377;
                        break;
                }
                pos.X += 3 + speed;
                pos.Y += 3 + speed;
                nextLevelCount += 1;
            }
            if (nextLevelCount >= 128 * 2)
            {
                level += 1;
                destroy = true;
            }
        }
        public void Update()
        {
            switch(type)
            {
                case 1:
                    bossC = new Rectangle((int)pos.X + 30, (int)pos.Y + 41, 52, 53);
                    break;
                case 2:
                    bossC = new Rectangle((int)pos.X + 13, (int)pos.Y + 17, 20, 27);
                    break;
            }
        }
        public void Attack(List<enemyProjectile> enemyProjectiles, player player)
        {
            if (player.hp <= 0 && player.respawnCutScene)
            {
                fireRate = 0;
            }
            switch (type)
            {
                case 1:
                    fireRate += 1;
                    if (fireRate >= 128 * 2 && hp >= 9 && hp >= 1)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 18, pos.Y + 22), 3, 4, 0));
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 88, pos.Y + 22), 3, 4, 0));
                        fireRate = 0;
                    }
                    if (fireRate >= 128 && hp <= 8 && hp >= 1)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 18, pos.Y + 22), 3, 4, 0));
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 88, pos.Y + 22), 3, 4, 0));
                        for (int i = 0; i < 5; i++)
                        {
                            enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 24 + i * 24, pos.Y + 22), 1, 7, -270));
                        }
                        fireRate = 0;
                    }
                    break;
                case 2:
                    if (hp >= 1)
                    {
                        fireRate += 1;
                        if (fireRate == 32 || fireRate == 40 || fireRate == 48)
                        {
                            if (direction == 3)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 4, pos.Y + 47), 1, 8, -220));
                            }
                            if (direction == 4)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 50, pos.Y + 47), 1, 8, -320));
                            }
                        }
                        if (fireRate >= 48 + 16)
                        {
                            fireRate = 0;
                        }
                        fireRate2 += 1;
                        if (fireRate2 >= 64)
                        {
                            if (direction == 3)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 44, pos.Y + 33), 6, 8, -180));
                            }
                            if (direction == 4)
                            {
                                enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + 16, pos.Y + 33), 6, 8, 0));
                            }
                            fireRate2 = 0;
                        }
                    }
                    break;
            }
        }
        public void Movment(player player)
        { 
            switch (type)
            { 
                case 1:
                    if (hp <= 8)
                    {
                        speed = 3;
                    }
                    if (pos.Y <= 50)
                    {
                        pos.Y += 1;
                    }
                    else
                    {
                        switch (direction)
                        {
                            case 1:
                                pos.Y += speed;
                                break;
                            case 2:
                                pos.Y -= speed;
                                break;
                            case 3:
                                pos.X -= speed;
                                break;
                            case 4:
                                pos.X += speed;
                                break;
                        }

                        if (hp >= 9)
                            switchDirCount += 1;
                        else
                            switchDirCount += 3;

                        if (direction == 3 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 4;
                            switchDirCount = 0;
                        }
                        if (direction == 4 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 3;
                            switchDirCount = 0;
                        }
                    }
                    break;
                case 2:
                    if (hp >= 1)
                    {
                        if (DistanceTo(player.pos.X, player.pos.Y) <= 128)
                        {
                            speed = 10;
                        }
                        else
                        {
                            speed = 4;
                        }
                        if (pos.Y >= 480 - width)
                        {
                            goUp = true;
                        }
                        if (pos.Y <= 0)
                        {
                            goUp = false;
                        }
                        switchDirCount += (int)speed;
                        switch (direction)
                        {
                            case 3:
                                pos.X -= speed;
                                imx = Frame(18);
                                break;
                            case 4:
                                pos.X += speed;
                                imx = 512;
                                break;
                        }
                        if (switchDirCount >= maxSwitchDirCount)
                        {
                            if (goUp)
                            {
                                pos.Y -= height / 2;
                            }
                            else
                            {
                                pos.Y += height / 2;
                            }
                        }
                        if (direction == 3 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 4;
                            switchDirCount = 0;
                        }
                        if (direction == 4 && switchDirCount >= maxSwitchDirCount)
                        {
                            direction = 3;
                            switchDirCount = 0;
                        }
                    }
                    break;
            }
        }
    }
}
