using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace ShmupSlut
{
    class objects
    {
        public Vector2 pos;

        public int imx;
        public int imy;
        public int width;
        public int height;
        public int hp;
        public int dm;
        public int currentFrame;
        public int direction;

        public int animationCount;

        public bool animationActive;
        public bool destroy;

        public float angle2;
        public float angle;
        public float speed;
        public float scale_x;
        public float scale_y;
        public float veclocity_x;
        public float veclocity_y;

        public float DistanceTo(float x2, float y2)
        {
            return (float)Math.Sqrt((pos.X - x2) * (pos.X - x2) + (pos.Y - y2) * (pos.Y - y2));
        }

        public void AngleMath()
        {
            angle2 = (angle * (float)Math.PI / 180);
            scale_x = (float)Math.Cos(angle2);
            scale_y = (float)Math.Sin(angle2);
            veclocity_x = (speed * scale_x);
            veclocity_y = (speed * scale_y);
        }

        public void MathAim(float x2, float y2)
        {
            angle = (float)Math.Atan2(y2 - pos.Y, x2 - pos.X);
            veclocity_x = (speed * (float)Math.Cos(angle));
            veclocity_y = (speed * (float)Math.Sin(angle));
        }

        public int Frame(int Frame2)
        {
            return Frame2 * 24 + Frame2 + 1;
        }
        public int BigFrame(int Frame2)
        {
            return Frame2 * 49+25 + Frame2 + 1;
        }

        public void CheckOnScreen()
        {
            if (pos.X >= 640 - width || pos.X <= 0 || pos.Y >= 480 - height || pos.Y <= 0)
            {
                destroy = true;
            }
        }
        
        public void SetSpriteCoords(int imx2, int imy2)
        {
            imx = imx2;
            imy = imy2;
        }

        public void SetSize(int w2, int h2)
        {
            width = w2;
            height = h2;
        }

        public void DrawSprite(SpriteBatch spritebatch, Texture2D spritesheet)
        {
            spritebatch.Draw(spritesheet, pos, new Rectangle(imx, imy, width, height), Color.White);
        }
        public void DrawSprite(SpriteBatch spritebatch, Texture2D spritesheet, float size)
        {
            spritebatch.Draw(spritesheet, pos, new Rectangle(imx, imy, width, height), Color.White, angle, new Vector2(width / 2, height / 2), size, SpriteEffects.None, 0);
        }
    }
}
