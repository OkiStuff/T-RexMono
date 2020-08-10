using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


/*
 TODO: Y RNG, Death 
 */

namespace TRex.Sprites
{
    public class Cactus : Sprite
    {
        public Cactus(Texture2D texture)
            : base(texture)
        {
            Position = new Vector2(1280, Game1.ScreenHeight * .5f);
        }
        

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (Position.X > -Game1.ScreenWidth)
            Position.X -= Game1.CurSpeed;
        }
    }
}