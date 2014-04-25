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
    class gui:objects
    {
        public float dialogCount;
        public float nextDialogCount;
        public const float maxDialogDisplay = 1500;

        public int currentDialog;

        public string[] level1Dialog;

        public gui()
        { 
            level1Dialog = new String[4];
            level1Dialog[0] = "Your mission is to destory earth, it won't be easy. \nAs you can se there are some \nof our ships laying around, destoried.";
            level1Dialog[1] = "You might have noticed that the enemy is using aliens, \nThey enslaved the aliens of mars in \n2069 and are now using them as meat shields";
            level1Dialog[2] = "Good luck, You'll need it!";
            level1Dialog[3] = "";
        }

        public void drawGui(SpriteBatch spriteBatch, SpriteFont fontSmall, Texture2D spritesheet, healthbar healthbar, player player, int level)
        {
            healthbar.DrawSprite(spriteBatch, spritesheet);
            if (player.gunType != 1) { spriteBatch.DrawString(fontSmall, "Power Down In: " + player.powerDownCount, new Vector2(20, 14), Color.Gold); }
            spriteBatch.DrawString(fontSmall, "Score: " + player.score, new Vector2(20, 28), Color.PeachPuff);
            spriteBatch.Draw(spritesheet, new Vector2(20, 28 + 32), new Rectangle(1, 351, 24, 24), Color.White);
            spriteBatch.Draw(spritesheet, new Vector2(22, 28 + 34), new Rectangle(Frame(currentFrame), 351, 20, 20), Color.White);
            if (level == 1 && dialogCount <= maxDialogDisplay)
            {
                spriteBatch.Draw(spritesheet, new Vector2(117, 15), new Rectangle(350, 540, 450, 60), Color.White);
                spriteBatch.DrawString(fontSmall, level1Dialog[currentDialog], new Vector2(120, 20), Color.White);
            }
        }
        public void update(player player, int level)
        {
            switch (level)
            {
                case 1:
                    dialogCount += 1;
                    nextDialogCount += 1;
                    if (nextDialogCount >= maxDialogDisplay/3)
                    {
                        currentDialog += 1;
                        nextDialogCount = 0;
                    }
                    break;
            }
            switch (player.gunType)
            { 
                case 1:
                    currentFrame = 1;
                    break;
                case 2:
                    currentFrame = 2;
                    break;
                case 3:
                    currentFrame = 4;
                    break;
                case 4:
                    currentFrame = 3;
                    break;

            }
        }
    }
}
