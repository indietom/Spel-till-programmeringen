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
        public const float maxDialogDisplay = 1100;

        public int currentDialog;

        public string[] level1Dialog;
        public string[] level2Dialog;
        public string[] level3Dialog;

        public gui()
        { 
            level1Dialog = new String[4];
            level1Dialog[0] = "Your mission is to destory earth, it won't be easy. \nAs you can se there are some \nof our ships laying around, destoried.";
            level1Dialog[1] = "You might have noticed that the enemy is using aliens, \nThey enslaved the aliens of mars in \n2069 and are now using them as meat shields";
            level1Dialog[2] = "Good luck, You'll need it!";
            level1Dialog[3] = "";

            level2Dialog = new String[4];
            level2Dialog[0] = "The humans are going to throw everything \nthey've got at you. We aren't going to be able to \ndeliver as many power ups. Make sure they count!";
            level2Dialog[1] = "The human mothership is said to be stored here \nin this city. Blowing it up would smash 50% of the \nthe humans resitance!";
            level2Dialog[2] = "If you fail there won't be anything for you \nto return to!";
            level2Dialog[3] = "";

            level3Dialog = new String[4];
            level3Dialog[0] = "That wasn't it! \nYou have to keep looking!";
            level3Dialog[1] = "They are tracking our plantes position \nadn are planing a retaliation attack!";
            level3Dialog[2] = "Finish your mission and do it quickly!";
            level3Dialog[3] = "";
        }

        public void drawGui(SpriteBatch spriteBatch, SpriteFont fontSmall, Texture2D spritesheet, healthbar healthbar, player player, int level)
        {
            healthbar.DrawSprite(spriteBatch, spritesheet);
            if (player.lives >= 4)
            {
                spriteBatch.Draw(spritesheet, new Vector2(21, 90), new Rectangle(Frame(1), Frame(16), 12, 12), Color.White);
                spriteBatch.DrawString(fontSmall, "x" + player.lives.ToString(), new Vector2(35, 85), Color.White);
            }
            else
            {
                spriteBatch.Draw(spritesheet, new Vector2(21, 90), new Rectangle(Frame(1), Frame(16), player.lives * 12 + player.lives - 1, 12), Color.White);
            }
            if (player.gunType != 1) { spriteBatch.DrawString(fontSmall, "Power Down In: " + player.powerDownCount, new Vector2(20, 14), Color.Gold); }
            spriteBatch.DrawString(fontSmall, "Score: " + player.score, new Vector2(20, 28), Color.PeachPuff);
            spriteBatch.Draw(spritesheet, new Vector2(20, 28 + 32), new Rectangle(1, 351, 24, 24), Color.White);
            spriteBatch.Draw(spritesheet, new Vector2(22, 28 + 34), new Rectangle(Frame(currentFrame), 351, 20, 20), Color.White);
            if (level == 1 && dialogCount <= maxDialogDisplay)
            {
                spriteBatch.Draw(spritesheet, new Vector2(117, 35), new Rectangle(350, 540, 450, 60), Color.White);
                spriteBatch.DrawString(fontSmall, level1Dialog[currentDialog], new Vector2(120, 33), Color.White);
            }
            if (level == 2 && dialogCount <= maxDialogDisplay)
            {
                spriteBatch.Draw(spritesheet, new Vector2(117, 35), new Rectangle(350, 540, 450, 60), Color.White);
                spriteBatch.DrawString(fontSmall, level2Dialog[currentDialog], new Vector2(120, 33), Color.White);
            }
            if (level == 3 && dialogCount <= maxDialogDisplay)
            {
                spriteBatch.Draw(spritesheet, new Vector2(117, 35), new Rectangle(350, 540, 450, 60), Color.White);
                spriteBatch.DrawString(fontSmall, level3Dialog[currentDialog], new Vector2(120, 33), Color.White);
            }
        }
        public void Update(player player, int level)
        {
            dialogCount += 1;
            nextDialogCount += 1;
            if (nextDialogCount >= maxDialogDisplay / 3)
            {
                currentDialog += 1;
                nextDialogCount = 0;
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
                case 5:
                    currentFrame = 5;
                    break;
                case 6:
                    currentFrame = 6;
                    break;
                case 7:
                    currentFrame = 7;
                    break;

            }
        }
    }
}
