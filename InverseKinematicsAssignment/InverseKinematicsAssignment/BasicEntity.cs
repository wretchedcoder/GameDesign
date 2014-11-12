using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace InverseKinematicsAssignment
{
    public class BasicEntity
    {
        public InverseKinematicGame Game;
        public Vector2 Position;
        public Rectangle DestRectangle;
        public Vector2 Velocity;
        public float Rotation;
        public Texture2D Texture;
        public Vector2 Midpoint;
        public float Scale = 1.0f;

        public bool isFlock;
        public bool isPlayer;

        public KeyboardState OldKeyState;
        public KeyboardState NewKeyState;

        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity;
            DestRectangle.X = (int)Position.X;
            DestRectangle.Y = (int)Position.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 
                Rotation, Midpoint, Scale, SpriteEffects.None, 1.0f);
        }
    }
}
