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
        public Owners Owner;

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
        public GameEntity OrderEntity;
        public bool IsOccupied;

        public Texture2D PlayerNodeTexture;
        public Texture2D EnemyNodeTexture;
        public Texture2D NeutralNodeTexture;
        public Texture2D OrderTexture;

        public NodeEntity(NobleQuestGame Game)
        {
            this.Game = Game;
            PlayerNodeTexture = this.Game.Content.Load<Texture2D>("PlayerNodeTexture");
            EnemyNodeTexture = this.Game.Content.Load<Texture2D>("EnemyNodeTexture");
            NeutralNodeTexture = this.Game.Content.Load<Texture2D>("NeutralNodeTexture");
            OrderTexture = this.Game.Content.Load<Texture2D>("HaltOrderTexture");
            this.Texture = NeutralNodeTexture;
            IsOccupied = false;
            Order = Orders.NONE;
        }
      
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
            if (PathSelectionEntity != null)
            {
                this.Game.PathSelectionList.Remove(PathSelectionEntity);
                PathSelectionEntity = null;
            }
        }

        public void ToggleWaitOrder()
        {
            if (this.Order == Orders.HALT)
            {
                this.Order = Orders.NONE;
                if (OrderEntity != null)
                {
                    this.Game.OrderList.Remove(OrderEntity);
                    OrderEntity = null;
                }                
            }
            else
            {
                this.Order = Orders.HALT;
            }
        }

        public void ClearPreferred()
        {
            if (PreferredPathEntity != null)
            {
                PreferredPathEntity.IsPreferredPath = false;
                PreferredPathEntity = null;
            }            
            if (PathSelectionEntity != null)
            {
                this.Game.PathSelectionList.Remove(PathSelectionEntity);
                PathSelectionEntity = null;
            }  
            Order = Orders.NONE;
            if (OrderEntity != null)
            {
                this.Game.OrderList.Remove(OrderEntity);
                OrderEntity = null;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.isTown)
            {
                switch (OwnedBy)
                {
                    case Owners.PLAYER:
                        this.Texture = this.PlayerNodeTexture;
                        break;
                    case Owners.ENEMY:
                        this.Texture = this.EnemyNodeTexture;
                        break;
                    case Owners.NEUTRAL:
                        this.Texture = this.NeutralNodeTexture;
                        break;
                    default:
                        break;
                }
            }            

            base.Draw(spriteBatch);

            if (PreferredPathEntity != null
                && PathSelectionEntity == null)
            {
                AddPathSelectionEntity();        
            }

            if (OrderEntity == null 
                && this.Order == Orders.HALT)
            {
                AddOrderEntity();
            }
        }

        public void AddPathSelectionEntity()
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

            this.Game.PathSelectionList.Add(PathSelectionEntity);   
        }

        public void AddOrderEntity()
        {
            OrderEntity = new GameEntity();

            OrderEntity.Texture = OrderTexture;
            OrderEntity.Position = this.Position;
            OrderEntity.Position.X += (0.80f) * this.Texture.Width - this.Midpoint.X;
            OrderEntity.Position.Y -= (0.80f) * this.Texture.Height - this.Midpoint.Y;
            OrderEntity.SrcRectangle = new Rectangle(0, 0, OrderEntity.Texture.Width, OrderEntity.Texture.Height);
            OrderEntity.DestRectangle = new Rectangle(
                (int)OrderEntity.Position.X,
                (int)OrderEntity.Position.Y,
                OrderEntity.Texture.Width,
                OrderEntity.Texture.Height);
            OrderEntity.Midpoint = new Vector2(OrderEntity.Texture.Width / 2, OrderEntity.Texture.Height / 2);

            this.Game.OrderList.Add(OrderEntity);
        }
    } // NodeEntity
}
