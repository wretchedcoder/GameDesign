#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace FlockingAssignment
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FlockingGame : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Random Random;

        public List<BasicEntity> FlockList;

        public BasicEntity Player;
        public BasicEntity Midpoint;

        public Texture2D WolfTexture;
        public Texture2D SheepTexture;
        public Texture2D MidTexture;

        int Width;
        int Height;

        public const int PROXIMITY = 50;

        public FlockingGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            FlockList = new List<BasicEntity>();
            Random = new Random();

            Width = graphics.PreferredBackBufferWidth;
            Height = graphics.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            WolfTexture = Content.Load<Texture2D>("wolf");
            SheepTexture = Content.Load<Texture2D>("sheep");
            MidTexture = Content.Load<Texture2D>("midpoint");

            for (int i = 0; i < 20; i++)
            {
                BasicEntity NewEntity = new BasicEntity();
                NewEntity.Game = this;
                NewEntity.Texture = WolfTexture;
                NewEntity.Position = new Vector2(Random.Next(Width), Random.Next(Height));
                NewEntity.DestRectangle = new Rectangle((int)NewEntity.Position.X, 
                    (int)NewEntity.Position.Y, WolfTexture.Width, WolfTexture.Height);

                NewEntity.Velocity = new Vector2((float)Random.NextDouble(),
                    (float)Random.NextDouble());
                NewEntity.Rotation = (float)Math.Atan2(NewEntity.Velocity.Y, NewEntity.Velocity.X);
                NewEntity.Midpoint = new Vector2(WolfTexture.Width / 2.0f, 
                    WolfTexture.Height / 2.0f);
                NewEntity.isFlock = true;
                FlockList.Add(NewEntity);
            }

            Player = new BasicEntity();
            Player.Game = this;
            Player.Texture = SheepTexture;
            Player.Position = new Vector2(Random.Next(Width), Random.Next(Height));
            Player.DestRectangle = new Rectangle((int)Player.Position.X,
                (int)Player.Position.Y, SheepTexture.Width, SheepTexture.Height);
            Player.Velocity = new Vector2(0.0f, 0.0f);
            Player.Rotation = 0.0f;
            Player.Midpoint = new Vector2(SheepTexture.Width / 2.0f,
                SheepTexture.Height / 2.0f);
            Player.isPlayer = true;

            Midpoint = new BasicEntity();
            Midpoint.Game = this;
            Midpoint.Texture = MidTexture;
            Midpoint.Position = new Vector2(Random.Next(Width), Random.Next(Height));
            Midpoint.DestRectangle = new Rectangle((int)Midpoint.Position.X,
                (int)Midpoint.Position.Y, MidTexture.Width, MidTexture.Height);
            Midpoint.Velocity = new Vector2(0.0f, 0.0f);
            Midpoint.Rotation = 0.0f;
            Midpoint.Midpoint = new Vector2(MidTexture.Width / 2.0f,
                MidTexture.Height / 2.0f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (BasicEntity Entity in FlockList)
            {
                Entity.Update(gameTime);
            }
            Player.Update(gameTime);
            Midpoint.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach(BasicEntity Entity in FlockList)
            {
                Entity.Draw(spriteBatch);
            }
            Player.Draw(spriteBatch);
            Midpoint.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
