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

        public ResourceEntity GetResourceEntity(NobleQuestGame game, Vector2 position, bool visible)
        {
            // Instantiate and Set Properties in GameEntity
            ResourceEntity resource = new ResourceEntity();
            resource.Texture = game.Content.Load<Texture2D>("ResourceBar");
            resource.Position = position;
            resource.Velocity = new Vector2(0f, 0f);
            resource.Midpoint = new Vector2(0f, 0f);
            resource.Rotation = 0.0f;
            resource.SrcRectangle = new Rectangle(0, 0, resource.Texture.Width, resource.Texture.Height);
            resource.DestRectangle = new Rectangle((int)position.X, (int)position.Y,
                resource.Texture.Width, resource.Texture.Height);
            resource.Game = game;
            resource.PlayerOwned = true;
            resource.EnemyOwned = false;

            // Set Properties in ResourceEntity
            resource.IsVisible = visible;
            resource.SpriteFont = game.SpriteFont;
            resource.Gold = 200;
            resource.Laborers = 1;
            resource.PopulationLimit = 1;
            resource.CurrentPopulation = 1;
            resource.HasBlacksmith = false;           

            return resource;
        }

        public NodeEntity GetPlayerTown(NobleQuestGame game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            TownNode town = new TownNode(game, Owners.PLAYER);            
            town.Texture = game.Content.Load<Texture2D>("PlayerCity");
            town.Position = position;
            town.Velocity = new Vector2(0f, 0f);
            town.Midpoint = new Vector2(town.Texture.Width / 2.0f, town.Texture.Height / 2.0f);
            town.Rotation = 0.0f;
            town.SrcRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle.X = (int)(town.Position.X - town.Midpoint.X);
            town.DestRectangle.Y = (int)(town.Position.Y - town.Midpoint.Y);
            town.Owner = Owners.PLAYER;
            

            // Set Properties in NodeEntity
            town.HasResourceStructure = true;
            town.LeftPaths = null;
            town.RightPaths = new List<PathEntity>();
            town.PreferredPathEntity = null;
            town.HasFort = true;
            town.isTown = true;

            // Set Properties in TownNode

                       

            return town;
        }

        public NodeEntity GetEnemyTown(NobleQuestGame game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            TownNode town = new TownNode(game, Owners.ENEMY);
            town.Texture = game.Content.Load<Texture2D>("EnemyCity");
            town.Position = position;
            town.Velocity = new Vector2(0f, 0f);
            town.Midpoint = new Vector2(town.Texture.Width / 2, town.Texture.Height / 2);
            town.Rotation = 0.0f;
            town.SrcRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle.X = (int)(town.Position.X - town.Midpoint.X);
            town.DestRectangle.Y = (int)(town.Position.Y - town.Midpoint.Y);
            town.Owner = Owners.ENEMY;

            // Set Properties in NodeEntity
            town.HasResourceStructure = true;
            town.LeftPaths = new List<PathEntity>();
            town.RightPaths = null;
            town.PreferredPathEntity = null;
            town.HasFort = true;
            town.isTown = true;

            // Set Properties in TownNode


            return town;
        }

        public PathEntity GetPathEntity(NobleQuestGame game, NodeEntity leftNode, NodeEntity rightNode)
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
            pathEntity.SrcRectangle = new Rectangle(0,0,(int)nodeDistance,pathEntity.Texture.Height);
            pathEntity.DestRectangle = new Rectangle((int)pathEntity.Position.X, (int)pathEntity.Position.Y,
                (int)(Math.Abs(rightNode.Position.X - leftNode.Position.X)), 
                (int)(Math.Abs(rightNode.Position.Y - leftNode.Position.Y)));
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

        public NodeEntity GetNode(NobleQuestGame game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            NodeEntity grassNode = new NodeEntity(game);
            grassNode.Position = position;
            grassNode.Velocity = new Vector2(0f, 0f);
            grassNode.Midpoint = new Vector2(grassNode.Texture.Width / 2, grassNode.Texture.Height / 2);
            //grassNode.Midpoint = new Vector2(0f, 0f);
            grassNode.Rotation = 0.0f;
            grassNode.SrcRectangle = new Rectangle(0, 0, grassNode.Texture.Width, grassNode.Texture.Height);
            grassNode.DestRectangle = new Rectangle(0, 0, grassNode.Texture.Width, grassNode.Texture.Height);
            grassNode.DestRectangle.X = (int)(grassNode.Position.X - grassNode.Midpoint.X);
            grassNode.DestRectangle.Y = (int)(grassNode.Position.Y - grassNode.Midpoint.Y);
            grassNode.Owner = Owners.NEUTRAL;

            // Set Properties in NodeEntity
            grassNode.HasResourceStructure = false;
            grassNode.LeftPaths = new List<PathEntity>();
            grassNode.RightPaths = new List<PathEntity>();
            grassNode.PreferredPathEntity = null;
            grassNode.HasFort = false;
            grassNode.Fort = null;
            grassNode.isTown = false;

            return grassNode;
        }

        public DynamicEntity GetInfantryEntity(NobleQuestGame game, Owners OwnedBy, GameEntity town)
        {
            // Instantiate and Set Properties in GameEntity
            InfantryEntity infantryEntity = new InfantryEntity(game);

            switch (OwnedBy)
            {
                case Owners.PLAYER:
                    infantryEntity.Texture = game.Content.Load<Texture2D>("PlayerInfantry");
                    infantryEntity.Direction = DynamicEntity.Directions.RIGHT;
                    break;
                case Owners.ENEMY:
                    infantryEntity.Texture = game.Content.Load<Texture2D>("EnemyInfantry");
                    infantryEntity.Direction = DynamicEntity.Directions.LEFT;
                    break;
                default:
                    break;
            }

            infantryEntity.Position = town.Position;
            infantryEntity.Velocity = new Vector2(0f, 0f);
            infantryEntity.Midpoint = new Vector2(infantryEntity.Texture.Width / 2, infantryEntity.Texture.Height / 2);
            infantryEntity.Rotation = 0.0f;
            infantryEntity.SrcRectangle = new Rectangle(0, 0, infantryEntity.Texture.Width, infantryEntity.Texture.Height);
            infantryEntity.DestRectangle = new Rectangle((int)town.Position.X, (int)town.Position.Y,
                infantryEntity.SrcRectangle.Width, infantryEntity.SrcRectangle.Height);
            infantryEntity.Game = game;
            infantryEntity.Owner = OwnedBy;
            infantryEntity.State = DynamicEntity.States.STOPPED;

            // Set Properties in Dynamic Entity
            infantryEntity.Location = (NodeEntity)town;

            if (town.TargetEntity != null)
            {
                infantryEntity.State = DynamicEntity.States.ATTACKING;
                infantryEntity.TargetEntity = town.TargetEntity;
                infantryEntity.Destination = town.TargetEntity.Location;

                town.TargetEntity.State = DynamicEntity.States.ATTACKING;
                town.TargetEntity.TargetEntity = infantryEntity;                
            }            

            game.DynamicEntityList.Add(infantryEntity);

            return infantryEntity;
        } // Get Infantry Entity

        public WorkerEntity GetWorkerEntity(NobleQuestGame game, bool playerOwned, TownNode town)
        {
            // Instantiate and Set Properties in GameEntity
            WorkerEntity workerEntity = new WorkerEntity(game);
            workerEntity.Texture = game.Content.Load<Texture2D>("Worker");
            workerEntity.Position = town.Position;
            workerEntity.Velocity = new Vector2(0f, 0f);
            workerEntity.Midpoint = new Vector2(workerEntity.Texture.Width / 2, workerEntity.Texture.Height / 2);
            workerEntity.Rotation = 0.0f;
            workerEntity.SrcRectangle = new Rectangle(0, 0, workerEntity.Texture.Width, workerEntity.Texture.Height);
            workerEntity.DestRectangle = new Rectangle((int)town.Position.X, (int)town.Position.Y,
                workerEntity.SrcRectangle.Width, workerEntity.SrcRectangle.Height);
            workerEntity.Game = game;
            workerEntity.PlayerOwned = playerOwned;
            workerEntity.EnemyOwned = !playerOwned;

            // Set Properties in Dynamic Entity
            // workerEntity.HitPoints = 100;
            workerEntity.Location = (NodeEntity)town;
            // workerEntity.Destination = null;
            // workerEntity.Moving = false;
            if (playerOwned)
            {
                workerEntity.Direction = DynamicEntity.Directions.RIGHT;
            }
            else
            {
                workerEntity.Direction = DynamicEntity.Directions.LEFT;
            }

            game.DynamicEntityList.Add(workerEntity);

            return workerEntity;
        }

        
    }// EntityFactory
}
