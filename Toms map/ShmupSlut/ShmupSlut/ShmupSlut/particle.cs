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
    class particle:objects
    {
        public int type;
        public float accel;
        public float deccre;

        public particle(Vector2 pos2, int type2, string color, float accel2, float deccre2, float ang, int size)
        {
            type = type2;
            pos = pos2;
            angle = ang;
            accel = accel2;
            deccre = deccre2;
            SetSize(size, size);
            switch (color)
            {
                case "red":
                    SetSpriteCoords(Frame(12), 1);
                    break;
                case "grey":
                    SetSpriteCoords(Frame(12), 6);
                    break;
            }
        }
        public void DrawParticle(Color color, SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, pos, new Rectangle(Frame(12), 1, width, height), color);
        }
        public void Movemnt()
        {
            switch (type)
            {
                case 1:
                    AngleMath();
                    speed = accel;
                    accel -= deccre;
                    pos.X += veclocity_x;
                    pos.Y += veclocity_y; 
                    if (accel <= 0)
                    {
                        destroy = true;
                    }
                    break;
            }
        }
    }
}
