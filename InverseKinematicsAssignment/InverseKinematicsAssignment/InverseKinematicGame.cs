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

namespace InverseKinematicsAssignment
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class InverseKinematicGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int BONE_COUNT = 5;

        public List<BoneEntity> EntityList;
        public BoneEntity[] LeafNodes;
        public BoneEntity CurrentLeaf;
        public int iterationCount = 0;
        public int MAX_ITERATIONS = 20;

        public MouseState OldMouseState;
        public MouseState NewMouseState;

        public Vector2 MousePosition;

        public InverseKinematicGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";

            EntityList = new List<BoneEntity>();
            LeafNodes = new BoneEntity[2];
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            BoneEntity LastEntity = null;
            for (int i = 0; i < BONE_COUNT; i++)
            {
                BoneEntity NewEntity = new BoneEntity(this);
                if (LastEntity == null)
                {
                    NewEntity.Position.X = 100.0f;
                    NewEntity.Position.Y = 450.0f;
                }
                else
                {
                    NewEntity.ParentBone = LastEntity;
                    LastEntity.ChildBone = NewEntity;
                }
                
                NewEntity.DestRectangle = new Rectangle();
                NewEntity.DestRectangle.X = (int)NewEntity.Position.X;
                NewEntity.DestRectangle.Y = (int)NewEntity.Position.Y;
                NewEntity.DestRectangle.Width = NewEntity.Texture.Width;
                NewEntity.DestRectangle.Height = NewEntity.Texture.Height;
                this.EntityList.Add(NewEntity);
                LastEntity = NewEntity;
            }
            LeafNodes[0] = LastEntity;
            LastEntity = null;

            for (int i = 0; i < BONE_COUNT; i++)
            {
                BoneEntity NewEntity = new BoneEntity(this);
                if (LastEntity == null)
                {
                    NewEntity.Position.X = 600.0f;
                    NewEntity.Position.Y = 450.0f;
                }
                else
                {
                    NewEntity.ParentBone = LastEntity;
                    LastEntity.ChildBone = NewEntity;
                }

                NewEntity.DestRectangle = new Rectangle();
                NewEntity.DestRectangle.X = (int)NewEntity.Position.X;
                NewEntity.DestRectangle.Y = (int)NewEntity.Position.Y;
                NewEntity.DestRectangle.Width = NewEntity.Texture.Width;
                NewEntity.DestRectangle.Height = NewEntity.Texture.Height;
                this.EntityList.Add(NewEntity);
                LastEntity = NewEntity;
            }
            LeafNodes[1] = LastEntity;

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

            NewMouseState = Mouse.GetState();

            this.MousePosition = new Vector2(this.NewMouseState.X,
                this.NewMouseState.Y);

            CurrentLeaf = LeafNodes[0];
            CurrentLeaf.Update(gameTime);

            iterationCount = 0;

            CurrentLeaf = LeafNodes[1];
            CurrentLeaf.Update(gameTime);


            //for (int i = EntityList.Count - 1; i >= 0; i--)
            //{
            //    EntityList[i].Update(gameTime);
            //}

            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for (int i = EntityList.Count - 1; i >= 0; i--)
            {
                EntityList[i].Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
