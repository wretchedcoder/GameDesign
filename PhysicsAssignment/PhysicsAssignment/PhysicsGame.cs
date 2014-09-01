#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;
#endregion

namespace PhysicsAssignment
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PhysicsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random generator;

        // Textures
        Texture2D texMovingCircle;
        Texture2D texStaticCircle;

        // Constants
        const int MAX_STATIC_CIRCLES = 9;
        const int MAX_STATIC_ROWS = 8;

        // Game Variables
        World world;
        Body floor;
        Body circle;
        float timer = 0.0f;
        float limit = 1.0f;

        Texture2D floorTex;
        Rectangle floorRect;
        Texture2D circleTex;
        Vector2 boxOffset;

        

        public PhysicsGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.LoadContent();
            
            generator = new Random();

            // Physics World Init
            world = new World(new Vector2(0.0f, 9.82f));            

            // Initialize Circle Lattice
            this.InitializeCircleLattice();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.texMovingCircle = Content.Load<Texture2D>("MovingCircle");
            this.texStaticCircle = Content.Load<Texture2D>("StaticCircle");
            this.boxOffset = new Vector2(texMovingCircle.Width / 2, texMovingCircle.Height / 2);
        }

        /// <summary>
        /// Creates the set of circles that form the lattice
        /// that the moving circles will go through
        /// </summary>
        protected void InitializeCircleLattice()
        {
            float screenWidth = graphics.PreferredBackBufferWidth;
            float screenHeight = graphics.PreferredBackBufferHeight;

            float heightStep = screenHeight / MAX_STATIC_ROWS;
            for (int i = 2; i < MAX_STATIC_ROWS - 2; i++)
            {
                int circlesThisRow = MAX_STATIC_CIRCLES + (i % 2);
                float widthStep = screenWidth / circlesThisRow;
                for (int j = 1; j < MAX_STATIC_CIRCLES; j++)
                {
                    Vector2 position = new Vector2(j * widthStep, i * heightStep);

                    Body newCircle = BodyFactory.CreateCircle(
                        world, ConvertUnits.ToSimUnits(16), ConvertUnits.ToSimUnits(16));
                    newCircle.Position = ConvertUnits.ToSimUnits(position);
                    newCircle.IsStatic = true;
                    newCircle.Restitution = 0.3f;
                    newCircle.Friction = 0.2f;
                }
            }
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
            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;
            if (timer > limit)
            {
                timer -= limit;
                CreateMovingCircle();
            }

            base.Update(gameTime);
        }

        protected void CreateMovingCircle()
        {
            float r = generator.Next(5) + 1;

            Body newCircle = BodyFactory.CreateCircle(
                        world, ConvertUnits.ToSimUnits(16), ConvertUnits.ToSimUnits(16));
            newCircle.Position = ConvertUnits.ToSimUnits(new Vector2(99f * r, 10f));
            newCircle.IsStatic = false;
            newCircle.Restitution = 0.3f;
            newCircle.Friction = 0.2f;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //SpriteBatch.Draw(floorTex, floorRect, Color.White);
            foreach (Body b in world.BodyList)
            {
                if (b.IsStatic)
                {
                    spriteBatch.Draw(
                        texStaticCircle, 
                        ConvertUnits.ToDisplayUnits(b.Position), 
                        null, 
                        Color.White, 
                        b.Rotation, 
                        boxOffset, 
                        1.0f, 
                        SpriteEffects.None, 
                        1.0f);
                }
                else
                {
                    spriteBatch.Draw(
                        texMovingCircle,
                        ConvertUnits.ToDisplayUnits(b.Position),
                        null,
                        Color.White,
                        b.Rotation,
                        boxOffset,
                        1.0f,
                        SpriteEffects.None,
                        1.0f);
                }
                if (b.Position.Y > 500)
                {
                    world.RemoveBody(b);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
