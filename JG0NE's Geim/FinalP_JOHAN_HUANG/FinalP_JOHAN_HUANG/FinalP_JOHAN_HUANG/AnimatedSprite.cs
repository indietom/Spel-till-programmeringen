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
using System.Diagnostics;

namespace FinalP_JOHAN_HUANG //your namespace goes here
{
    class AnimatedSprite
    {
        // Private members
        private int columns;
        private int rows;
        private int currentColumn;
        private int currentRow;
        private int frameWidth;
        private int frameHeight;
        private int animationTimer;
        private bool continuous;
        private bool complete;

        // Public properties
        public Texture2D Texture { get; set; }
        public int AnimationDelay { get; set; }
        public bool Complete
        {
            get { return this.complete; } // This is made into a public property so that an object using the animated sprite can check if
            // the animation is done and then react to that by changing its texture, removing itself etc.                                          
        }

        // Constructor
        public AnimatedSprite(Texture2D texture, int numberOfColumns, int numberOfRows, int animationDelay, bool continuous)
        {
            this.Texture = texture;
            this.columns = numberOfColumns;
            this.rows = numberOfRows;
            this.AnimationDelay = animationDelay;
            this.continuous = continuous;
            this.currentColumn = 0;
            this.currentRow = 0;
            this.frameWidth = texture.Width / columns; // One frame is the entire width of the texture divided by the number of columns
            this.frameHeight = texture.Height / rows; // One frame is the entire height of the texture divided by the number of rows
            this.animationTimer = 0;
            this.complete = false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 destination, SpriteEffects spriteEffect)
        {
            // If the animation is not complete (only for non-continous animations) then draw it
            if (this.complete == false)
            {
                // Get a source rectangle representing the current frame in the texture
                Rectangle frame = new Rectangle(currentColumn * frameWidth, currentRow * frameHeight, frameWidth, frameHeight);

                // Draw the texture using only the part defined by the frame rectangle
                spriteBatch.Draw(this.Texture, destination, frame, Color.White, 0, Vector2.Zero, 1,  spriteEffect, 1);
            }

            // Updating frame logic

            if (this.animationTimer == 0) // If the animation timer is back on zero (has gone one whole turn), then change the frame
            {
                if (this.currentColumn < this.columns - 1) // If the current frame is not the in the last column...
                {
                    this.currentColumn++; // ...move forward one column
                }
                else // If it is the last column...
                {
                    if (this.currentRow < this.rows - 1)  // If the current frame is not the in the last row...
                    {
                        this.currentRow++;
                        this.currentColumn = 0;
                    }
                    else if (this.continuous) // ...check if the animated sprite is a continuous animation and if so...
                    {
                        this.currentRow = 0;
                        this.currentColumn = 0;
                    }
                    else // If it is not a continuous animation...
                    {
                        this.complete = true; // ...then set complete to true, indicating that the animation will not be drawn anymore
                    }
                }
                // In any case, if the animation timer was at zero, add the animation delay to it, thus making it count down until the next update
                this.animationTimer = this.AnimationDelay;
            }
            else // If the animation timer is not yet at zero, then no frame changing should be made and instead...
            {
                this.animationTimer--; // ...we just lower the timer by one
            }
        }
    }
}
