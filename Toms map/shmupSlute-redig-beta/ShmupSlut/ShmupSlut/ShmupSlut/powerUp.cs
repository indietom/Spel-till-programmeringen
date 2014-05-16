using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class powerUp:objects
    {
        public int type;

        public Rectangle powerUpC;

        public powerUp(Vector2 pos2, int type2)
        {
            type = type2;
            pos = pos2;
            SetSize(24, 24);
            if (type != 7 && type != 8)
            {
                SetSpriteCoords(Frame(type - 1), Frame(20));
            }
            else
            {
                if (type == 7)
                {
                    SetSpriteCoords(1, Frame(21));    
                }
                if (type == 8)
                {
                    SetSpriteCoords(Frame(1), Frame(21));
                }
            }
        }

        public void Update(ref player player)
        {
            powerUpC = new Rectangle((int)pos.X, (int)pos.Y, 24, 24);
            if (player.playerC.Intersects(powerUpC))
            {
                if (player.gunType != 1)
                {
                    player.powerDownCount = 128 * 10;
                }
                switch (type)
                { 
                    case 1:
                        player.gunType = 2;
                        break;
                    case 2:
                        player.gunType = 4;
                        break;
                    case 3:
                        player.gunType = 3;
                        break;
                    case 4:
                        player.gunType = 5;
                        break;
                    case 5:
                        player.gunType = 6;
                        break;
                    case 6:
                        player.gunType = 7;
                        break;
                    case 7:
                        if (player.hp < 10)
                        {
                            player.hp += 3;
                        }
                        else
                        {
                            player.score += 1000;
                        }
                        break;
                    case 8:
                        player.lives += 1;
                        break;
                }
                destroy = true;
            }
        }
        public void Movment()
        {
            pos.Y += 2;
        }
    }
}
