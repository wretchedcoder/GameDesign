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

namespace ParticleAssignment
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ParticleGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Particle> ParticleList;

        Random RandomGenerator;

        public ParticleGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ParticleList = new List<Particle>();
            this.IsMouseVisible = true;
            RandomGenerator = new Random();
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

            int MouseX = Mouse.GetState().X;
            int MouseY = Mouse.GetState().Y;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed) 
            {
                Particle NewParticle = new Particle(this);
                NewParticle.Position = new Vector2(MouseX, MouseY);                
                // Rotation is a random double from 0.0 to 1.0 multiplied by 2.0 PI (Circle is made of 2 PI)
                NewParticle.Rotation = (float)(RandomGenerator.NextDouble() * 2.0f * Math.PI);
                
                // We use the cosine and sin multiplied by fixed numbers which determine the
                // initial velocity (these numbers can be changed to alter the initial velocity)
                NewParticle.Velocity = new Vector2((float)(Math.Cos(NewParticle.Rotation) * 6.0), 
                    (float)(Math.Sin(NewParticle.Rotation) * 4.0 - 2.0));
                NewParticle.Midpoint = new Vector2(NewParticle.Texture.Width / 2.0f, 
                    NewParticle.Texture.Height / 2.0f);
                NewParticle.DestRectangle = new Rectangle((int)NewParticle.Position.X, (int)NewParticle.Position.Y, 
                    NewParticle.Texture.Width, NewParticle.Texture.Height);
                NewParticle.Scale = new Vector2(0.1f, 0.1f);
                NewParticle.Floor = graphics.PreferredBackBufferHeight - 1;
                this.ParticleList.Add(NewParticle);

            }

            for (int i = ParticleList.Count - 1; i > -1; i--)
            {
                if (ParticleList[i].IsAlive)
                {
                    ParticleList[i].Update(gameTime);
                }
                else
                {
                    ParticleList.Remove(ParticleList[i]);
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
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);

            for (int i = ParticleList.Count - 1; i > -1; i--)
            {
                ParticleList[i].Draw(spriteBatch);
            }     

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
