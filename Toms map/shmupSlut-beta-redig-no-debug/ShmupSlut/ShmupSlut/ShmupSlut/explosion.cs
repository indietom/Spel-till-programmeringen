using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class explosion:objects
    {
        public explosion(Vector2 pos2)
        {
            pos = pos2;
            SetSpriteCoords(Frame(currentFrame), Frame(13));
            SetSize(24, 24);
        }
        public void Animation()
        {
            imx = Frame(currentFrame);
            animationCount += 1;
            if (animationCount >= 2)
            {
                currentFrame += 1;
                animationCount = 0;
            }
            if (currentFrame == 8)
            {
                destroy = true;
            }
        }
    }
}
