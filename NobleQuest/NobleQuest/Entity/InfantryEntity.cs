﻿using System;
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
                if (this.PlayerOwned
                    && this.Location.RightPaths != null
                    && this.Location.RightPaths.Count != 0
                    && !this.Moving)
                {
                    int numPaths = this.Location.RightPaths.Count;
                    PathEntity Path = this.Location.RightPaths[RandomGenerator.Next(numPaths)];
                    Vector2 newVelocity = new Vector2(Path.RightNode.Position.X - Path.LeftNode.Position.X, 
                        Path.RightNode.Position.Y - Path.LeftNode.Position.Y);
                    double rotation = Math.Atan2(Path.RightNode.Position.Y - Path.LeftNode.Position.Y,
                        Path.RightNode.Position.X - Path.LeftNode.Position.X);

                    this.Velocity = Vector2.Multiply(newVelocity, 0.001f); ;
                    this.Rotation = (float)rotation;                    
                    this.Destination = Path.RightNode;                    
                    this.Moving = true;
                }
                else if (this.EnemyOwned
                    && this.Location.LeftPaths != null
                    && this.Location.LeftPaths.Count != 0
                    && !this.Moving)
                {
                    int numPaths = this.Location.LeftPaths.Count;
                    PathEntity Path = this.Location.LeftPaths[RandomGenerator.Next(numPaths)];
                    Vector2 newVelocity = new Vector2(Path.LeftNode.Position.X - Path.RightNode.Position.X,
                        Path.LeftNode.Position.Y - Path.RightNode.Position.Y);
                    double rotation = Math.Atan2(Path.LeftNode.Position.Y - Path.RightNode.Position.Y,
                        Path.LeftNode.Position.X - Path.RightNode.Position.X);

                    this.Velocity = Vector2.Multiply(newVelocity, 0.001f); ;
                    this.Rotation = (float)rotation;
                    this.Destination = Path.LeftNode;
                    this.Moving = true;
                }  
            }            

            this.Position += this.Velocity;
            this.DestRectangle.X = (int)this.Position.X;
            this.DestRectangle.Y = (int)this.Position.Y;  
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
                    // Attack DynamicEntity
                }
            }
            else
            {
                if (dynamic.PlayerOwned)
                {
                    // Attack DynamicEntity
                }
                else
                {
                    // No Action
                }
            }
        }

        public override void HandleCollision(NodeEntity node)
        {
            if (this.PlayerOwned)
            {
                if (node.PlayerOwned)
                {
                    // Check Wait Flag
                }
                else
                {
                    // Check Fort
                    // Attack Fort if Present
                    // Claim for Player if not
                }
            }
            else 
            {
                if (node.PlayerOwned)
                {
                    // Check Fort
                    // Attack Fort if Present
                    // Claim for Player if not
                }
                else
                {
                    // Check Wait Flag
                }
            }
        }
    } // End of InfantryEntity Class
}
