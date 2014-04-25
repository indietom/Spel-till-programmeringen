using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShmupSlut
{
    class spawnManager:objects
    {
        public int spawnBackgroundCount;
        public int spawnBackgroundCount2;
        public int spawnForegroundCount;
        public int spawnEnemyConut1;
        public int spawnEnemyConut2;
        public int spawnEnemyCount3;
        public int difficulty;
        public int waveSize;

        public float difficultyCount;

        public Vector2 linePos;

        public spawnManager(List<backgroundObject> backgroundObjects, List<enemy> enemies,  int level)
        {
            Random random = new Random();
            difficulty = 1;
            if (level == 1)
            {
                spawnBackgroundCount = 480/3;
                spawnBackgroundCount2 = 480;
                for (int i = 0; i < 100; i++)
                {
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(-480, 480)), 476, 151, 4, 4, 3, "back"));
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), random.Next(-480, 480)), 476, 155, 4, 4, 1, "back"));
                }
                for (int i = 0; i < 100; i++)
                {
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(-480, 480)), 476, 151, 4, 4, 3, "back"));
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), random.Next(-480, 480)), 476, 151, 4, 4, 3, "back"));
                }
            }
        }
        public void Spawn(int level, List<backgroundObject> backgroundObjects, List<enemy> enemies)
        {
            Random random = new Random();
            if (level == 1)
            {
                difficultyCount += 0.01f;
                if (difficultyCount >= 10 * difficulty)
                {
                    difficulty += 1;
                    difficultyCount = 0;
                }
                spawnEnemyConut1 += 1;
                if (spawnEnemyConut1 >= 128*10)
                {
                    linePos.X = random.Next(640 - 24);
                    linePos.Y = random.Next(-200, -100);
                    for (int i = 0; i < 3 + difficulty; i++)
                    {
                        enemies.Add(new enemy(new Vector2(linePos.X,  linePos.Y - i * 32), 1, 1, 2)); 
                    }
                    spawnEnemyConut1 = random.Next(128*2);
                }
                spawnEnemyConut2 += 1;
                if (spawnEnemyConut2 >= 128 * 2 - difficulty*2)
                {
                    waveSize = random.Next(1, 2);
                    for (int i = 0; i < waveSize+difficulty; i++)
                    {
                        enemies.Add(new enemy(new Vector2(random.Next(640-24), random.Next(-700, -100)), 2, random.Next(3,6), random.Next(1, 5)));
                    }
                    spawnEnemyConut2 = random.Next(32);
                }
                if (spawnBackgroundCount2 == 128)
                {
                    waveSize = random.Next(1, 2);
                    for (int i = 0; i < waveSize + difficulty; i++)
                    {
                        enemies.Add(new enemy(new Vector2(random.Next(640 - 24), random.Next(-700, -100)), 1, random.Next(3, 6), random.Next(1, 5)));
                    }
                }
                spawnEnemyCount3 += 1;
                if (spawnEnemyCount3 >= 128 * 15)
                {
                    enemies.Add(new enemy(new Vector2(random.Next(48, 640 - 24 - 48), random.Next(-300, -100)), 4, 6, 2)); 
                    spawnEnemyCount3 = 0;
                }
                spawnForegroundCount += 1;
                if (spawnForegroundCount >= 800)
                {
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(480 - 4)), Frame(19), 1, 49, 49, 2, "fore"));
                    spawnForegroundCount = random.Next(200);
                }

                spawnBackgroundCount += 1;
                spawnBackgroundCount2 += 1;
                if (spawnBackgroundCount2 >= 480)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(480 - 4)), 476, 155, 4, 4, 1, "back"));
                    }
                    spawnBackgroundCount2 = 0;
                }
                if (spawnBackgroundCount >= 480/3)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(480 - 4)), 476, 151, 4, 4, 3, "back"));
                    }
                    spawnBackgroundCount = 0;
                }
            }
            if (level == 2)
            {
                spawnBackgroundCount += 1;
                if (spawnBackgroundCount == 240)
                {
                    for (int x = 0; x < 24 * 7; x += 1)
                    {
                        for (int y = 0; y < 24 * 5; y += 1)
                        {
                            backgroundObjects.Add(new backgroundObject(new Vector2(x * 96, -480 - y * 72), 576, 126, 96, 72, 2, "back"));
                        }
                    }
                    spawnBackgroundCount = 0;
                }
            }
        }
    }
}
