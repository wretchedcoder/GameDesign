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
    public class DynamicEntity : GameEntity
    {
        public static int DIMENSION = 32;
        
        public enum States { STOPPED, MOVING, ATTACKING };
        public States State;

        public enum Directions { LEFT, RIGHT };
        public Directions Direction;

        public NodeEntity Location;
        public NodeEntity Destination;
        public bool IsIgnoringPreferredPath = false;
        public bool IsAtDestination = false;
        public bool CanMoveToNonOwned = true;
        public bool IgnoresOrders = false;
        public bool IsWorker = false;

        public float TotalAttackTime = 0.0f;
        public float AttackCooldown = 1.0f;

        public HitPointBarEntity HitBar;

        public float TotalTimePassed = 0f;
        public float FrameRate = 1.0f / 24.0f;
        public int CurrentFrame = 0;
        public int FRAME_MAX = 20;

        public DynamicEntity()
        { }

        public DynamicEntity(NobleQuestGame Game)
        {
            this.Game = Game;
            State = States.STOPPED;
            Direction = Directions.RIGHT;

            HitPointMax = 0;
            HitPoint = 0;
            Damage = 0;

            HitBar = new HitPointBarEntity(this.Game);
            HitBar.AssociatedEntity = this;
        }

        public override void Update(GameTime gameTime)
        {
            this.HitBar.Update(gameTime);

            // Animation Logic
            TotalTimePassed += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (TotalTimePassed > FrameRate)
            {
                TotalTimePassed -= FrameRate;
                if ( CurrentFrame >= 19)
                {
                    CurrentFrame = -1;
                }
                CurrentFrame++;
            }

            SrcRectangle.X = CurrentFrame * DIMENSION;
            SrcRectangle.Y = (int)State * DIMENSION;

            if (this.HitPoint <= 0)
            {
                this.RemoveFromGame();
            }

            if (this.Location.Order == Orders.HALT
                && !this.IgnoresOrders)
            {
                return;
            }

            switch (State)
            {
                case States.STOPPED:
                    // Determine Destination
                    this.DetermineDestination();
                    if (this.Destination.Occupant != null
                        && this.Destination.Occupant.Owner == this.Owner)
                    {
                        return;
                    }
                    if (CanMoveToNonOwned)
                    {
                        this.SetVelocityAndRotation();
                        State = States.MOVING;
                        IsAtDestination = false;
                        Location.Occupant = null;
                    }
                    else if (!CanMoveToNonOwned)
                    {
                        if (Destination.Owner == this.Owner)
                        {
                            this.SetVelocityAndRotation();
                            State = States.MOVING;
                            IsAtDestination = false;
                            Location.Occupant = null;
                        }
                        else if (!Location.isTown)
                        {
                            ReverseDirection();
                        }
                    }                                      
                    break;
                case States.MOVING:
                    // Update Position, DestRectangle, etc.
                    if (Destination.Occupant != null
                        && Destination.Occupant.Owner == this.Owner)
                    {
                        StopEntity();
                    }
                    this.Position += this.Velocity;
                    this.DestRectangle.X = (int)(this.Position.X - this.Midpoint.X);
                    this.DestRectangle.Y = (int)(this.Position.Y - this.Midpoint.Y);
                    if (ArrivedAtDestination())
                    {
                        Destination.Occupant = this;
                        StopEntity();                        
                    }
                    break;
                default:
                    // If a state is not in base class (e.g. attack),
                    // then it will be handled in the subclass Update()
                    break;
            }
        }

        public void DetermineDestination()
        {
            // Check for Preferred Path Entity
            if (Location.PreferredPathEntity != null
                && Location.Owner == this.Owner
                && !IsIgnoringPreferredPath)
            {
                switch (Direction)
                {
                    case Directions.LEFT:
                        if (Location.LeftPaths != null)
                        {
                            if (Destination == Location.PreferredPathEntity.LeftNode)
                            {
                                Direction = Directions.RIGHT;
                                Destination = Location.PreferredPathEntity.RightNode;
                                if (Location == Destination)
                                {
                                    Direction = Directions.LEFT;
                                    Destination = Location.PreferredPathEntity.LeftNode;
                                }
                            }
                            else
                            {
                                Direction = Directions.LEFT;
                                Destination = Location.PreferredPathEntity.LeftNode;
                                if (Location == Destination)
                                {
                                    Direction = Directions.RIGHT;
                                    Destination = Location.PreferredPathEntity.RightNode;
                                }
                            }
                            
                        }
                        else
                        {
                            Direction = Directions.RIGHT;
                            Destination = Location.PreferredPathEntity.RightNode;
                        }                        
                        return;
                    case Directions.RIGHT:
                        if (Location.RightPaths != null)
                        {
                            if (Destination == Location.PreferredPathEntity.RightNode)
                            {
                                Direction = Directions.LEFT;
                                Destination = Location.PreferredPathEntity.LeftNode;
                                if (Location == Destination)
                                {
                                    Direction = Directions.RIGHT;
                                    Destination = Location.PreferredPathEntity.RightNode;
                                }
                            }
                            else
                            {
                                Direction = Directions.RIGHT;
                                Destination = Location.PreferredPathEntity.RightNode;
                                if (Location == Destination)
                                {
                                    Direction = Directions.LEFT;
                                    Destination = Location.PreferredPathEntity.LeftNode;
                                }
                            }     
                        }
                        else
                        {
                            Direction = Directions.LEFT;
                            Destination = Location.PreferredPathEntity.LeftNode;
                        }           
                        return;
                    default:
                        break;
                }  
            }

            // Get Random Path
            switch (Direction)
            {
                case Directions.LEFT:
                    if (Location.LeftPaths != null)
                    {
                        Destination = GetPath(Location.LeftPaths).LeftNode;
                    }
                    else
                    {
                        Direction = Directions.RIGHT;
                        Destination = GetPath(Location.RightPaths).RightNode;
                    }
                    break;
                case Directions.RIGHT:
                    if (Location.RightPaths != null)
                    {
                        Destination = GetPath(Location.RightPaths).RightNode;
                    }
                    else
                    {
                        Direction = Directions.LEFT;
                        Destination = GetPath(Location.LeftPaths).LeftNode;
                    }
                    break;
                default:
                    break;
            }
        }// DetermineDestination

        public void SetVelocityAndRotation()
        {
            float xDelta = Location.Position.X - Destination.Position.X;
            float yDelta = Location.Position.Y - Destination.Position.Y;
            switch (Direction)
            {
                case Directions.LEFT:
                    xDelta *= -1;
                    yDelta *= -1;
                    break;
                case Directions.RIGHT:
                    xDelta *= -1;
                    yDelta *= -1;
                    break;
                default:
                    break;
            }
            this.Velocity = new Vector2(xDelta * 0.01f, yDelta * 0.01f);
            this.Rotation = (float)(Math.Atan2(yDelta, xDelta));
        }

        public PathEntity GetPath(List<PathEntity> pathList)
        {
            int count = pathList.Count;
            return pathList[RandomGenerator.Next(count)];
        }

        public bool ArrivedAtDestination()
        {
            switch (Direction)
            {
                case Directions.LEFT:
                    if (this.Position.X <= this.Destination.Position.X)
                    {
                        return true;
                    }
                    return false;
                case Directions.RIGHT:
                    if (this.Position.X >= this.Destination.Position.X)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        public void StopEntity()
        {
            State = States.STOPPED;
            this.Location = this.Destination;
            //this.Position = this.Location.Position;
            this.Destination = null;
            IsAtDestination = true;
        }

        public void ReverseDirection()
        {
            if (Direction == Directions.RIGHT)
            {
                Direction = Directions.LEFT;
            }
            else
            {
                Direction = Directions.RIGHT;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            this.HitBar.Draw(spriteBatch);

            if (HitPointMax > 0)
            {
                return;
            }
        }

        public virtual void HandleCollision(TownNode town) { }

        public virtual void HandleCollision(NodeEntity node) { }

        public virtual void HandleCollision(DynamicEntity dynamic) { }

        public virtual void Attack(DynamicEntity target, GameTime gameTime) { }

        public virtual void Attack(TownNode target, GameTime gameTime) { }

        public virtual void RemoveFromGame() { }
    }
}
