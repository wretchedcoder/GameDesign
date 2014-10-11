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
    public class SelectionEntity : GameEntity
    {
        public NodeEntity SelectedNode;

        public SelectionEntity(NobleQuestGame Game)
        {
            this.Game = Game;
            this.Texture = this.Game.Content.Load<Texture2D>("SelectionArrow");
        }

        public override void Update(GameTime gameTime)
        {
            if (SelectedNode != null)
            {
                IsVisible = true;                
            }
            else
            {
                IsVisible = false;
            }
        }

        public void setSelectedNode(NodeEntity NewSelectedNode)
        {
            SelectedNode = NewSelectedNode;
            Position = SelectedNode.Position;
            Position.Y -= SelectedNode.Texture.Height / 3;
            Velocity = ZERO_VELOCITY;
            Midpoint = new Vector2(Texture.Width / 2, Texture.Height);
            Rotation = 0.0f;
            Offset = new Vector2(0.0f, 0.0f);
            SrcRectangle = new Rectangle(0, 0,
                Texture.Width, Texture.Height);
            DestRectangle = SelectedNode.DestRectangle;
        }
    }
}
