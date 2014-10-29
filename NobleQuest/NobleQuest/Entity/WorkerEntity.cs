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

        public WorkerEntity()
        {
            nodesVisited = new HashSet<NodeEntity>();
            this.CanMoveToNonOwned = false;
            this.IgnoresOrders = true;
            this.IsWorker = true;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }
        public override void HandleCollision(TownNode town) 
        {
            int nodesVisitedCount = nodesVisited.Count;
            nodesVisitedCount = ((nodesVisitedCount) * (nodesVisitedCount + 1)) / 2;

            if (this.OwnedBy == Owners.PLAYER)
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

            if (node.OwnedBy == this.OwnedBy)
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
