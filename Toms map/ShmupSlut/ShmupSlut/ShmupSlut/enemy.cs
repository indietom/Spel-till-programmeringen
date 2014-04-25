using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class enemy:objects
    {
        public int aiType;
        public int graphicsType;
        public int fireRate;
        public int shootingPos;

        public string material;

        public bool vulnerable;
        public bool rotated;

        public enemy(Vector2 pos2, int aiType2, int graphicsType2, float spe)
        {
            Random random = new Random();
            speed = spe;
            pos = pos2;
            aiType = aiType2;
            graphicsType = graphicsType2;
            switch (graphicsType)
            {
                case 1:
                    currentFrame = 1;
                    SetSpriteCoords(Frame(currentFrame), Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 2:
                    SetSpriteCoords(1, Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 3:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 4:
                    SetSpriteCoords(Frame(3), Frame(11));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 5:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 4;
                    material = "metal";
                    break;
                case 6:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 7;
                    material = "metal";
                    break;
            }
            switch (aiType)
            { 
                case 1:
                    vulnerable = true;
                    hp = 1;
                    break;
                case 2:
                    vulnerable = true;
                    hp = 2;
                    break;
                case 3:
                    rotated = true;
                    break;
                case 4:
                    shootingPos = random.Next(50, 300);
                    vulnerable = true;
                    hp = 5;
                    break;
            }
        }
        public enemy(Vector2 pos2, int aiType2, int graphicsType2, float spe, float ang)
        {
            Random random = new Random();
            speed = spe;
            pos = pos2;
            aiType = aiType2;
            graphicsType = graphicsType2;
            angle = ang;
            switch (graphicsType)
            {
                case 1:
                    currentFrame = 1;
                    SetSpriteCoords(Frame(currentFrame), Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 2:
                    SetSpriteCoords(1, Frame(10));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 3:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    material = "metal";
                    break;
                case 4:
                    SetSpriteCoords(Frame(3), Frame(11));
                    SetSize(24, 24);
                    material = "organic";
                    break;
                case 5:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 4;
                    material = "metal";
                    break;
                case 6:
                    SetSpriteCoords(currentFrame, Frame(11));
                    SetSize(24, 24);
                    currentFrame = 7;
                    material = "metal";
                    break;
            }
            switch (aiType)
            {
                case 1:
                    vulnerable = true;
                    hp = 1;
                    break;
                case 2:
                    vulnerable = true;
                    hp = 2;
                    break;
                case 3:
                    rotated = true;
                    break;
                case 4:
                    shootingPos = random.Next(50, 300);
                    vulnerable = true;
                    hp = 5;
                    break;
            }
        }
        public void Movment(player player)
        {
            switch (aiType)
            { 
                case 1:
                    pos.Y += speed;
                    break;
                case 2:
                    pos.Y += speed;
                    break;
                case 3:
                    MathAim(player.pos.X, player.pos.Y);
                    break;
                case 4:
                    if (pos.Y <= shootingPos)
                    {
                        pos.Y += 2;
                    }
                    if (direction == 3)
                    {
                        pos.X -= 1;
                        if (pos.X <= 48)
                        {
                            pos.X += 5;
                            direction = 4;
                        }
                    }
                    else
                    {
                        pos.X += 1;
                        if (pos.X >= 640-24-48)
                        {
                            pos.X -= 5;
                            direction = 3;
                        }
                    }
                    break;
            }
        }
        public void CheckHealth(List<projectile> projectiles, List<explosion> explosions, ref player player)
        {
            Rectangle enemyC = new Rectangle((int)pos.X+6, (int)pos.Y+6, width-6, 24-6);
            if (pos.Y > 600)
            {
                destroy = true;
            }
            foreach (projectile p in projectiles)
            {
                Rectangle projectileC = new Rectangle((int)p.pos.X, (int)p.pos.Y, width, height);
                if (projectileC.Intersects(enemyC) && vulnerable)
                {
                    hp -= p.dm;
                    p.destroy = true;
                }
            }
            if (hp <= 0 && vulnerable)
            {
                explosions.Add(new explosion(pos));
                switch (aiType)
                { 
                    case 1:
                        player.score += 1000;
                        break;
                    case 2:
                        player.score += 3000;
                        break;
                    case 3:
                        player.score += 4000;
                        break;
                }
                destroy = true;
            }
        }
        public void Attack(List<enemyProjectile> enemyProjectiles)
        {
            Random random = new Random();
            switch (aiType)
            { 
                case 2:
                    fireRate += 1;
                    if (fireRate == 64 + 16 || fireRate == 64 + 32 || fireRate == 64 + 48)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X+width/2-2, pos.Y+height/2-2), 1, 8, -270));
                    }
                    if (fireRate >= 128 + 64)
                    {
                        fireRate = random.Next(-64, 0);
                    }
                    break;
                case 4:
                    fireRate += 1;
                    if (fireRate >= 64)
                    {
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + width / 2 + 3, pos.Y + height / 2 - 2), 1, 5, -270));
                        enemyProjectiles.Add(new enemyProjectile(new Vector2(pos.X + width / 2 - 8, pos.Y + height / 2 - 2), 1, 5, -270));
                        fireRate = 0;
                    }
                    break;
            }
        }
        public void Animation()
        {
            switch (graphicsType)
            {
                case 1:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if(animationCount >= 10)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 5)
                    {
                        currentFrame = 1;
                    }
                    break;
                case 3:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if(animationCount >= 5)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 3)
                    {
                        currentFrame = 0;
                    }
                    break;
                case 5:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 5)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 7)
                    {
                        currentFrame = 4;
                    }
                    break;
                case 6:
                    imx = Frame(currentFrame);
                    animationCount += 1;
                    if (animationCount >= 3)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if (currentFrame == 10)
                    {
                        currentFrame = 7;
                    }
                    break;
            }
        }
    }
}
