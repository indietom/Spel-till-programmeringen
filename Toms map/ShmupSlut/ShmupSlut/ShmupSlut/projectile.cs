using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ShmupSlut
{
    class projectile:objects
    {
        public int type;
        public float accel;

        public projectile(int type2, Vector2 pos2, float ang2)
        {
            pos = pos2;
            type = type2;
            angle = ang2;
            switch (type)
            {
                case 1:
                    speed = 7;
                    SetSize(6, 11);
                    SetSpriteCoords(Frame(5), 1);
                    dm = 1;
                    break;
                case 2:
                    accel = -1;
                    SetSize(5, 13);
                    SetSpriteCoords(106, 26);
                    dm = 3;
                    break;
            }
        }
        public void Movment()
        {
            if (type == 1)
            {
                AngleMath();
                pos.X += veclocity_x;
                pos.Y += veclocity_y;
            }
            if(type == 2)
            {
                AngleMath();
                accel += 0.1f;
                speed = accel;
                pos.X += veclocity_x;
                pos.Y += veclocity_y;
            }
            CheckOnScreen();
        }
    }
}









