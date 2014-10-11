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
    public class PathEntity : GameEntity
    {
        public NodeEntity LeftNode { get; set; }
        public NodeEntity RightNode { get; set; }
        public bool IsPreferredPath = false;
        public PathSelectionEntity PathSelectionEntity;

        public PathEntity()
        {

        }

        public PathEntity(NodeEntity LeftNode, NodeEntity RightNode)
        {
            this.LeftNode = LeftNode;
            this.RightNode = RightNode;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (IsPreferredPath && IsVisible)
            {
                PathSelectionEntity = new PathSelectionEntity(this.Game);
                PathSelectionEntity.IsVisible = true;
                PathSelectionEntity.Position = this.Position;
                PathSelectionEntity.Velocity = ZERO_VELOCITY;
                PathSelectionEntity.Midpoint = new Vector2(Texture.Width / 2, Texture.Height / 2);
                PathSelectionEntity.Rotation = this.Rotation;
                PathSelectionEntity.Offset = new Vector2(0.0f, 0.0f);
                PathSelectionEntity.SrcRectangle = new Rectangle(0, 0,
                    PathSelectionEntity.Texture.Width, PathSelectionEntity.Texture.Height);
                PathSelectionEntity.DestRectangle = this.DestRectangle;
                PathSelectionEntity.Draw(spriteBatch);
            }
            else
            {
                PathSelectionEntity = null;
            }
        }
        
    }
}
