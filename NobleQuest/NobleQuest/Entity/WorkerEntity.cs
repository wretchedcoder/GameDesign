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
        public List<NodeEntity> VisitedNodes;

        public WorkerEntity()
        {
            VisitedNodes = new List<NodeEntity>();
        }

        public override void Update(GameTime gameTime)
        {
            if (HitPoints <= 0)
            {
                base.Game.DynamicEntityList.Remove(this);
                //if (PlayerOwned)
                //{
                //    base.Game.Player.Resources.CurrentPopulation--;
                //    base.Game.Player.Resources.Laborers--;
                //}
                //else
                //{
                //    base.Game.Enemy.Resources.CurrentPopulation--;
                //    base.Game.Enemy.Resources.Laborers--;
                //}
            }

            base.Update(gameTime);
        }

        public override void HandleCollision(TownNode town)
        {
            if (this.PlayerOwned)
            {
                if (town.PlayerOwned)
                { 
                    this.Game.Player.Resources.Gold += (VisitedNodes.Count * (VisitedNodes.Count + 1)) / 2;
                    VisitedNodes.Clear();
                    Velocity = ZERO_VELOCITY;
                    Moving = false;
                    Location = town;
                    Direction = RIGHT;
                }
                else
                {
                    Velocity = ZERO_VELOCITY;
                    Moving = false;
                    Location = town;
                    Direction = LEFT;
                }
            }
            else
            {
                
            }
        }

        public override void HandleCollision(NodeEntity node)
        {
            if (node.isTown)
            {
                return;
            }
            
            if (this.PlayerOwned)
            {
                if (node.PlayerOwned)
                {
                    if (!VisitedNodes.Contains(node) && node != Game.Player.Town)
                    {
                        VisitedNodes.Add(node);
                    }
                }
                else
                {
                    Velocity = ZERO_VELOCITY;
                    Moving = false;
                    Location = node;
                    Direction = LEFT;
                }                
            }
            else
            {
                Velocity = ZERO_VELOCITY;
                Moving = false;
                Location = node;
                Direction = LEFT;
            }
        }

        public override void HandleCollision(DynamicEntity dynamic) 
        { 
            
        }
    }

    
}
