using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class gib:objects
    {
        public int type;
        public float accel;
        public float deccre;

        public gib(Vector2 pos2, float spe, float deccre2, float ang)
        {
            pos = pos2;
            accel = spe;
            
        }
    }
}
