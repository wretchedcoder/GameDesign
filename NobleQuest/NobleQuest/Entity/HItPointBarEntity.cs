using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace NobleQuest.Entity
{
    public class HitPointBarEntity : GameEntity
    {
        public Texture2D Background;
        public Texture2D Foreground;

        public GameEntity AssociatedEntity;
        public int AdjustedWith;

        public Rectangle SrcBackgroundRectangle;
        public Rectangle SrcForegroundRectangle;

        public float WidthRatio = 0.0f;
        public float HeightRatio = 0.0f;

        public Vector2 ForegroundPosition;

        public bool UpdatePosition = true;
        public bool InvertDirection = false;

        public HitPointBarEntity(NobleQuestGame Game)
        {
            this.Game = Game;
            this.Background = this.Game.Content.Load<Texture2D>("HitPointBackground");
            this.Foreground = this.Game.Content.Load<Texture2D>("HitPointBar");
            this.Rotation = 0f;
            this.Midpoint = Vector2.Zero;
            this.ForegroundPosition = Vector2.Zero;
            this.SrcForegroundRectangle = new Rectangle();
        }

        public void InitBar()
        {
            this.Position.X = AssociatedEntity.Position.X - AssociatedEntity.DestRectangle.Width;
            this.Position.Y = AssociatedEntity.Position.Y - AssociatedEntity.Midpoint.Y - this.Background.Height;
        }

        public override void Update(GameTime gameTime)
        {
            float leftHitPoints = (float)this.AssociatedEntity.HitPoint /
                (float)this.AssociatedEntity.HitPointMax;
            AdjustedWith = (int)((float)this.Foreground.Width * leftHitPoints);

            if (UpdatePosition)
            {
                this.Position.X = AssociatedEntity.DestRectangle.X;
                this.Position.Y = AssociatedEntity.DestRectangle.Y - this.Background.Height;
            }
            this.ForegroundPosition.X = this.Position.X + 
                ((this.Background.Width - this.Foreground.Width) * 0.50f);
            this.ForegroundPosition.Y = this.Position.Y +
                ((this.Background.Height - this.Foreground.Height) * 0.50f);
            this.SrcBackgroundRectangle = new Rectangle(0, 0, this.Background.Width, this.Background.Height);
            if (InvertDirection)
            {
                this.ForegroundPosition.X += this.Foreground.Width - this.AdjustedWith;
            }
            else
            {
                this.ForegroundPosition.X = this.Position.X +
                ((this.Background.Width - this.Foreground.Width) * 0.50f);
            }
            this.SrcForegroundRectangle.X = 0;
            this.SrcForegroundRectangle.Y = 0;
            this.SrcForegroundRectangle.Width = this.AdjustedWith;
            this.SrcForegroundRectangle.Height = this.Foreground.Height;           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                // Draw Background
                spriteBatch.Draw(
                    this.Background,
                    this.Position,
                    this.SrcBackgroundRectangle,
                    Color.White,
                    this.Rotation,
                    this.Midpoint,
                    1.0f,
                    SpriteEffects.None,
                    1.0f);

                // Draw Foreground
                spriteBatch.Draw(
                    this.Foreground,
                    this.ForegroundPosition,
                    this.SrcForegroundRectangle,
                    Color.White,
                    this.Rotation,
                    this.Midpoint,
                    1.0f,
                    SpriteEffects.None,
                    1.0f);
            }
        }
    }
}
