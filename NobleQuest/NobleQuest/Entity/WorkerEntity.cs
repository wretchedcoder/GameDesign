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
    public class WorkerEntity : DynamicEntity
    {
        public HashSet<NodeEntity> nodesVisited;

        public WorkerEntity(NobleQuestGame Game) : base(Game)
        {
            HitPointMax = 100;
            HitPoint = 100;
            Damage = 5;

            nodesVisited = new HashSet<NodeEntity>();
            this.CanMoveToNonOwned = false;
            this.IgnoresOrders = true;
            this.IsWorker = true;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Location.Owner != this.Owner
                || ((this.State == States.MOVING) 
                   && (this.Destination.Owner != this.Owner)))
            {
                this.Game.DynamicEntityList.Remove(this);
            }

            if (this.State == States.ATTACKING)
            {
                this.Velocity = Vector2.Zero;
                this.Attack(TargetEntity, gameTime);
            }

            if (HitPoint <= 0)
            {
                this.Game.DynamicEntityList.Remove(this);
                if (TargetEntity != null)
                {
                    TargetEntity.SetVelocityAndRotation();
                    TargetEntity.State = States.MOVING;
                    TargetEntity.TargetEntity = null;
                }
            }
        }
        public override void HandleCollision(TownNode town) 
        {
            int nodesVisitedCount = nodesVisited.Count;
            nodesVisitedCount = ((nodesVisitedCount) * (nodesVisitedCount + 1)) / 2;

            if (this.Owner == Owners.PLAYER)
            {
                this.Game.Player.Resources.Gold += nodesVisitedCount;
            }
            else
            {
                this.Game.Enemy.Resources.Gold += nodesVisitedCount;
            }
            nodesVisited.Clear();
        }

        public override void HandleCollision(NodeEntity node) 
        { 
            if (node.isTown)
            {
                return;
            }

            if (node.Owner == this.Owner)
            {
                nodesVisited.Add(node);
            }
            else
            {
                StopEntity();
                ReverseDirection();
            }
        }

        public virtual void HandleCollision(DynamicEntity dynamic) { }

        public virtual void Attack(DynamicEntity target) { }
    }

    
}
