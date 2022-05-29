using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDV
{
    class EnemyGenerator
    {
        public Texture2D texture;
        float density;
        Rectangle spawnzone;
        int e = 0;

        public List<Enemy> enemies = new List<Enemy>();

        float timer;

        Random rand1, rand2;

        public EnemyGenerator(Texture2D newtexture, Rectangle spawn, float newDensity)
        {
            texture = newtexture;
            density = newDensity;
            spawnzone = spawn;

            rand1 = new Random();
            rand2 = new Random();
        }

        public void createEnemy()
        {
            enemies.Add(new Enemy(texture, new Vector2((40 + spawnzone.X) + (float)rand1.NextDouble() * spawnzone.Width, spawnzone.Height + spawnzone.Y), /*new Vector2(0, rand2.Next(4, 4))*/ new Vector2(0,2)));
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (timer > 0)
            {
                timer -= 0.2f / density;
                createEnemy();
                e++;
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update();

                if (enemies[i].Position.Y > graphics.Viewport.Height + texture.Height)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

    }
}
