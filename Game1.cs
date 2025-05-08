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

        KeyboardState keyboardState;
        float gravity, jumpSpeed;
        bool onGround;

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
            platforms.Add(new Rectangle(450, 175, 75, 20));
            platforms.Add(new Rectangle(550, 100, 75, 20));
            platforms.Add(new Rectangle(650, 200, 75, 20));
            platforms.Add(new Rectangle(550, 300, 75, 20));

            gravity = 0.3f; // how fast player accelerated downwards
            jumpSpeed = 8f; // the strength of the jump
            onGround = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            rectangleTexture = Content.Load<Texture2D>("rectangle");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            speed.X = 0f;
            if (keyboardState.IsKeyDown(Keys.A))
            {
                speed.X += -2f;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                speed.X += 2f;
            }
            playerPosition.X += speed.X;
            player.Location = playerPosition.ToPoint();

            foreach (Rectangle platform in platforms)
                if (player.Intersects(platform))
                {
                    playerPosition.X += -speed.X;
                    player.Location = playerPosition.ToPoint();
                }

            if (!onGround) // Applies gravity in beginning
            {
                speed.Y += gravity;
                if (speed.Y < 0 && keyboardState.IsKeyUp(Keys.Space)) // Lets user control the jump based on how long they hold the spacebar!
                {
                    speed.Y /= 1.5f; //0f is jarring. Dividing it makes it smoother
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Space)) // Jumps
            {
                speed.Y -= jumpSpeed;
                onGround = false;
            }
            else // Applies Gravity 24/7
            {
                speed.Y += gravity;
            }
            playerPosition.Y += speed.Y;
            player.Location = playerPosition.ToPoint();

            foreach (Rectangle platform in platforms)
            {
                if (player.Intersects(platform))
                {
                    if (speed.Y > 0f) // lands on top platform
                    {
                        onGround = true;
                        speed.Y = 0;
                        playerPosition.Y = platform.Y - player.Height;
                    }
                    else //hits bottom of platform
                    {
                        speed.Y = 0;
                        playerPosition.Y = platform.Bottom;
                    }
                    player.Location = playerPosition.ToPoint();
                }
            }

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
