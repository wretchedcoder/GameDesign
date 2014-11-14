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
        

        public InfantryEntity(NobleQuestGame Game) : base(Game)
        {
            HitPointMax = 100;
            HitPoint = 100;
            Damage = 20;
            this.Game = Game;            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.State == States.ATTACKING)
            {
                this.Velocity = Vector2.Zero;
                if (TargetEntity != null)
                {
                    this.Attack(TargetEntity, gameTime);
                }
                else if (TargetTown != null)
                {
                    this.Attack(TargetTown, gameTime);
                }
            }
        }

        

        public override void HandleCollision(TownNode town) 
        {
            if (town.Owner != this.Owner)
            {
                this.State = States.ATTACKING;
                this.TargetTown = town;
                town.TargetEntity = this;
            }
        }

        public override void HandleCollision(NodeEntity node) 
        { 
            // HandleCollison(TownNode) handles this...
            if (node.isTown)
            {
                return;
            }

            if (node.Occupant != null
                && node.Occupant.Owner != this.Owner)
            {
                return;
            }

            switch (Owner)
            {
                case Owners.PLAYER:
                    if (node.Owner != Owners.PLAYER)
                    {
                        node.ClearPreferred();
                        node.Owner = Owners.PLAYER;                        
                    }
                    break;
                case Owners.ENEMY:
                    if (node.Owner != Owners.ENEMY)
                    {
                        node.ClearPreferred();
                        node.Owner = Owners.ENEMY;
                    }
                    break;
                default:
                    break;
            }
        }

        public override void HandleCollision(DynamicEntity dynamic) 
        { 
            if (dynamic.Owner != this.Owner)
            {
                this.State = States.ATTACKING;
                this.TargetEntity = dynamic;

                dynamic.State = States.ATTACKING;
                dynamic.TargetEntity = this;
            }
        }

        public override void Attack(DynamicEntity target, GameTime gameTime) 
        {
            TotalAttackTime += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (TotalAttackTime >= AttackCooldown)
            {
                target.HitPoint -= this.Damage;
                TotalAttackTime -= AttackCooldown;
            }            
        }

        public override void Attack(TownNode target, GameTime gameTime)
        {
            TotalAttackTime += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (TotalAttackTime >= AttackCooldown)
            {
                target.HitPoint -= this.Damage;
                TotalAttackTime -= AttackCooldown;
            }
        }

        public override void RemoveFromGame()
        {
            this.Game.DynamicEntityList.Remove(this);
            this.Location.Occupant = null;
            if (TargetEntity != null)
            {
                TargetEntity.SetVelocityAndRotation();
                TargetEntity.State = States.MOVING;
                TargetEntity.TargetEntity = null;
            }
            if (TargetTown != null)
            {
                TargetTown.TargetEntity = null;
            }
        }
        
    } // End of InfantryEntity Class
}
