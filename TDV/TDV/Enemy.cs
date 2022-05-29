using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDV
{
    public class Enemy
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 Position
        {
            get { return position; }
        }

        public Enemy(Texture2D newTexture, Vector2 newPosition, Vector2 newVelocity)
        {
            texture = newTexture;
            position = newPosition;
            velocity = newVelocity;
        }

        public void Update()
        {
            position += velocity;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, (float)3.1415926536, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
        }
        //public Rectangle Rectangle
        //{
        //    get
        //    {
        //        return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        //    }
        //}
        #region colisions
        //public bool IsTouchingLeft(Enemy enemy)
        //{
        //    return this.Rectangle.Right + this.velocity.X > enemy.Rectangle.Left &&
        //        this.Rectangle.Left > enemy.Rectangle.Left &&
        //        this.Rectangle.Bottom > enemy.Rectangle.Top &&
        //        this.Rectangle.Top < enemy.Rectangle.Bottom;
        //}

        //public bool IsTouchingRight(Enemy enemy)
        //{
        //    return this.Rectangle.Left + this.velocity.X < enemy.Rectangle.Right &&
        //        this.Rectangle.Right > enemy.Rectangle.Right &&
        //        this.Rectangle.Bottom > enemy.Rectangle.Top &&
        //        this.Rectangle.Top < enemy.Rectangle.Bottom;
        //}

        //public bool IsTouchingTop(Enemy enemy)
        //{
        //    return this.Rectangle.Bottom + this.velocity.Y > enemy.Rectangle.Top &&
        //        this.Rectangle.Top < enemy.Rectangle.Top &&
        //        this.Rectangle.Right > enemy.Rectangle.Left &&
        //        this.Rectangle.Left < enemy.Rectangle.Right;
        //}

        //public bool IsTouchingBottom(Enemy enemy)
        //{
        //    return this.Rectangle.Top + this.velocity.Y < enemy.Rectangle.Bottom &&
        //        this.Rectangle.Bottom > enemy.Rectangle.Bottom &&
        //        this.Rectangle.Right > enemy.Rectangle.Left &&
        //        this.Rectangle.Left < enemy.Rectangle.Right;
        //}

        #endregion

    }
}

