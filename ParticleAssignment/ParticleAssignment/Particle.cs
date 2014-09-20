using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace ParticleAssignment
{
    public class Particle
    {
        public ParticleGame Game;
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public Vector2 Midpoint;
        public Rectangle DestRectangle;
        public Rectangle SrcRectangle;
        public Vector2 Scale;
        public bool IsAlive;
        public int Age;
        public Color color;
        public int Floor;
        

        public Particle(ParticleGame Game)
        {
            this.Game = Game;
            this.Texture = Game.Content.Load<Texture2D>("fire_particle");
            this.Age = 255;
            this.color = new Color(0, this.Age/2, this.Age, this.Age);
            this.IsAlive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (this.Age <= 0)
            {
                IsAlive = false;
            }
            this.Age--;

            color.A = (byte)(this.Age);
            color.R = (byte)(256 - this.Age);
            color.B = (byte)(this.Age);
            color.G = (byte)(this.Age);

            if (Position.Y + Velocity.Y >= Floor)
            {
                // Use the -1 to make the particle go in another direction
                // and the 0.5 to slow it down
                this.Velocity.Y = (-1) * this.Velocity.Y * 0.5f;
            }
            this.Velocity.Y += 0.1f;

            this.Position += this.Velocity;

            this.Scale.X = this.Velocity.Length() * 0.1f;

            this.Rotation = (float)Math.Atan2(this.Velocity.Y, this.Velocity.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, color, Rotation, Midpoint, Scale, SpriteEffects.None, 1.0f);

            
        }
    }
}
