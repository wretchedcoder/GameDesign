﻿#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using NobleQuest.Entity;
using System;
using System.Collections.Generic;
using WMPLib;
#endregion

namespace NobleQuest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class NobleQuestGame : Game
    {
        public bool GameStarted = false;
        public bool GameWon = false;
        public bool GameLost = false;

        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;

        public GameEntity BackgroundEntity;
        public Song Song;

        public GameEntity TitleTextEntity;
        public GameEntity StartTextEntity;
        public GameEntity VictoryTextEntity;
        public GameEntity DefeatTextEntity;

        public float TextTime = 0.0f;
        public float TextCooldown = 5.0f;

        public List<GameEntity> GameEntityList;
        public List<NodeEntity> NodeEntityList;
        public List<PathEntity> PathEntityList;
        public List<DynamicEntity> DynamicEntityList;
        public List<PathSelectionEntity> PathSelectionList;
        public List<GameEntity> OrderList;
        public EntityFactory EntityFactory;
        public Player Player;
        public Enemy Enemy;
        public SelectionEntity SelectionEntity;

        public float TimeCounter;

        public SpriteFont SpriteFont;
        public static Texture2D BlockBar;

        public Random Random;

        public MouseState OldMouseState;
        public MouseState NewMouseState;
        public KeyboardState OldKeyState;
        public KeyboardState NewKeyState;
        public NodeEntity SelectedNode;

        public NodeEntity TopEnd = null;
        public NodeEntity MidEnd = null;
        public NodeEntity BotEnd = null;

        public NobleQuestGame()
            : base()
        {
            base.IsMouseVisible = true;
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferHeight = 500;
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            GameEntityList = new List<GameEntity>();           

            this.Random = new Random();
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
            SelectionEntity = new SelectionEntity(this);

            this.buildMap();

            this.InitTitleScreen();

            base.Initialize();
        }

        public void buildMap()
        {
            GameEntityList = new List<GameEntity>();
            NodeEntityList = new List<NodeEntity>();
            PathEntityList = new List<PathEntity>();
            DynamicEntityList = new List<DynamicEntity>();
            PathSelectionList = new List<PathSelectionEntity>();
            OrderList = new List<GameEntity>();
            TopEnd = null;
            MidEnd = null;
            BotEnd = null;

            this.EntityFactory = new EntityFactory();
            this.Player = new Player();
            this.Player.Game = this;
            this.Enemy = new Enemy(this);

            MapBuilder MapBuilder = new MapBuilder();
            MapBuilder.BuildMap(this);

            this.Enemy.initPaths();
        }

        public void InitTitleScreen()
        {
            BackgroundEntity = EntityFactory.GetBackgroundEntity(this);
            TitleTextEntity = EntityFactory.GetTitleTextEntity(this);
            StartTextEntity = EntityFactory.GetStartTextEntity(this);

            VictoryTextEntity = EntityFactory.GetVictoryTextEntity(this);
            DefeatTextEntity = EntityFactory.GetDefeatTextEntity(this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = "Content/MinstrelGuild.mp3";
            wplayer.controls.play();
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

            if (this.GameStarted)
            {
                if (this.GameWon)
                {
                    this.UpdateEndText(gameTime);
                }
                else if (this.GameLost)
                {
                    this.UpdateEndText(gameTime);
                }
                else
                {
                    this.UpdateGame(gameTime);
                }
            }
            else
            {
                this.UpdateTitle();
            }           

            base.Update(gameTime);
        }

        public void UpdateTitle()
        {
            NewMouseState = Mouse.GetState();
            if (OldMouseState != null)
            {
                if (OldMouseState.LeftButton == ButtonState.Pressed
                    && NewMouseState.LeftButton == ButtonState.Released)
                {
                    this.GameStarted = true;
                }
            }
            OldMouseState = NewMouseState;
        }

        public void UpdateGame(GameTime gameTime)
        {
            TimeCounter += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            #region Get Selected Node
            NewMouseState = Mouse.GetState();
            if (OldMouseState != null)
            {
                if (OldMouseState.LeftButton == ButtonState.Pressed
                    && NewMouseState.LeftButton == ButtonState.Released)
                {
                    for (int i = NodeEntityList.Count - 1; i >= 0; i--)
                    {
                        int NodeXOffset = (int)NodeEntityList[i].Midpoint.X;
                        int NodeYOffset = (int)NodeEntityList[i].Midpoint.Y;
                        if (NodeEntityList[i].DestRectangle.Contains(NewMouseState.X, NewMouseState.Y))
                        {
                            SelectionEntity.setSelectedNode(NodeEntityList[i]);
                            break;
                        }
                        SelectionEntity.SelectedNode = null;
                    }
                }
            }
            OldMouseState = NewMouseState;
            #endregion Get Selected Node

            // Keyboard.GetState().IsKeyDown(Keys.Escape)
            NewKeyState = Keyboard.GetState();
            if (OldKeyState != null)
            {
                if (OldKeyState.IsKeyDown(Keys.Tab)
                    && NewKeyState.IsKeyUp(Keys.Tab)
                    && SelectionEntity.SelectedNode != null)
                {
                    SelectionEntity.SelectedNode.IncrementPreferredPath();
                }

                if (OldKeyState.IsKeyDown(Keys.LeftShift)
                    && NewKeyState.IsKeyUp(Keys.LeftShift)
                    && SelectionEntity.SelectedNode != null)
                {
                    SelectionEntity.SelectedNode.ClearPreferred();
                }

                if (OldKeyState.IsKeyDown(Keys.W)
                    && NewKeyState.IsKeyUp(Keys.W)
                    && SelectionEntity.SelectedNode != null)
                {
                    SelectionEntity.SelectedNode.ToggleWaitOrder();
                }
            }
            OldKeyState = NewKeyState;


            this.Player.Update(gameTime);
            this.Enemy.Update(gameTime);
            this.Player.Resources.Update(gameTime);
            this.Enemy.Resources.Update(gameTime);
            for (int i = PathEntityList.Count - 1; i >= 0; i--)
            {
                if (PathEntityList[i].IsAlive)
                {
                    PathEntityList[i].Update(gameTime);
                }     
                else
                {
                    PathEntityList.Remove(PathEntityList[i]);
                }
            }
            for (int i = NodeEntityList.Count - 1; i >= 0; i--)
            {
                if (NodeEntityList[i].IsAlive)
                {
                    NodeEntityList[i].Update(gameTime);
                }
                else
                {
                    NodeEntityList.Remove(NodeEntityList[i]);
                }
            }
            for (int i = DynamicEntityList.Count - 1; i >= 0; i--)
            {
                if (DynamicEntityList[i].IsAlive)
                {
                    DynamicEntityList[i].Update(gameTime);
                }
                else
                {
                    DynamicEntityList.Remove(DynamicEntityList[i]);
                }
            }
            SelectionEntity.Update(gameTime);


            // Check for Dynamic Entity Collisions
            for (int i = DynamicEntityList.Count - 1; i >= 0; i--)
            {
                DynamicEntity dynamicEntity = DynamicEntityList[i];
                if (dynamicEntity.DestRectangle.Intersects(Player.Town.DestRectangle))
                {
                    dynamicEntity.HandleCollision(Player.Town);
                }
                else if (dynamicEntity.DestRectangle.Intersects(Enemy.Town.DestRectangle))
                {
                    dynamicEntity.HandleCollision(Enemy.Town);
                }
                for (int j = NodeEntityList.Count - 1; j >= 0; j--)
                {
                    NodeEntity nodeEntity = NodeEntityList[j];
                    if (dynamicEntity.DestRectangle.Intersects(nodeEntity.DestRectangle))
                    {
                        dynamicEntity.HandleCollision(nodeEntity);
                    }
                }
                for (int j = DynamicEntityList.Count - 1; j >= 0; j--)
                {
                    DynamicEntity otherDynEntity = DynamicEntityList[j];
                    if (dynamicEntity.DestRectangle.Intersects(otherDynEntity.DestRectangle))
                    {
                        dynamicEntity.HandleCollision(otherDynEntity);
                    }
                }
            }
        }

        public void UpdateEndText(GameTime gameTime)
        {
            this.TextTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (this.TextTime >= this.TextCooldown)
            {
                this.GameStarted = false;
                this.GameWon = false;
                this.GameLost = false;
                this.TextTime = 0.0f;
                this.GameEntityList.Clear();
                this.NodeEntityList.Clear();
                this.PathEntityList.Clear();
                this.DynamicEntityList.Clear();
                this.PathSelectionList.Clear();
                this.OrderList.Clear();
                this.buildMap();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Ivory);

            this.SpriteBatch.Begin();
            this.BackgroundEntity.Draw(this.SpriteBatch);

            if (this.GameStarted)
            {
                if (this.GameWon)
                {
                    this.DrawVictory();
                }
                else if (this.GameLost)
                {
                    this.DrawDefeat();
                }
                else
                {
                    this.DrawGame();
                }
            }
            else
            {
                this.DrawTitle();
            }
            
            this.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawTitle()
        {
            TitleTextEntity.Draw(this.SpriteBatch);
            StartTextEntity.Draw(this.SpriteBatch);
        } // End DrawTitle()

        public void DrawGame()
        {
            this.Player.Resources.Draw(this.SpriteBatch);
            for (int i = PathEntityList.Count - 1; i >= 0; i--)
            {
                PathEntityList[i].Draw(this.SpriteBatch);
            }
            for (int i = NodeEntityList.Count - 1; i >= 0; i--)
            {
                NodeEntityList[i].Draw(this.SpriteBatch);
            }
            for (int i = PathSelectionList.Count - 1; i >= 0; i--)
            {
                PathSelectionList[i].Draw(this.SpriteBatch);
            }
            for (int i = DynamicEntityList.Count - 1; i >= 0; i--)
            {
                DynamicEntityList[i].Draw(this.SpriteBatch);
            }            
            for (int i = OrderList.Count - 1; i >= 0; i--)
            {
                OrderList[i].Draw(this.SpriteBatch);
            }
            SelectionEntity.Draw(this.SpriteBatch);
        } // End DrawGame()

        public void DrawVictory()
        {
            VictoryTextEntity.Draw(this.SpriteBatch);
        } // End DrawVictory()

        public void DrawDefeat()
        {
            DefeatTextEntity.Draw(this.SpriteBatch);
        } // End DrawDefeat();
    }
}
