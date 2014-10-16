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

namespace Modeling3DAssignment
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ModelingGame : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Random Random;

        public List<StaticEntity> StaticEntityList;

        
        public Matrix ViewMatrix;
        public Matrix PerspectiveMatrix;

        public Model TreeModel;

        public ModelingGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            StaticEntityList = new List<StaticEntity>();

            ViewMatrix = new Matrix();
            PerspectiveMatrix = Matrix.CreatePerspectiveFieldOfView(1.5f, 800.0f / 600.0f, 0.1f, 1000.0f);

            Random = new Random();
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

            TreeModel = Content.Load<Model>("tree");

            for (int i = 0; i < 100; i++)
            {
                int x = Random.Next(100);
                int y = Random.Next(100);
                int z = Random.Next(100);

                StaticEntity NewEntity = new StaticEntity();
                NewEntity.Model = TreeModel;
                NewEntity.ModelMatrix = Matrix.CreateTranslation(new Vector3(x, y, z));
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(StaticEntity Entity in StaticEntityList)
            {
                Entity.Draw(gameTime);
            }// StaticEntity

            base.Draw(gameTime);
        }
    }
}
