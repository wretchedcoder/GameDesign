using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace BezierCurvesAssignment
{
    public class ControlPoint
    {
        public BezierCurvesGame Game;
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Midpoint;
        public Vector2 Velocity;
        public Rectangle DestRect;
        public Rectangle SrcRect;
        public Vector2 TextPosition;
        public bool isDragged;
        

        public ControlPoint(BezierCurvesGame game)
        {
            Game = game;
            Texture = Game.Content.Load<Texture2D>("Point");
            // Position = new Vector2();
            Midpoint = new Vector2();
            Velocity = new Vector2();
            DestRect = new Rectangle();
        }

        public void Update(GameTime gameTime)
        {
            if (isDragged)
            {
                Position.X = Mouse.GetState().X;
                Position.Y = Mouse.GetState().Y;
            }
            DestRect.X = (int)Position.X;
            DestRect.Y = (int)Position.Y;
            DestRect.Width = Texture.Width;
            DestRect.Height = Texture.Height;
            Midpoint.X = (Texture.Width / 2);
            Midpoint.Y = (Texture.Height / 2);
            SrcRect = DestRect;
            TextPosition.X = Position.X;
            TextPosition.Y = Position.Y + Texture.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0.0f,
                Midpoint,
                1.0f,
                SpriteEffects.None,
                1.0f);

            string text = "[ " + Position.X + " , " + Position.Y + " ]";
            
            spriteBatch.DrawString(Game.GameFont, text, TextPosition, Color.Green);
        }
    }
}
