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
                    break;
            }
        }
        public void Movment()
        {
            switch (type)
            { 
                case 1:
                    AngleMath();
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y;
                    break;
            }
            CheckOnScreen();
        }
    }
}
