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
    public class Path
    {
        BezierCurvesGame Game;
        Texture2D Texture;
        Vector2 Position;
        Vector2 Velocity;
        Rectangle DestRect;
        Vector2 Midpoint;

        public Path(BezierCurvesGame game)
        {
            Game = game;
            Texture = Game.Content.Load<Texture2D>("Path");
            DestRect = new Rectangle();
            Position = new Vector2();
            Velocity = new Vector2();
            Midpoint = new Vector2();
        }

        public void Update(int r, GameTime gameTime)
        {
            DestRect.X = (int)Position.X;
            DestRect.Y = (int)Position.Y;
            DestRect.Width = Texture.Width;
            DestRect.Height = Texture.Height;
            Midpoint.X = (Texture.Width / 2);
            Midpoint.Y = (Texture.Height / 2);

            Position.X = 0.0f;
            Position.Y = 0.0f;
            int pathCount = Game.PathList.Count;
            float t = (float) r / (float) pathCount;
            float u = 1.0f - t;
            for (int i = 0; i < Game.PointList.Count; i++)
            {
                int p = Game.PointList.Count - 1 - i;
                int max = Math.Max(p, i);
                if (max == Game.PointList.Count - 1)
                {
                    max = 1;
                }
                Position.X += (float)(Game.PointList[i].Position.X * max * (Math.Pow(u, p) * (Math.Pow(t, i))));
                Position.Y += (float)(Game.PointList[i].Position.Y * max * (Math.Pow(u, p) * (Math.Pow(t, i))));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Midpoint, 1.0f, SpriteEffects.None, 1.0f);
        }
    }

    
}
