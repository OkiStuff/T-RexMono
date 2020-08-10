using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRex.Models;


namespace TRex.Sprites
{
    public class Player : Sprite
    {
        public int Score;
        
        public bool HasDied = false;
        //public float Gravity = .04f;
        public bool HasJumped = false;

        public Player(Texture2D texture) : base(texture) {}

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            foreach (var sprite in sprites)
            {
                if (sprite is Player)
                    continue;

                if (sprite.Rectangle.Intersects(this.Rectangle)) this.HasDied = true;
                
                if (Game1.GlobalTimer >= 0.01f)
                {
                    if (Game1.HighScore < Score) Game1.HighScore++;
                    Game1.GlobalTimer = 0f;
                    Score++;
                }
            }

            Position += Velocity;
            // Keep Sprite On Screen
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - Rectangle.Width);
            Velocity = Vector2.Zero;
        }

        private void Move()
        {


            if (Input.IsKeyPressed(Keys.Space))
            {
                Velocity.Y = -_texture.Height * (Scale) + -200;
                HasJumped = true;
            }

            if (HasJumped)
            {
                
                if (Position.Y >= Game1.ScreenHeight * .5f) { }
                
                // gravity crap
                else {Velocity.Y = 5f;}
                
            }

        }
        
    }
}