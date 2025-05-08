using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_Platformer_Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D rectangleTexture;
        Rectangle player;
        Vector2 speed, playerPosition;

        List<Rectangle> platforms;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            speed = Vector2.Zero;
            playerPosition = new Vector2(10,10);
            player = new Rectangle(10,10,50,50);
            platforms = new List<Rectangle>();

            platforms.Add(new Rectangle(0,400,800,20)); //Ground!
            platforms.Add(new Rectangle(100,350, 100,20));
            platforms.Add(new Rectangle(350,250, 75,20));
            platforms.Add(new Rectangle(200,300,75,20));
            platforms.Add(new Rectangle(150, 10, 75, 20));
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            rectangleTexture = Content.Load<Texture2D>("rectangle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(rectangleTexture, player, Color.BlueViolet);

            foreach (Rectangle platform in platforms)
            {
                _spriteBatch.Draw(rectangleTexture, platform, Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
