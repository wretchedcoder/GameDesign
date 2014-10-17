using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace FlockingAssignment
{
    public class BasicEntity
    {
        public FlockingGame Game;
        public Vector2 Position;
        public Rectangle DestRectangle;
        public Vector2 Velocity;
        public float Rotation;
        public Texture2D Texture;
        public Vector2 Midpoint;

        public bool isFlock;
        public bool isPlayer;

        public KeyboardState OldKeyState;
        public KeyboardState NewKeyState;

        public void Update(GameTime gameTime)
        {
            if (isFlock)
            {
                Vector2 AvoidanceVector = new Vector2(0f, 0f);
                float distance = 0.0f;
                foreach (BasicEntity Entity in Game.FlockList)
                {
                    distance = Vector2.Distance(Entity.Position, this.Position);

                    // Proximity Check
                    if (distance > (float) FlockingGame.PROXIMITY
                        || distance == 0.0f)
                    {
                        continue;
                    }

                    // Calculate Avoidance Vector
                    Vector2 thisAvoidance = Vector2.Zero;
                    thisAvoidance.X = Entity.Position.X - this.Position.X;
                    thisAvoidance.Y = Entity.Position.Y - this.Position.Y;

                    //thisAvoidance = Vector2.Multiply(thisAvoidance, (distance / FlockingGame.PROXIMITY));

                    AvoidanceVector += Vector2.Multiply(thisAvoidance, -1.0f);
                }
                // Calculate Player Vector
                Vector2 PlayerVector = new Vector2(0f, 0f);
                PlayerVector.X = Game.Player.Position.X - this.Position.X;
                PlayerVector.Y = Game.Player.Position.Y - this.Position.Y;

                PlayerVector = Vector2.Divide(PlayerVector, 1000);
                AvoidanceVector = Vector2.Divide(AvoidanceVector, 175);
                this.Velocity = PlayerVector + AvoidanceVector;
                if (this.Velocity.X !=  0 || this.Velocity.Y != 0)
                {
                    this.Rotation = (float)Math.Atan2(this.Velocity.Y, this.Velocity.X);
                }
                //this.Velocity = PlayerVector;
            }// if(isFlock)
            else if (isPlayer)
            {
                if (this.Velocity.X > 0)
                {
                    this.Velocity.X -= 1;
                }
                else if (this.Velocity.X < 0)
                {
                    this.Velocity.X += 1;
                }
                if (this.Velocity.Y > 0)
                {
                    this.Velocity.Y -= 1;
                }
                else if (this.Velocity.Y < 0)
                {
                    this.Velocity.Y += 1;
                }
                NewKeyState = Keyboard.GetState();
                if (OldKeyState != null)
                {
                    if (OldKeyState.IsKeyDown(Keys.W)
                        && NewKeyState.IsKeyDown(Keys.W))
                    {
                        this.Velocity.Y -= 1;
                    }
                    if (OldKeyState.IsKeyDown(Keys.A)
                        && NewKeyState.IsKeyDown(Keys.A))
                    {
                        this.Velocity.X -= 1;
                    }
                    if (OldKeyState.IsKeyDown(Keys.S)
                        && NewKeyState.IsKeyDown(Keys.S))
                    {
                        this.Velocity.Y += 1;
                    }
                    if (OldKeyState.IsKeyDown(Keys.D)
                        && NewKeyState.IsKeyDown(Keys.D))
                    {
                        this.Velocity.X += 1;
                    }
                }
                OldKeyState = NewKeyState;
                if (this.Velocity.X != 0 || this.Velocity.Y != 0)
                {
                    this.Rotation = (float)Math.Atan2(this.Velocity.Y, this.Velocity.X);
                }
            } // isPlayer
            else
            {
                Vector2 Midpoint = new Vector2(0f, 0f);
                foreach (BasicEntity Entity in Game.FlockList)
                {
                    Midpoint += Entity.Position;
                }
                Midpoint = Vector2.Divide(Midpoint, Game.FlockList.Count);
                Game.Midpoint.Position = Midpoint;
            } // midpoint

            Position += Velocity;
            DestRectangle.X = (int)Position.X;
            DestRectangle.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 
                Rotation, Midpoint, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
