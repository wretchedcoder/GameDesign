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
            this.unitType = UnitType.INFANTRY;
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
                this.DestRectangle.X = (int)(this.Position.X - this.Midpoint.X);
                this.DestRectangle.Y = (int)(this.Position.Y - this.Midpoint.Y);
            }
        }

        

        public override void HandleCollision(TownNode town) 
        {
            if (town.Owner != this.Owner)
            {
                if (this.State != States.ATTACKING)
                {
                    this.State = States.ATTACKING;
                    this.TargetTown = town;
                    town.TargetEntity = this;
                }
            }
        }

        public override void HandleCollision(NodeEntity node) 
        { 
            
        }

        public override void HandleCollision(DynamicEntity dynamic) 
        { 
            if (dynamic.Owner != this.Owner)
            {
                if (this.State != States.ATTACKING)
                {
                    if (this.OldState == States.NONE)
                    {
                        this.OldState = this.State;
                    }                    
                    this.State = States.ATTACKING;
                    this.TargetEntity = dynamic;
                }
                if (dynamic.State != States.ATTACKING)
                {
                    if (dynamic.OldState == States.NONE)
                    {
                        dynamic.OldState = dynamic.State;
                    } 
                    dynamic.State = States.ATTACKING;
                    dynamic.TargetEntity = this;
                }
            }
            else if (this != dynamic)
            {
                if (this.Direction == Directions.RIGHT
                    && dynamic.Position.X > this.Position.X)
                {
                    if (this.State != States.ATTACKING)
                    {
                        PauseDelayTime = PauseDelay;
                        State = States.STOPPED;
                    }
                }
                else if (this.Direction == Directions.LEFT
                    && dynamic.Position.X < this.Position.X)
                {
                    if (this.State != States.ATTACKING)
                    {
                        PauseDelayTime = PauseDelay;
                        State = States.STOPPED;
                    }
                }                
            }
        }

        public override void Attack(DynamicEntity target, GameTime gameTime) 
        {
            if (!target.IsAlive)
            {
                target = null;
                this.State = States.STOPPED;
                return;
            }

            TotalAttackTime += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (TotalAttackTime >= AttackCooldown)
            {
                target.HitPoint -= this.Damage + this.GetModifier();
                TotalAttackTime -= AttackCooldown;
            }            
        }

        public override void Attack(TownNode target, GameTime gameTime)
        {
            TotalAttackTime += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (TotalAttackTime >= AttackCooldown)
            {
                target.HitPoint -= this.Damage + this.GetModifier();
                this.HitPoint -= target.Damage;
                TotalAttackTime -= AttackCooldown;
                target.LastAttackedBy = this;
            }
        }

        public override void RemoveFromGame(GameTime gameTime)
        {
            this.IsAlive = false;
            if (this.Owner == Owners.ENEMY)
            {
                if (TargetEntity == null)
                {
                    this.Game.Enemy.AddPath(UnitType.INFANTRY, this.VisitedPath);
                }
                else
                {
                    this.Game.Enemy.AddPath(TargetEntity.unitType, this.VisitedPath);
                }                
            }
            this.Game.DynamicEntityList.Remove(this);
            this.Location.Occupant = null;
            if (TargetEntity != null)
            {
                TargetEntity.State = States.STOPPED;
                //TargetEntity.OldState = States.NONE;
                TargetEntity.Update(gameTime);
                TargetEntity.TargetEntity = null;
            }
            if (TargetTown != null)
            {
                TargetTown.TargetEntity = null;
            }
        }

        public virtual int GetModifier()
        {
            if (this.TargetEntity == null)
            {
                return 0;
            }
            switch(this.TargetEntity.unitType)
            {
                case UnitType.ARCHER:
                    return 20;
                case UnitType.INFANTRY:
                    return 0;
                case UnitType.KNIGHT:
                    return 0;
                default:
                    return 0;
            }
        }
        
    } // End of InfantryEntity Class
}
