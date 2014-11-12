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
    public class EffectorEntity : BasicEntity
    {
        public BoneEntity BoneEntity;

        public EffectorEntity(InverseKinematicGame Game, BoneEntity BoneEntity)
        {
            this.Game = Game;
            this.Texture = this.Game.Content.Load<Texture2D>("effector");
            this.BoneEntity = BoneEntity;
        }

        public override void Update(GameTime gameTime)
        {
            this.Position.X = (BoneEntity.Position.X) + 
                (float)Math.Cos(BoneEntity.Rotation) * BoneEntity.Length;

            this.Position.Y = (BoneEntity.Position.Y) + 
                (float)Math.Sin(BoneEntity.Rotation) * BoneEntity.Length;

            this.Midpoint = Vector2.Zero;
            this.Midpoint.X = this.Texture.Width / 2.0f;
            this.Midpoint.Y = this.Texture.Height / 2.0f;
        }
    }
}
