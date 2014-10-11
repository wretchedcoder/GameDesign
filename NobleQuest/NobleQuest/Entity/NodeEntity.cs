using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using NobleQuest.Entity;

namespace NobleQuest.Entity
{
    public class NodeEntity : GameEntity
    {
        public bool HasResourceStructure;
        public bool HasFort;
        public List<PathEntity> LeftPaths;
        public List<PathEntity> RightPaths;
        public List<PathEntity> AllPaths;
        public PathEntity PreferredPathEntity;
        public Fort Fort;
        public bool isTown;
        public bool isSelected;
        public PathSelectionEntity PathSelectionEntity;
      
        public void IncrementPreferredPath()
        {
            if (AllPaths == null)
            {
                AllPaths = new List<PathEntity>();
                if (RightPaths != null)
                {
                    AllPaths.AddRange(RightPaths);
                }
                if (LeftPaths != null)
                {
                    AllPaths.AddRange(LeftPaths);
                }
            }

            if (PreferredPathEntity == null)
            {
                PreferredPathEntity = AllPaths[0];
                PreferredPathEntity.IsPreferredPath = true;
            }
            else
            {
                int index = AllPaths.IndexOf(PreferredPathEntity);
                if (index + 1 < AllPaths.Count)
                {
                    PreferredPathEntity.IsPreferredPath = false;
                    PreferredPathEntity = AllPaths[index + 1];
                    PreferredPathEntity.IsPreferredPath = true;
                }
                else
                {
                    PreferredPathEntity.IsPreferredPath = false;
                    PreferredPathEntity = AllPaths[0];
                    PreferredPathEntity.IsPreferredPath = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (PreferredPathEntity != null)
            {
                PathSelectionEntity = new PathSelectionEntity(this.Game);

                PathSelectionEntity.Position = this.Position;
                PathSelectionEntity.SrcRectangle = new Rectangle(0, 0, PathSelectionEntity.Texture.Width, PathSelectionEntity.Texture.Height);
                PathSelectionEntity.DestRectangle = new Rectangle(
                    (int)PathSelectionEntity.Position.X,
                    (int)PathSelectionEntity.Position.Y,
                    PathSelectionEntity.Texture.Width,
                    PathSelectionEntity.Texture.Height);
                PathSelectionEntity.Midpoint = new Vector2(PathSelectionEntity.Texture.Width / 2, PathSelectionEntity.Texture.Height / 2);

                NodeEntity leftNode = PreferredPathEntity.LeftNode;
                NodeEntity rightNode = PreferredPathEntity.RightNode;
                if (LeftPaths != null &&
                    LeftPaths.Contains(PreferredPathEntity))
                {
                    leftNode = PreferredPathEntity.RightNode;
                    rightNode = PreferredPathEntity.LeftNode;
                }                
                float nodeDistance = Vector2.Distance(leftNode.Position, rightNode.Position);
                double rotation = Math.Atan2(rightNode.Position.Y - leftNode.Position.Y, rightNode.Position.X - leftNode.Position.X);

                PathSelectionEntity.Rotation = (float)rotation - (float)Math.PI * 0.5f;
                            

                PathSelectionEntity.Draw(spriteBatch);
            }
        }
    }
}
