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
    class InfantryEntity : Entity.DynamicEntity
    {
        public int attack = 10;

        public override void Update(GameTime gameTime)
        {
            if (HitPoints <= 0)
            {
                base.Game.DynamicEntityList.Remove(this);
                if (PlayerOwned)
                {
                    base.Game.Player.Resources.CurrentPopulation--;
                    base.Game.Player.Resources.Infantry--;
                }
                else
                {
                    base.Game.Enemy.Resources.CurrentPopulation--;
                    base.Game.Enemy.Resources.Infantry--;
                }
            }

            base.Update(gameTime);
        }

        public override void HandleCollision(TownNode town)
        {
            if (PlayerOwned)
            {
                if (town.EnemyOwned == true)
                {
                    this.Game.DynamicEntityList.Remove(this);
                    this.Game.Player.Resources.CurrentPopulation--;
                    this.Game.Player.Resources.Infantry--;
                }
                else
                {
                    Location = town;
                    Velocity = ZERO_VELOCITY;
                    Moving = false;
                }
            }
            else
            {
                if (town.PlayerOwned)
                {
                    this.Game.DynamicEntityList.Remove(this);
                    this.Game.Enemy.Resources.CurrentPopulation--;
                    this.Game.Enemy.Resources.Infantry--;
                }
                else
                {
                    Location = town;
                    Velocity = ZERO_VELOCITY;
                    Moving = false;
                }
            }
        }

        public override void HandleCollision(DynamicEntity dynamic)
        {
            if (this.PlayerOwned)
            {
                if (dynamic.PlayerOwned)
                {
                    // No Action
                }
                else
                {
                    dynamic.HitPoints -= this.attack;
                }
            }
            else
            {
                if (dynamic.PlayerOwned)
                {
                    dynamic.HitPoints -= this.attack;
                }
                else
                {
                    // No Action
                }
            }
        }

        public override void HandleCollision(NodeEntity node)
        {
            if (node.isTown == true)
            {
                return;
            }
            if (this.PlayerOwned)
            {
                if (node.PlayerOwned)
                {
                    // Check Wait Flag
                }
                else if (node.EnemyOwned)
                {
                    // Check Fort
                    // Attack Fort if Present
                    // Claim for Player if not

                    node.PlayerOwned = true;
                    node.EnemyOwned = false; 
                }
                else
                {
                    node.PlayerOwned = true;
                    Location = node;
                    Moving = false;
                }
            }
            else 
            {
                if (node.PlayerOwned)
                {
                    // Check Fort
                    // Attack Fort if Present
                    // Claim for Player if not
                    node.EnemyOwned = true;
                    node.PlayerOwned = false;
                }
                else if (node.EnemyOwned)
                {
                    // Check Wait Flag
                    
                }
                else
                {
                    node.EnemyOwned = true;
                    Location = node;
                    Moving = false;
                }
            }
        }
    } // End of InfantryEntity Class
}
