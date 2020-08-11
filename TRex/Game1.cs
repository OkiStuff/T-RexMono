using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private SpriteFont _bigFont;
        private Texture2D Ground;
        
        private List<Sprite> _sprites;
        private float _timer;
        public static float GlobalTimer;
        private float _timerSpeed;

        public static float CurSpeed;
        public static int HighScore;

        private bool _hasStarted = false;
        
        public static Color TextColor = Color.White;
        public static SoundEffectInstance JumpSound, DeathSound, RestartSound, ScoreBonusSound, ButtonHover, BGMusic;
        
        
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            Window.AllowAltF4 = true;

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            
            Random = new Random();

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;
            
            CurSpeed = 5f;
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {  
            //Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Mouse"), 0, 0));
            Window.Title = "Google Dinosaur Game By Epic Boi";
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _bigFont = Content.Load<SpriteFont>("big");
            
            _font = Content.Load<SpriteFont>("Font");
            Ground = Content.Load<Texture2D>("Ground");
            
            JumpSound = Content.Load<SoundEffect>("Sounds/Jump").CreateInstance();
            JumpSound.Volume = .1f;
            
            DeathSound = Content.Load<SoundEffect>("Sounds/Death").CreateInstance();
            DeathSound.Volume = .5f;
            
            RestartSound = Content.Load<SoundEffect>("Sounds/Restart").CreateInstance();
            RestartSound.Volume = .5f;
            
            ScoreBonusSound = Content.Load<SoundEffect>("Sounds/ScoreBonus").CreateInstance();
            ScoreBonusSound.Volume = .5f;
            
            ButtonHover = Content.Load<SoundEffect>("Sounds/ButtonHover").CreateInstance();
            ButtonHover.Volume = .5f;

            BGMusic = Content.Load<SoundEffect>("Sounds/Soft_Piano").CreateInstance();
            BGMusic.Volume = .1f;
            BGMusic.IsLooped = true;
            
            Sounds.PlaySound(SoundTypes.BGMusic);

            Restart();
        }

        private void Restart()
        {
            if (_hasStarted)
                Sounds.PlaySound(SoundTypes.Restart);
            //var cactusTexture = Content.Load<Texture2D>("Player");
            var playerTexture = Content.Load<Texture2D>("Player");
            CurSpeed = 5f;
            
            _sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                    Position = new Vector2(100,Game1.ScreenHeight * .5f),
                    Color = TextColor,
                    Speed = CurSpeed,
                },
                
                //new Cactus(Content.Load<Texture2D>("Cactus"))
                //{
                //    
                //    Position = new Vector2(1000, Game1.ScreenHeight * .5f),
                //    Color = Color.Red,
                //    Speed = 5f,
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
            
            // timer
            _timer += (float) gameTime.ElapsedGameTime.TotalSeconds;
            _timerSpeed += (float) gameTime.ElapsedGameTime.TotalSeconds;
            GlobalTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            if (Input.IsKeyReleased(Keys.Space) && !_hasStarted && _timer >= 0.25f)
            {
                _timer = 0f;
                _hasStarted = true;
                Sounds.PlaySound(SoundTypes.Restart);
                BGMusic.Volume += .2f;
            }

            if (!_hasStarted)
                return;
            
            

            

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
                        Sounds.PlaySound(SoundTypes.Death);
                        if (HighScore < ((Player)sprite).Score)
                            HighScore = ((Player)sprite).Score;

                        // reset timer
                        _timer = 0f;
                        _timerSpeed = 0f;
                        GlobalTimer = 0f;
                        
                        BGMusic.Volume = .1f;
                        _hasStarted = false;
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

           if (!_hasStarted)
               spriteBatch.DrawString(_bigFont, string.Format("Press space to start!"),
                   new Vector2(Game1.ScreenHeight * .5f, Game1.ScreenHeight * .5f - 100), TextColor);//,0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

           foreach (var sprite in _sprites)
           {
               if (sprite is Player && _hasStarted)
               {
                   spriteBatch.DrawString(_font, string.Format("Score: {0}", ((Player)sprite).Score), 
                       new Vector2(1020, fontY += 20), TextColor);
                   spriteBatch.DrawString(_font, string.Format("High Score: {0}", HighScore), 
                       new Vector2(1020, fontY += 20), TextColor);
               }
                
           }
           
           spriteBatch.End();

           base.Draw(gameTime);
        }
    }
}
