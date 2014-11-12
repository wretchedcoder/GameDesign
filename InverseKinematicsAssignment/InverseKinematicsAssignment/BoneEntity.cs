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
    public class BoneEntity : BasicEntity
    {
        public BoneEntity ParentBone;
        public BoneEntity ChildBone;

        public EffectorEntity EffectorEntity;
        public float Length;

        public BoneEntity(InverseKinematicGame Game)
        {
            this.Game = Game;
            this.Texture = this.Game.Content.Load<Texture2D>("arm");
            this.Scale = 0.20f;
            this.Midpoint = new Vector2(this.Texture.Height / 2.0f,
                this.Texture.Height / 2.0f);
            //this.Midpoint = Vector2.Multiply(this.Midpoint, this.Scale);

            this.EffectorEntity = new EffectorEntity(Game, this);
            this.Length = this.Texture.Width - this.Midpoint.X;
            this.Length *= this.Scale;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateRotation(gameTime);
            UpdatePosition(gameTime);

            this.DestRectangle.X = (int)this.Position.X;
            this.DestRectangle.Y = (int)this.Position.Y;
            this.DestRectangle.Width = (int)(this.Texture.Width * this.Scale);
            this.DestRectangle.Height = (int)(this.Texture.Height * this.Scale);            
        } // public override void Update

        public void UpdateRotation(GameTime gameTime)
        {
            Vector2 Target = this.Game.MousePosition;
            Vector2 Effector = Vector2.Zero;
            if (this.ChildBone != null)
            {
                Effector = this.ChildBone.EffectorEntity.Position;
            }
            else
            {
                Effector = this.EffectorEntity.Position;
            }

            if (Vector2.Distance(this.Game.CurrentLeaf.EffectorEntity.Position,
                    this.Game.MousePosition) > 10.0f
                && this.Game.iterationCount < this.Game.MAX_ITERATIONS)
            {
                this.Game.iterationCount++;
                float jointToTargetRot = (float)Math.Atan2(
                    Target.Y - this.Position.Y,
                    Target.X - this.Position.X);
                float jointToEffectorRot = (float)Math.Atan2(
                    Effector.Y - this.Position.Y,
                    Effector.X - this.Position.X);
                this.Rotation += jointToTargetRot - jointToEffectorRot;
                this.EffectorEntity.Update(gameTime);

                if (this.ParentBone != null)
                {
                    this.ParentBone.UpdateRotation(gameTime);
                }
                this.UpdatePosition(gameTime);
            }                      
        }

        public void UpdatePosition(GameTime gameTime)
        {            
             if (this.ParentBone != null)
             {
                 this.Position = this.ParentBone.EffectorEntity.Position;
             }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            this.EffectorEntity.Draw(spriteBatch);
        }

        public bool IsRoot()
        {
            if (this.ParentBone == null)
            {
                return true;
            }
            return false;
        }

        public bool IsLeaf()
        {
            if (this.ChildBone == null)
            {
                return true;
            }
            return false;
        }
    }
}
