using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScreenSaverFNA.Classes;
using System;
using System.Collections.Generic;

namespace ScreenSaverFNA
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private Texture2D snowflakeTexture;
        private Texture2D backgroundTexture;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Snowflake> Snowflakes;

        const int SnowflakesCount = 1500;
        const int MinSize = 16;
        const int MaxSize = 32;
        const int MinSpeed = 2;
        const int MidSpeed = 4;
        const int MaxSpeed = 8;
        const int SpeedDivider = 2;
        private readonly Random random = new();

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            snowflakeTexture = Content.Load<Texture2D>("snowFlake");
            backgroundTexture = Content.Load<Texture2D>("winterBackground");

            Snowflakes = [];
            for (var i = 0; i < SnowflakesCount; i++)
            {
                var x = random.Next(_graphics.PreferredBackBufferWidth);
                var y = random.Next(_graphics.PreferredBackBufferHeight);
                var size = random.Next(MinSize, MaxSize);
                var speed = (size == MinSize) ? random.Next(MinSpeed, MidSpeed) : random.Next(MidSpeed, MaxSpeed);
                Snowflakes.Add(new Snowflake(x, y, size, speed));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0) //допустим...
            { 
                Exit(); 
            }

            foreach (var snowflake in Snowflakes)
            {
                snowflake.Y += snowflake.Speed;
                snowflake.X += snowflake.Speed / SpeedDivider;

                if (snowflake.Y > _graphics.PreferredBackBufferHeight)
                {
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = random.Next(_graphics.PreferredBackBufferWidth);
                }
                if (snowflake.X > _graphics.PreferredBackBufferWidth)
                {
                    snowflake.Y = random.Next(_graphics.PreferredBackBufferHeight);
                    snowflake.X = -snowflake.Size;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            foreach(var snowflake in Snowflakes)
            {
                _spriteBatch.Draw(snowflakeTexture, new Rectangle(snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size), Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}