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

        public GameEntity GetPlayerTown(Game game, Vector2 position)
        {
            // Instantiate and Set Properties in GameEntity
            TownNode town = new TownNode();            
            town.Texture = game.Content.Load<Texture2D>("PlayerCity");
            town.Position = position;
            town.Velocity = new Vector2(0f, 0f);
            town.Midpoint = new Vector2(town.Position.X + town.Texture.Width / 2, 
                town.Position.Y + town.Texture.Height / 2);
            town.Rotation = 0.0f;
            town.Offset = new Vector2(town.Position.X + town.Texture.Width / 2,
                town.Position.Y + town.Texture.Height / 2);
            town.Rectangle = new Rectangle();
            town.Game = game;
            town.PlayerOwned = true;
            town.EnemyOwned = false;

            // Set Properties in NodeEntity
            town.StructurePresent = true;
            town.Resource = NodeEntity.Resources.WOOD;
            town.LeftPaths = null;
            town.RightPaths = new HashSet<PathEntity>();
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
            town.Midpoint = new Vector2(town.Position.X + town.Texture.Width / 2,
                town.Position.Y + town.Texture.Height / 2);
            town.Rotation = 0.0f;
            town.Offset = new Vector2(town.Position.X + town.Texture.Width / 2,
                town.Position.Y + town.Texture.Height / 2);
            town.Rectangle = new Rectangle();
            town.Game = game;
            town.PlayerOwned = false;

            // Set Properties in NodeEntity
            town.StructurePresent = true;
            town.Resource = NodeEntity.Resources.WOOD;
            town.LeftPaths = new HashSet<PathEntity>();
            town.RightPaths = null;
            town.PreferredPathEntity = null;

            // Set Properties in TownNode


            return town;
        }

        public GameEntity GetPathEntity(Game game, NodeEntity leftNode, NodeEntity rightNode)
        {
            // Instantiate and Set Properties in GameEntity
            PathEntity pathEntity = new Entity.PathEntity();
            pathEntity.Texture = game.Content.Load<Texture2D>("Path");
            pathEntity.Position = leftNode.Position;
            pathEntity.Velocity = new Vector2(0f, 0f);
            pathEntity.Midpoint = new Vector2(pathEntity.Texture.Width / 2, pathEntity.Texture.Height / 2);
            pathEntity.Rotation = 0.0f;
            pathEntity.Offset = new Vector2(pathEntity.Texture.Width / 2, pathEntity.Texture.Height / 2);
            pathEntity.Rectangle = new Rectangle();
            pathEntity.Game = game;
            pathEntity.PlayerOwned = false;
            pathEntity.EnemyOwned = false;

            // Set Properties in PathEntity
            pathEntity.LeftNode = leftNode;
            pathEntity.RightNode = leftNode;

            // Set Properties in Nodes
            leftNode.RightPaths.Add(pathEntity);
            rightNode.LeftPaths.Add(pathEntity);

            return pathEntity;
        }
    }
}
