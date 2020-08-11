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
        public const float Gravity = .04f;
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

            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // Keep Sprite On Screen
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - Rectangle.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.ScreenHeight * .5f);
        }

        private void Move()
        {
            /*
            if (Input.IsKeyPressed(Keys.Space))
            {
                Velocity.Y = -1f;
                HasJumped = true;
            }

            if (HasJumped)
            {
                
                if (Position.Y >= Game1.ScreenHeight * .5f) { }
                
                // gravity crap
                else {Velocity.Y += .01f;}
                
            }
            */

            if (Position.Y >= Game1.ScreenHeight * .5f)
                HasJumped = false;
            
            if (Input.IsKeyPressed(Keys.Space) && !HasJumped)
            {
                Velocity.Y = -1f;
                HasJumped = true;
                Sounds.PlaySound(SoundTypes.Jump);
            }
            
            if (Input.IsKeyReleased(Keys.Space) && Velocity.Y <= 0)
            {
                Velocity.Y *= .5f;
            }

            Velocity.Y += Gravity;
        }
        
    }
}