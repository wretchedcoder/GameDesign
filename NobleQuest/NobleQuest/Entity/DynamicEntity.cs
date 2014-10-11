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
        public int HitPoints;
        public NodeEntity Location;
        public bool Moving;
        public NodeEntity Destination;
        public int Direction;
        public int LEFT = -1;
        public int RIGHT = 1;
        public bool isHandlingTownCollison = false;
        public bool isHandlingNodeCollison = false;
        public bool isHandlingDynEntityCollison = false;

        public DynamicEntity()
        {
            RandomGenerator = new Random();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!Moving)
            {
                DetermineDestination();
                
                Vector2 newVelocity = new Vector2(Destination.Position.X - Location.Position.X,
                        Destination.Position.Y - Location.Position.Y);
                double rotation = Math.Atan2(Destination.Position.Y - Location.Position.Y,
                    Destination.Position.X - Location.Position.X);

                this.Velocity = Vector2.Multiply(newVelocity, 0.005f); ;
                this.Rotation = (float)rotation;
                this.Moving = true;
            }

            this.Position += this.Velocity;
            this.DestRectangle.X = (int)this.Position.X;
            this.DestRectangle.Y = (int)this.Position.Y; 
        }

        public void DetermineDestination()
        {
            if (Location.PreferredPathEntity == null)
            {
                if (Direction == RIGHT)
                {
                    Destination = GetPath(Location.RightPaths).RightNode;
                }
                else
                {
                    Destination = GetPath(Location.LeftPaths).LeftNode;
                }
            }
            else
            {
                Destination = Location.PreferredPathEntity.RightNode;
            }
        }

        public PathEntity GetPath(List<PathEntity> pathList)
        {
            int count = pathList.Count;
            return pathList[RandomGenerator.Next(count)];
        }

        public virtual void HandleCollision(TownNode town) { }

        public virtual void HandleCollision(NodeEntity node) { }

        public virtual void HandleCollision(DynamicEntity dynamic) { }

        public virtual void Attack(DynamicEntity target) { }
    }
}
