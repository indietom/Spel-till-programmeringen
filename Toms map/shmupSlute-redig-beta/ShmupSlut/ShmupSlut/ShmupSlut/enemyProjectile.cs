using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class enemyProjectile:objects
    {
        public int type;

        public Rectangle enemyProjectileC;

        public enemyProjectile(Vector2 pos2, int type2, float spe, float ang)
        {
            pos = pos2;
            type = type2;
            angle = ang;
            speed = spe;
            switch (type)
            { 
                case 1:
                    SetSpriteCoords(101, Frame(1));
                    SetSize(5, 5);
                    dm = 1;
                    break;
                case 2:
                    dm = 2;
                    SetSize(5, 7);
                    SetSpriteCoords(101, 31);
                    break;
                case 3:
                    speed = 3;
                    dm = 3;
                    SetSize(13, 5);
                    SetSpriteCoords(112, Frame(1));
                    break;
                case 5:
                    dm = 1;
                    SetSize(3, 7);
                    SetSpriteCoords(102, 38);
                    break;
                case 6:
                    dm = 4;
                    SetSize(20, 5);
                    if(angle == 0)
                    {
                        SetSpriteCoords(127, 42);
                    }
                    if (angle == -180)
                    {
                        SetSpriteCoords(127, 37);
                    }
                    break;
            }
        }
        public enemyProjectile(Vector2 pos2, int type2, float spe, player player)
        {
            Random random = new Random();
            pos = pos2;
            type = type2;
            speed = spe;
            switch (type)
            {
                case 1:
                    SetSpriteCoords(101, Frame(1));
                    SetSize(5, 5);
                    dm = 1;
                    break;
                case 2:
                    dm = 2;
                    SetSize(5, 7);
                    SetSpriteCoords(101, 31);
                    break;
                case 3:
                    speed = 3;
                    dm = 3;
                    SetSize(13, 5);
                    SetSpriteCoords(112, Frame(1));
                    break;
                case 4:
                    SetSpriteCoords(101, Frame(1));
                    SetSize(5, 5);
                    dm = 1;
                    MathAim(player.pos.X + 12 + random.Next(-24, 24), player.pos.Y + 12 + random.Next(-24, 24));
                    break;
            }
        }
        public void Update(player player, List<projectile> projectiles, List<gib> gibs)
        {
            Random random = new Random();
            if (type == 3)
            {
                enemyProjectileC = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);
            }
            else
            {
                enemyProjectileC = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            }
            if (player.playerC.Intersects(enemyProjectileC))
            {
                if (!player.respawnCutScene)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        gibs.Add(new gib(new Vector2(player.pos.X + 12, player.pos.Y + 12), random.Next(1, 7), random.Next(360), "organic", random.Next(10, 21)));
                    }
                    player.hp -= dm;
                }
                destroy = true;
            }
            foreach (projectile p in projectiles)
            {
                if (type == 3)
                {
                    if (enemyProjectileC.Intersects(p.projectileC))
                    {
                        p.destroy = true;
                        destroy = true;
                    }
                }
            }
        }
        public void Movment(player player)
        {
            switch (type)
            { 
                case 1:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 2:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 3:
                    MathAim(player.pos.X+12, player.pos.Y+12);
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 4:
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 5:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
                case 6:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
            }
            CheckOnScreen();
        }
    }
}
