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
            town.Rectangle = new Rectangle();
            town.Game = game;
            town.PlayerOwned = true;

            // Set Properties in NodeEntity
            town.StructurePresent = true;
            town.Resource = NodeEntity.Resources.WOOD;
            town.LeftNodes = null;
            town.RightNodes = new List<PathEntity>();
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
            town.Rectangle = new Rectangle();
            town.Game = game;
            town.PlayerOwned = true;

            // Set Properties in NodeEntity
            town.StructurePresent = true;
            town.Resource = NodeEntity.Resources.WOOD;
            town.LeftNodes = null;
            town.RightNodes = new List<PathEntity>();
            town.PreferredPathEntity = null;

            // Set Properties in TownNode


            return town;
        }
    }
}
