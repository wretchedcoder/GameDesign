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

namespace BezierCurvesAssignment
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BezierCurvesGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public List<ControlPoint> PointList;
        public List<Path> PathList;

        MouseState OldMouseState;
        MouseState NewMouseState;
        ControlPoint SelectedPoint;

        public SpriteFont GameFont;

        public BezierCurvesGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            PointList = new List<ControlPoint>();
            PathList = new List<Path>();        
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
            GameFont = Content.Load<SpriteFont>("Arial");

            for (int i = 0; i < 100; i++)
            {
                PathList.Add(new Path(this));
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

            NewMouseState = Mouse.GetState();
            int mouseX = NewMouseState.X;
            int mouseY = NewMouseState.Y;

            if (NewMouseState.LeftButton == ButtonState.Pressed
                && OldMouseState.LeftButton == ButtonState.Pressed
                && SelectedPoint == null)
            {
                for (int i = PointList.Count - 1; i >= 0; i--)
                {
                    if (PointList[i].DestRect.Contains(new Point(mouseX, mouseY)))
                    {
                        SelectedPoint = PointList[i];
                        break;
                    }
                }                
            }
            else if (NewMouseState.LeftButton == ButtonState.Released
                && OldMouseState.LeftButton == ButtonState.Pressed
                && SelectedPoint == null)
            {
                if (PointList.Count < 4)
                {
                    ControlPoint newPoint = new ControlPoint(this);
                    newPoint.Position = new Vector2(mouseX, mouseY);
                    PointList.Add(newPoint);
                }
            }
            else if (NewMouseState.LeftButton == ButtonState.Released
                && OldMouseState.LeftButton == ButtonState.Released)
            {
                if (SelectedPoint != null)
                {
                    SelectedPoint.isDragged = false;
                }
                SelectedPoint = null;
            }
            OldMouseState = NewMouseState;

            if (SelectedPoint != null)
            {
                SelectedPoint.isDragged = true;
            }

            for (int i = PointList.Count - 1; i >= 0; i--)
            {
                PointList[i].Update(gameTime);
            }
            if (PointList.Count >= 2)
            {
                for (int i = 0; i < PathList.Count; i++)
                {
                    PathList[i].Update(i, gameTime);
                }
            }        

            // base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            this.spriteBatch.Begin();

            for (int i = PointList.Count-1; i >= 0; i-- )
            {
                PointList[i].Draw(this.spriteBatch);
            }
            if (PointList.Count >= 2)
            {
                for (int i = 0; i < PathList.Count; i++)
                {
                    PathList[i].Draw(this.spriteBatch);
                }
            }
            

            this.spriteBatch.End();
        }
    }
}
