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
        public enum States { STOPPED, MOVING };
        public States State;

        public enum Directions { LEFT, RIGHT };
        public Directions Direction;

        public NodeEntity Location;
        public NodeEntity Destination;
        public bool IsIgnoringPreferredPath = false;
        public bool IsAtDestination = false;
        public bool CanMoveToNonOwned = true;
        public bool IgnoresOrders = false;
        

        public DynamicEntity()
        {
            State = States.STOPPED;
            Direction = Directions.RIGHT;
        }

        public override void Update(GameTime gameTime)
        {
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
                    if (CanMoveToNonOwned)
                    {
                        this.SetVelocityAndRotation();
                        State = States.MOVING;
                        IsAtDestination = false;
                    }
                    else
                    {
                        if (Destination.OwnedBy == this.OwnedBy)
                        {
                            this.SetVelocityAndRotation();
                            State = States.MOVING;
                            IsAtDestination = false;
                        }
                        else if (!Location.isTown)
                        {
                            ReverseDirection();
                        }
                    }                                      
                    break;
                case States.MOVING:
                    // Update Position, DestRectangle, etc.
                    this.Position += this.Velocity;
                    this.DestRectangle.X = (int)this.Position.X;
                    this.DestRectangle.Y = (int)this.Position.Y;
                    if (ArrivedAtDestination())
                    {
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
                            }
                            else
                            {
                                Destination = Location.PreferredPathEntity.LeftNode;
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
                            }
                            else
                            {
                                Destination = Location.PreferredPathEntity.RightNode;
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

        private void SetVelocityAndRotation()
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
            this.Position = this.Location.Position;
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

        public virtual void HandleCollision(TownNode town) { }

        public virtual void HandleCollision(NodeEntity node) { }

        public virtual void HandleCollision(DynamicEntity dynamic) { }

        public virtual void Attack(DynamicEntity target) { }
    }
}
