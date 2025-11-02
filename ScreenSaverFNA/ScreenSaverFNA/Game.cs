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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<Snowflake> Snowflakes;

        const int SnowflakesCount = 1500;
        const int MinSize = 16;
        const int MaxSize = 32;
        const int MinSpeed = 2;
        const int MidSpeed = 5;
        const int MaxSpeed = 9;
        const int SpeedDivider = 2;
        private readonly Random random = new();

        public Game()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            snowflakeTexture = Content.Load<Texture2D>("snowFlake");
            backgroundTexture = Content.Load<Texture2D>("winterBackground");

            Snowflakes = [];
            for (var i = 0; i < SnowflakesCount; i++)
            {
                var x = random.Next(graphics.PreferredBackBufferWidth);
                var y = random.Next(graphics.PreferredBackBufferHeight);
                var size = random.Next(MinSize, MaxSize);
                var speed = (size < (MinSize+MaxSize)/2) ? random.Next(MinSpeed, MidSpeed) : random.Next(MidSpeed, MaxSpeed);
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

                if (snowflake.Y > graphics.PreferredBackBufferHeight)
                {
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = random.Next(graphics.PreferredBackBufferWidth);
                }
                if (snowflake.X > graphics.PreferredBackBufferWidth)
                {
                    snowflake.Y = random.Next(graphics.PreferredBackBufferHeight);
                    snowflake.X = -snowflake.Size;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            foreach(var snowflake in Snowflakes)
            {
                spriteBatch.Draw(snowflakeTexture, new Rectangle(snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}