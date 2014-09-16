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

        public NodeEntity GetPlayerTown(Game game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            TownNode town = new TownNode();            
            town.Texture = game.Content.Load<Texture2D>("PlayerCity");
            town.Position = position;
            town.Velocity = new Vector2(0f, 0f);
            town.Midpoint = new Vector2(town.Texture.Width / 2.0f, town.Texture.Height / 2.0f);
            town.Rotation = 0.0f;
            town.SrcRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle = new Rectangle((int)position.X, (int)position.Y,
                town.Texture.Width, town.Texture.Height);
            town.Game = game;
            town.PlayerOwned = true;
            town.EnemyOwned = false;

            // Set Properties in NodeEntity
            town.HasResourceStructure = true;
            town.LeftPaths = null;
            town.RightPaths = new List<PathEntity>();
            town.PreferredPathEntity = null;
            town.HasFort = true;

            // Set Properties in TownNode
                       

            return town;
        }

        public NodeEntity GetEnemyTown(Game game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            TownNode town = new TownNode();
            town.Texture = game.Content.Load<Texture2D>("EnemyCity");
            town.Position = position;
            town.Velocity = new Vector2(0f, 0f);
            town.Midpoint = new Vector2(town.Texture.Width / 2, town.Texture.Height / 2);
            town.Rotation = 0.0f;
            town.SrcRectangle = new Rectangle(0, 0, town.Texture.Width, town.Texture.Height);
            town.DestRectangle = new Rectangle((int)position.X, (int)position.Y,
                town.SrcRectangle.Width, town.SrcRectangle.Height);
            town.Game = game;
            town.PlayerOwned = false;

            // Set Properties in NodeEntity
            town.HasResourceStructure = true;
            town.LeftPaths = new List<PathEntity>();
            town.RightPaths = null;
            town.PreferredPathEntity = null;
            town.HasFort = true;

            // Set Properties in TownNode


            return town;
        }

        public PathEntity GetPathEntity(Game game, NodeEntity leftNode, NodeEntity rightNode)
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

        public NodeEntity GetGrassNode(Game game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            NodeEntity grassNode = new NodeEntity();
            grassNode.Texture = game.Content.Load<Texture2D>("GrassNode");
            grassNode.Position = position;
            grassNode.Velocity = new Vector2(0f, 0f);
            grassNode.Midpoint = new Vector2(grassNode.Texture.Width / 2, grassNode.Texture.Height / 2);
            grassNode.Rotation = 0.0f;
            grassNode.SrcRectangle = new Rectangle(0, 0, grassNode.Texture.Width, grassNode.Texture.Height);
            grassNode.DestRectangle = new Rectangle((int)position.X, (int)position.Y,
                grassNode.SrcRectangle.Width, grassNode.SrcRectangle.Height);
            grassNode.Game = game;
            grassNode.PlayerOwned = false;

            // Set Properties in NodeEntity
            grassNode.HasResourceStructure = false;
            grassNode.LeftPaths = new List<PathEntity>();
            grassNode.RightPaths = new List<PathEntity>();
            grassNode.PreferredPathEntity = null;
            grassNode.HasFort = false;
            grassNode.Fort = null;

            return grassNode;
        }

        public DynamicEntity GetInfantryEntity(NobleQuestGame game, bool playerOwned, GameEntity town)
        {
            // Instantiate and Set Properties in GameEntity
            InfantryEntity infantryEntity = new InfantryEntity();
            infantryEntity.Texture = game.Content.Load<Texture2D>("PlayerInfantry");
            infantryEntity.Position = town.Position;
            infantryEntity.Velocity = new Vector2(0f, 0f);
            infantryEntity.Midpoint = new Vector2(infantryEntity.Texture.Width / 2, infantryEntity.Texture.Height / 2);
            infantryEntity.Rotation = 0.0f;
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

            game.DynamicEntityList.Add(infantryEntity);

            return infantryEntity;
        }
    }
}
