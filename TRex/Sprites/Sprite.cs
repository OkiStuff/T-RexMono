using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRex.Models;

namespace TRex.Sprites
{
    public class Sprite
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public Vector2 Velocity;
        public float Speed;
        public float Scale = 2f;
        public bool IsRemoved = false;
        

        public Color Color = Color.White;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Game1.TextColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }
    }
}