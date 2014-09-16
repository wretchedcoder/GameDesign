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
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public List<GameEntity> GameEntityList;
        public List<NodeEntity> NodeEntityList;
        public List<PathEntity> PathEntityList;
        public List<DynamicEntity> DynamicEntityList;
        public EntityFactory EntityFactory;
        public Player Player;
        public Enemy Enemy;

        public float TimeCounter;

        public SpriteFont SpriteFont;
        public static Texture2D BlockBar;

        public NobleQuestGame()
            : base()
        {
            base.IsMouseVisible = true;
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            GameEntityList = new List<GameEntity>();
            NodeEntityList = new List<NodeEntity>();
            PathEntityList = new List<PathEntity>();
            DynamicEntityList = new List<DynamicEntity>();
            this.EntityFactory = new EntityFactory();
            this.Player = new Player();
            this.Player.Game = this;
            this.Enemy = new Enemy();

            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SpriteFont = Content.Load<SpriteFont>("Arial");
            BlockBar = Content.Load<Texture2D>("BlockBar");
            MapBuilder MapBuilder = new MapBuilder();

            MapBuilder.BuildMap(this);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            TimeCounter += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            this.Player.Update(gameTime);
            // this.Enemy.Update(gameTime);
            this.Player.Resources.Update(gameTime);
            this.Enemy.Resources.Update(gameTime);
            for (int i = PathEntityList.Count - 1; i >= 0; i--)
            {
                PathEntityList[i].Update(gameTime);
            }
            for (int i = NodeEntityList.Count - 1; i >= 0; i--)
            {
                NodeEntityList[i].Update(gameTime);
            }
            for (int i = DynamicEntityList.Count - 1; i >= 0; i--)
            {
                DynamicEntityList[i].Update(gameTime);
            }

            // Check for Dynamic Entity Collisions
            foreach(DynamicEntity dynamicEntity in DynamicEntityList)
            {
                foreach(NodeEntity nodeEntity in NodeEntityList)
                {
                    if (dynamicEntity.DestRectangle.Intersects(nodeEntity.DestRectangle))
                    {
                        dynamicEntity.HandleCollision(nodeEntity);
                    }
                }
                foreach(DynamicEntity otherDynEntity in DynamicEntityList)
                {
                    if (dynamicEntity.DestRectangle.Intersects(otherDynEntity.DestRectangle))
                    {
                        dynamicEntity.HandleCollision(otherDynEntity);
                    }
                }
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
            this.Player.Resources.Draw(this.SpriteBatch);
            // this.Enemy.Resources.Draw(this.SpriteBatch);
            for (int i = PathEntityList.Count - 1; i >= 0; i-- )
            {
                PathEntityList[i].Draw(this.SpriteBatch);
            }
            for (int i = NodeEntityList.Count - 1; i >= 0; i--)
            {
                NodeEntityList[i].Draw(this.SpriteBatch);
            }
            for (int i = DynamicEntityList.Count - 1; i >= 0; i--)
            {
                DynamicEntityList[i].Draw(this.SpriteBatch);
            }
            this.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
