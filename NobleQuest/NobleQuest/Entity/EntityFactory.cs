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
            town.LeftPaths = new HashSet<PathEntity>();
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
            grassNode.LeftPaths = new HashSet<PathEntity>();
            grassNode.RightPaths = null;
            grassNode.PreferredPathEntity = null;
            grassNode.FortPresent = false;
            grassNode.Fort = null;

            return grassNode;
        }
    }
}
