using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRex.Models;
using TRex.Sprites;

namespace TRex
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public static Random Random;
        
        public static int ScreenHeight;
        public static int ScreenWidth;

        private SpriteFont _font;
        private Texture2D Ground;
        
        private List<Sprite> _sprites;
        private float _timer;
        public static float GlobalTimer;
        private float _timerSpeed;

        public static float CurSpeed;
        public static int HighScore;

        public static bool Start;
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "T-Rex Game by Epic Boi (Made in around a day using Monogame)";
            Window.AllowAltF4 = true;

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            
            Random = new Random();

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;
            
            CurSpeed = 5f;
            Start = true;
        }

        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Font");
            Ground = Content.Load<Texture2D>("Ground");
            
            Restart();
        }

        private void Restart()
        {
            //var cactusTexture = Content.Load<Texture2D>("Player");
            var playerTexture = Content.Load<Texture2D>("Player");

            _sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                    Position = new Vector2(100,Game1.ScreenHeight * .5f),
                    Color = Color.White,
                    Speed = CurSpeed,
                },
                
                //new Cactus(playerTexture)
                //{
                //    
                //    Position = new Vector2(1000, Game1.ScreenHeight * .5f),
                //    Color = Color.Red,
                //    Speed = 2f,
                //}
            };
            
            
            
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

       
        protected override void Update(GameTime gameTime)
        {
            
            Input.UpdateState();   

            _timer += (float) gameTime.ElapsedGameTime.TotalSeconds;
            _timerSpeed += (float) gameTime.ElapsedGameTime.TotalSeconds;
            GlobalTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            foreach(var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            if (_timer >= Random.Next(0,150))
            {
                _timer = 0;
                //_sprites.Add(new Cactus(Content.Load<Texture2D>("Player")));
                _sprites.Add(
                    new Cactus(Content.Load<Texture2D>("Cactus"))
                );

            }

            if (_timerSpeed >= 5f)
            {
                _timerSpeed = 0;
                CurSpeed += 1f;
            }


            PostUpdate();
            
            base.Update(gameTime);
            
            
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }

                if (sprite is Player)
                {
                    var player = sprite as Player;
                    if (player.HasDied)
                    {
                        if (HighScore < ((Player)sprite).Score)
                            HighScore = ((Player)sprite).Score;
                        Restart();
                    }
                }
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0f, .05f, .1f, 1f));

           spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
           
           foreach(var sprite in _sprites)
               sprite.Draw(spriteBatch);
           
           spriteBatch.Draw(Ground, new Vector2(0, Game1.ScreenHeight * .5f + 100f), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
           
           var fontY = 10;
           var i = 0;

           foreach (var sprite in _sprites)
           {
               if (sprite is Player)
               {
                   spriteBatch.DrawString(_font, string.Format("Score: {0}", ((Player)sprite).Score), 
                       new Vector2(1020, fontY += 20), Color.White);
                   spriteBatch.DrawString(_font, string.Format("High Score: {0}", HighScore), 
                       new Vector2(1020, fontY += 20), Color.White);
               }
                
           }
           
           spriteBatch.End();

           base.Draw(gameTime);
        }
    }
}
