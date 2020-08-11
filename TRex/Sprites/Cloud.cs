/*
using System;
using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRex.Systems
{
    public static class CloudSystem
    {
        public static Cloud[] Clouds = new Cloud[20];

        public static float CloudRotationTick = 0f;

        public static void Load()
        {
            for (int i = 0; i < Clouds.Length; i++)
            {
                Clouds[i] = NewCloud();
            }
        }

        public static void Update()
        {
            for (int i = 0; i < Clouds.Length; i++)
            {
                if (Globals.GameState == GameStates.Running)
                    Clouds[i].Transform.Position.X -= (Globals.Speed * .1f) + Clouds[i].MoveSpeed;
                else
                    Clouds[i].Transform.Position.X -= Clouds[i].MoveSpeed * .25f;
                
                if (Clouds[i].Transform.Position.X <=
                    (-Clouds[i].Sprite.Texture.Width * Clouds[i].Transform.Scale.X) * 4f)
                {
                    Clouds[i] = NewCloud();
                }

                Clouds[i].Transform.Position.Y += (float)Math.Sin(CloudRotationTick) * Clouds[i].BobAmount;
                CloudRotationTick += Clouds[i].Transform.Scale.X * (float)Globals.Random.NextDouble() * .001f;

                Clouds[i].Transform.Position.Y = Math.Clamp(Clouds[i].Transform.Position.Y, -1000, ((GameSettings.ScreenHeight * .5f) + 140) - (DinosaurSystem.Dinosaur.Sprite.Texture.Height * DinosaurSystem.Dinosaur.Transform.Scale.Y) * .5f);
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < Clouds.Length; i++)
            {
                Functions.DrawSprite(ref Clouds[i].Sprite, ref Clouds[i].Transform);
            }
        }

        public static Cloud NewCloud()
        {
            var cloud = new Cloud();

            cloud.Sprite.Texture = Assets.CloudTexture;
            cloud.Sprite.Centered = true;
            cloud.Sprite.Effects = Functions.Choose(SpriteEffects.None, SpriteEffects.FlipHorizontally);

            cloud.BobAmount = (float)Globals.Random.NextDouble() - .5f;

            cloud.Transform.Scale = Vector2.One * (((float)Globals.Random.NextDouble() * 4) + .5f);

            if (Globals.GameState == GameStates.Running || Globals.GameState == GameStates.GameOver)
            {
                cloud.Transform.Position = new Vector2(
                    Globals.Random.Next(GameSettings.ScreenWidth + cloud.Sprite.Texture.Width * 5, (GameSettings.ScreenWidth * 2) + cloud.Sprite.Texture.Width * 5),
                    Globals.Random.Next(0, (int)(((GameSettings.ScreenHeight * .5f) + 140) - (DinosaurSystem.Dinosaur.Sprite.Texture.Height * DinosaurSystem.Dinosaur.Transform.Scale.Y) * .5f)));
            }
            else if (Globals.GameState == GameStates.BeforeStart)
            {
                cloud.Transform.Position = new Vector2(
                    Globals.Random.Next(0, (GameSettings.ScreenWidth * 2)),
                    Globals.Random.Next(0, (int)(((GameSettings.ScreenHeight * .5f) + 140) - (DinosaurSystem.Dinosaur.Sprite.Texture.Height * DinosaurSystem.Dinosaur.Transform.Scale.Y) * .5f)));
            }

            cloud.Sprite.Colour = new Color(1f / cloud.Transform.Scale.X, 1f / cloud.Transform.Scale.X, 1f / cloud.Transform.Scale.X, 1f);
            
            cloud.MoveSpeed = cloud.Transform.Scale.X * .25f;
            return cloud;
        }
    }
}
*/
