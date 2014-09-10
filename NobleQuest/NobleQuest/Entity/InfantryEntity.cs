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
        public override void Update(GameTime gameTime)
        {
            if (this.Location.PreferredPathEntity == null)
            {
                if (this.PlayerOwned)
                {
                    int numPaths = this.Location.RightPaths.Count;
                    PathEntity Path = this.Location.RightPaths[RandomGenerator.Next(numPaths)];
                    Vector2 newVelocity = new Vector2(Path.RightNode.Position.X - Path.LeftNode.Position.X, 
                        Path.RightNode.Position.Y - Path.LeftNode.Position.Y);
                    double rotation = Math.Atan2(Path.RightNode.Position.Y - Path.LeftNode.Position.Y,
                        Path.RightNode.Position.X - Path.LeftNode.Position.X);


                    this.Velocity = Vector2.Multiply(newVelocity, 0.001f); ;
                    this.Rotation = (float)rotation;
                    this.Position += this.Velocity;
                    this.Destination = Path.RightNode;
                    
                }                
            }

            if (this.DestRectangle.Intersects(this.Destination.DestRectangle))
            {

            }
        }
    }
}
