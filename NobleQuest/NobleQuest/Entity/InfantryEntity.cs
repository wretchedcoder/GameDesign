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
        public InfantryEntity()
        {
            HitPointMax = 100;
            HitPoint = 100;
            Damage = 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.State == States.ATTACKING)
            {
                this.Velocity = Vector2.Zero;
                this.Attack(TargetEntity);
            }

            if (HitPoint <= 0)
            {
                this.Game.DynamicEntityList.Remove(this);
                TargetEntity.SetVelocityAndRotation();
                TargetEntity.State = States.MOVING;
                TargetEntity.TargetEntity = null;
            }
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
                        node.ClearPreferred();
                    }
                    break;
                case Owners.ENEMY:
                    if (node.OwnedBy != Owners.ENEMY)
                    {
                        node.OwnedBy = Owners.ENEMY;
                        node.ClearPreferred();
                    }
                    break;
                default:
                    break;
            }
        }

        public override void HandleCollision(DynamicEntity dynamic) 
        { 
            if (dynamic.OwnedBy != this.OwnedBy)
            {
                this.State = States.ATTACKING;
                dynamic.State = States.ATTACKING;
                TargetEntity = dynamic;
                TargetEntity.TargetEntity = dynamic;
            }
        }

        public override void Attack(DynamicEntity target) 
        {
            target.HitPoint -= this.Damage;
        }
        
    } // End of InfantryEntity Class
}
