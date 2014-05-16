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
        public int spawnEnemyCount4;
        public int side;
        public int difficulty;
        public int waveSize;
        public int spawnPowerUpCount;

        public float difficultyCount;

        public Vector2 linePos;
        public Vector2 trianglePos;

        public spawnManager(List<backgroundObject> backgroundObjects, List<enemy> enemies,  int level)
        {
            Random random = new Random();
            difficulty = 5;
            if (level == 1)
            {
                spawnBackgroundCount = 480/2;
                spawnBackgroundCount2 = 480;
                for (int i = 0; i < 100; i++)
                {
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(-480, 480)), 476, 151, 4, 4, 2, "back"));
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), random.Next(-480, 480)), 476, 155, 4, 4, 1, "back"));
                }
                for (int i = 0; i < 100; i++)
                {
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(-480, 480)), 476, 151, 4, 4, 2, "back"));
                    backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), random.Next(-480, 480)), 476, 151, 4, 4, 2, "back"));
                }
            }
            if (level == 2)
            {
            }
        }
        public void Spawn(int level, List<backgroundObject> backgroundObjects, List<enemy> enemies, List<powerUp> powerUps, List<boss> bosses)
        {
            Random random = new Random();
            if (level == 1)
            {
                spawnPowerUpCount += 1;
                if (spawnPowerUpCount >= 128*12)
                {
                    powerUps.Add(new powerUp(new Vector2(random.Next(640-24), random.Next(-200, -100)), random.Next(1,9 )));
                    spawnPowerUpCount = random.Next(128*4);
                }
                difficultyCount += 0.01f;
                if (difficultyCount >= 10 * difficulty)
                {
                    difficulty += 1;
                    difficultyCount = 0;
                }
                if (difficulty == 3 && difficultyCount == 0.01f + 0.01f)
                {
                    bosses.Add(new boss(new Vector2(640-200, -248), 1));
                }
                if (bosses.Count >= 1)
                {
                    spawnEnemyConut1 = 0;
                    spawnEnemyConut2 = 0;
                    spawnEnemyCount3 = 0;
                    spawnEnemyCount4 = 0;
                }
                spawnEnemyCount4 += 1;
                if (spawnEnemyCount4 >= 128 * 3)
                {
                    side = random.Next(3,5);
                    if (side == 3)
                    {
                        enemies.Add(new enemy(new Vector2(640 - 34, random.Next(-100, -50)), 5, 7, 1));
                    }
                    else
                    {
                        enemies.Add(new enemy(new Vector2(10, random.Next(-100, -50)), 5, 8, 1));
                    }
                    spawnEnemyCount4 = 0;
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
                if (spawnBackgroundCount >= 480/2)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        backgroundObjects.Add(new backgroundObject(new Vector2(random.Next(640 - 4), -480 - random.Next(480 - 4)), 476, 151, 4, 4, 2, "back"));
                    }
                    spawnBackgroundCount = 0;
                }
            }
            if (level == 2)
            {
                difficultyCount += 0.01f;
                if (difficultyCount >= 10 * difficulty)
                {
                    difficulty += 1;
                    difficultyCount = 0;
                }
                if (difficulty == 5 && difficultyCount == 0.01f + 0.01f)
                {
                    bosses.Add(new boss(new Vector2(640 - 200, -248), 2));
                }
                if (bosses.Count >= 1)
                {
                    spawnEnemyConut1 = 0;
                    spawnEnemyConut2 = 0;
                    spawnEnemyCount3 = 0;
                    spawnEnemyCount4 = 0;
                }
                spawnPowerUpCount += 1;
                if (spawnPowerUpCount >= 128 * 15)
                {
                    powerUps.Add(new powerUp(new Vector2(random.Next(640 - 24), random.Next(-200, -100)), random.Next(1, 9)));
                    spawnPowerUpCount = 0;
                }
                spawnEnemyConut1 += 1;
                if (spawnEnemyConut1 >= 128*5)
                {
                    trianglePos = new Vector2(random.Next(0, 640 - 24 * 5), random.Next(-300, -200));
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 0 || i == 4)
                        {
                            enemies.Add(new enemy(new Vector2(trianglePos.X + 24 * i, trianglePos.Y), 2, 11, 1));
                        }
                        else
                        {
                            enemies.Add(new enemy(new Vector2(trianglePos.X + 24 * i, trianglePos.Y), 1, 11, 1));
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        enemies.Add(new enemy(new Vector2(trianglePos.X + 24 + 24 * i, trianglePos.Y + 24), 1, 11, 1));
                    }
                    enemies.Add(new enemy(new Vector2(trianglePos.X + 48, trianglePos.Y + 48), 2, 11, 1));
                    spawnEnemyConut1 = 0;
                }
                spawnEnemyConut2 += 1;
                if (spawnEnemyConut2 >= 128 * 7)
                {
                    waveSize = random.Next(1, 3);
                    for (int i = 0; i < waveSize + difficulty; i++)
                    { 
                        enemies.Add(new enemy(new Vector2(random.Next(640-24), random.Next(-250, -100)), 3, 10, random.Next(1,3)));
                    }
                    spawnEnemyConut2 = 0;
                }
                spawnEnemyCount3 += 1;
                if (spawnEnemyCount3 >= 128*10)
                {
                    enemies.Add(new enemy(new Vector2(random.Next(100, 500), random.Next(-200, -100)), 4, 9, 1));
                    spawnEnemyCount3 = 0;
                }
                spawnEnemyCount4 += 1;
                if (spawnEnemyCount4 >= 128 * 2)
                {
                    waveSize = random.Next(1, 4);
                    for (int i = 0; i < waveSize + difficulty; i++)
                    {
                        enemies.Add(new enemy(new Vector2(random.Next(100, 500), random.Next(-200, -100)), 6, random.Next(12,14), random.Next(2,6)));
                        spawnEnemyCount4 = 0;
                    }
                }

                spawnBackgroundCount += 1;
                if (spawnBackgroundCount >= 72/2)
                {
                    for (int i = 0; i < 24 * 7; i++)
                    {
                        backgroundObjects.Add(new backgroundObject(new Vector2(i * 72, -72), 600, 126, 72, 72, 2, "back"));
                    }
                    for (int i = 0; i < 24 * 7; i += 3)
                    {
                        backgroundObjects.Add(new backgroundObject(new Vector2(i * 24, -72), Frame(random.Next(20, 30)), Frame(random.Next(3, 5)), 24, 24, 2, "back"));
                        backgroundObjects.Add(new backgroundObject(new Vector2(i * 24 + 48, -72), Frame(random.Next(20, 30)), Frame(random.Next(3, 5)), 24, 24, 2, "back"));
                        backgroundObjects.Add(new backgroundObject(new Vector2(i * 24, -72 + 48), Frame(random.Next(20, 30)), Frame(random.Next(3, 5)), 24, 24, 2, "back"));
                        backgroundObjects.Add(new backgroundObject(new Vector2(i * 24 + 48, -72 + 48), Frame(random.Next(20, 30)), Frame(random.Next(3, 5)), 24, 24, 2, "back"));
                    }
                    spawnBackgroundCount = 0;
                }
            }
        }
    }
}
