using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRex.Sprites
{
    public class Cloud
    {
        protected Texture2D _texture;

        public Vector2 Position = new Vector2(100, 100);
        public float Scale = 5f;
        public Color Color = Color.White;

        // Black: 000, White, 111
        
        public Cloud(Texture2D texture)
        {
            _texture = texture;
            Position.Y = Game1.Random.Next(-100,360);
            Scale = ((float)Game1.Random.NextDouble() * 4) + .5f;
            Position.X = 1280;
        }
        public void Update(GameTime gameTime, List<Cloud> clouds)
        {
            Position.X -= .5f;

            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.ScreenHeight * .5f);

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}