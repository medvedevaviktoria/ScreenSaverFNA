using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScreenSaverFNA.Classes;
using System;
using System.Collections.Generic;

namespace ScreenSaverFNA
{
    public class Game1 : Game
    {
        private Texture2D snowflakeTexture;
        private Texture2D backgroundTexture;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Snowflake> Snowflakes;

        const int SnowflakesCount = 1500;
        const int minSize = 16;
        const int maxSize = 32;
        const int minSpeed = 1;
        const int midSpeed = 4;
        const int maxSpeed = 8;
        private readonly Random random = new();


        public Game1()
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
            // TODO: Add your initialization logic here

            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            snowflakeTexture = Content.Load<Texture2D>("snowFlake");
            backgroundTexture = Content.Load<Texture2D>("winterBackground");

            Snowflakes = [];
            for (var i = 0; i < SnowflakesCount; i++)
            {
                var x = random.Next(_graphics.PreferredBackBufferWidth);
                var y = random.Next(_graphics.PreferredBackBufferHeight);
                var size = random.Next(minSize, maxSize);
                var speed = (size == minSize) ? random.Next(minSpeed, midSpeed) : random.Next(midSpeed, maxSpeed);
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
                snowflake.X += snowflake.Speed / 2;

                if (snowflake.Y> _graphics.PreferredBackBufferHeight)
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

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}