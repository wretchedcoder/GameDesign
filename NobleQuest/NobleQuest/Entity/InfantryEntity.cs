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
    public class InfantryEntity : Entity.DynamicEntity
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void HandleCollision(TownNode town) 
        { 
            
        }

        public override void HandleCollision(NodeEntity node) 
        { 
            // HandleCollison(TownNode) handles this...
            if (node.isTown)
            {
                return;
            }

            switch (OwnedBy)
            {
                case Owners.PLAYER:
                    if (node.OwnedBy != Owners.PLAYER)
                    {
                        node.OwnedBy = Owners.PLAYER;
                    }
                    break;
                case Owners.ENEMY:
                    if (node.OwnedBy != Owners.ENEMY)
                    {
                        node.OwnedBy = Owners.ENEMY;
                    }
                    break;
                default:
                    break;
            }
        }

        public override void HandleCollision(DynamicEntity dynamic) { }

        public override void Attack(DynamicEntity target) { }
        
    } // End of InfantryEntity Class
}
