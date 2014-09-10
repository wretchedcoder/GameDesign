#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using NobleQuest.Entity;
#endregion

namespace NobleQuest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class NobleQuestGame : Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;
        List<GameEntity> GameEntityList;
        EntityFactory EntityFactory;
        public GameEntity PlayerCity { get; set; }
        public GameEntity EnemyCity { get; set; }

        public NobleQuestGame()
            : base()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameEntityList = new List<GameEntity>();
            EntityFactory = new EntityFactory();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            MapBuilder MapBuilder = new MapBuilder();

            GameEntityList.AddRange(MapBuilder.BuildMap(this));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
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

            bool sIsPressed = Keyboard.GetState().IsKeyDown(Keys.S);

            if (sIsPressed)
            {
                // TODO: Check for Blacksmith in Player Town

                GameEntityList.Insert(0, EntityFactory.GetInfantryEntity(this, true, this.PlayerCity));
            }

            foreach (GameEntity gameEntity in GameEntityList)
            {
                gameEntity.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Ivory);

            this.SpriteBatch.Begin();
            for (int i = GameEntityList.Count - 1; i >= 0; i-- )
            {
                GameEntityList[i].Draw(this.SpriteBatch);
            }
            this.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
