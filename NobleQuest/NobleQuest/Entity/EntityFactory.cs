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
    public class EntityFactory
    {
        public EntityFactory()
        {

        }

        public GameEntity GetPlayerTown(Game game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            TownNode town = new TownNode();            
            town.Texture = game.Content.Load<Texture2D>("PlayerCity");
            town.Position = position;
            town.Velocity = new Vector2(0f, 0f);
            town.Midpoint = new Vector2(town.Texture.Width / 2, town.Texture.Height / 2);
            town.Rotation = 0.0f;
            town.Offset = new Vector2(town.Texture.Width / 2, town.Texture.Height / 2);
            town.SrcRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle = new Rectangle((int)position.X, (int)position.Y, 
                town.SrcRectangle.Width, town.SrcRectangle.Height);
            town.Game = game;
            town.PlayerOwned = true;
            town.EnemyOwned = false;

            // Set Properties in NodeEntity
            town.StructurePresent = true;
            town.Resource = NodeEntity.Resources.WOOD;
            town.LeftPaths = null;
            town.RightPaths = new List<PathEntity>();
            town.PreferredPathEntity = null;

            // Set Properties in TownNode
                       

            return town;
        }

        public GameEntity GetEnemyTown(Game game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            TownNode town = new TownNode();
            town.Texture = game.Content.Load<Texture2D>("EnemyCity");
            town.Position = position;
            town.Velocity = new Vector2(0f, 0f);
            town.Midpoint = new Vector2(town.Texture.Width / 2, town.Texture.Height / 2);
            town.Rotation = 0.0f;
            town.Offset = new Vector2(town.Texture.Width / 2, town.Texture.Height / 2);
            town.SrcRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle = new Rectangle((int)position.X, (int)position.Y,
                town.SrcRectangle.Width, town.SrcRectangle.Height);
            town.Game = game;
            town.PlayerOwned = false;

            // Set Properties in NodeEntity
            town.StructurePresent = true;
            town.Resource = NodeEntity.Resources.WOOD;
            town.LeftPaths = new List<PathEntity>();
            town.RightPaths = null;
            town.PreferredPathEntity = null;

            // Set Properties in TownNode


            return town;
        }

        public GameEntity GetPathEntity(Game game, NodeEntity leftNode, NodeEntity rightNode)
        {
            // Get Distance Between Nodes
            float nodeDistance = Vector2.Distance(leftNode.Position, rightNode.Position);
            double rotation = Math.Atan2(rightNode.Position.Y - leftNode.Position.Y, rightNode.Position.X - leftNode.Position.X);

            // Instantiate and Set Properties in GameEntity
            PathEntity pathEntity = new Entity.PathEntity();
            pathEntity.Texture = game.Content.Load<Texture2D>("Path");
            pathEntity.Position = leftNode.Position;
            pathEntity.Velocity = new Vector2(0f, 0f);
            // pathEntity.Midpoint = new Vector2(pathEntity.Texture.Width / 2, pathEntity.Texture.Height / 2);
            pathEntity.Midpoint = new Vector2(0f, pathEntity.Texture.Height / 2);
            pathEntity.Rotation = (float)rotation;
            pathEntity.Offset = new Vector2(0f, 0f);
            pathEntity.SrcRectangle = new Rectangle(0,0,(int)nodeDistance,pathEntity.Texture.Height);
            pathEntity.DestRectangle = new Rectangle((int)pathEntity.Position.X, (int)pathEntity.Position.Y,
                pathEntity.SrcRectangle.Width, pathEntity.SrcRectangle.Height);
            pathEntity.Game = game;
            pathEntity.PlayerOwned = false;
            pathEntity.EnemyOwned = false;

            // Set Properties in PathEntity
            pathEntity.LeftNode = leftNode;
            pathEntity.RightNode = rightNode;

            // Set Properties in Nodes
            if ( leftNode.RightPaths != null)
            {
                leftNode.RightPaths.Add(pathEntity);
            }
            if ( rightNode.LeftPaths != null)
            {
                rightNode.LeftPaths.Add(pathEntity);
            }          

            return pathEntity;
        }

        public GameEntity GetGrassNode(Game game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            NodeEntity grassNode = new NodeEntity();
            grassNode.Texture = game.Content.Load<Texture2D>("GrassNode");
            grassNode.Position = position;
            grassNode.Velocity = new Vector2(0f, 0f);
            grassNode.Midpoint = new Vector2(grassNode.Texture.Width / 2, grassNode.Texture.Height / 2);
            grassNode.Rotation = 0.0f;
            grassNode.Offset = new Vector2(grassNode.Texture.Width / 2, grassNode.Texture.Height / 2);
            grassNode.SrcRectangle = new Rectangle(0, 0, grassNode.Texture.Width, grassNode.Texture.Height);
            grassNode.DestRectangle = new Rectangle((int)position.X, (int)position.Y,
                grassNode.SrcRectangle.Width, grassNode.SrcRectangle.Height);
            grassNode.Game = game;
            grassNode.PlayerOwned = false;

            // Set Properties in NodeEntity
            grassNode.StructurePresent = true;
            grassNode.Resource = NodeEntity.Resources.HAY;
            grassNode.LeftPaths = new List<PathEntity>();
            grassNode.RightPaths = null;
            grassNode.PreferredPathEntity = null;
            grassNode.FortPresent = false;
            grassNode.Fort = null;

            return grassNode;
        }

        public GameEntity GetInfantryEntity(Game game, bool playerOwned, GameEntity town)
        {
            // Instantiate and Set Properties in GameEntity
            InfantryEntity infantryEntity = new InfantryEntity();
            infantryEntity.Texture = game.Content.Load<Texture2D>("PlayerInfantry");
            infantryEntity.Position = town.Position;
            infantryEntity.Velocity = new Vector2(0f, 0f);
            infantryEntity.Midpoint = new Vector2(infantryEntity.Texture.Width / 2, infantryEntity.Texture.Height / 2);
            infantryEntity.Rotation = 0.0f;
            infantryEntity.Offset = new Vector2(infantryEntity.Texture.Width / 2, infantryEntity.Texture.Height / 2);
            infantryEntity.SrcRectangle = new Rectangle(0, 0, infantryEntity.Texture.Width, infantryEntity.Texture.Height);
            infantryEntity.DestRectangle = new Rectangle((int)town.Position.X, (int)town.Position.Y,
                infantryEntity.SrcRectangle.Width, infantryEntity.SrcRectangle.Height);
            infantryEntity.Game = game;
            infantryEntity.PlayerOwned = playerOwned;
            infantryEntity.EnemyOwned = !playerOwned;

            // Set Properties in Dynamic Entity
            infantryEntity.HitPoints = 100;
            infantryEntity.Location = (NodeEntity)town;
            infantryEntity.Destination = null;
            infantryEntity.Moving = false;

            return infantryEntity;
        }
    }
}
