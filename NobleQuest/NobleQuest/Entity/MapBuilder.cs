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
    public class MapBuilder
    {
        public void BuildMap(NobleQuestGame game)
        {
            EntityFactory EntityFactory = new Entity.EntityFactory();

            GameEntity ThisGameEntity = null;
            GameEntity LastGameEntity = null;

            float hudYOffset = game.Graphics.PreferredBackBufferHeight - 225.0f;

            game.Player.Resources = EntityFactory.GetResourceEntity(game, new Vector2(0f, hudYOffset), true);
            game.Enemy.Resources = EntityFactory.GetResourceEntity(game, new Vector2(0f, hudYOffset), false);

            ThisGameEntity = EntityFactory.GetPlayerTown(game, new Vector2(100f, 200f));
            game.Player.Town = (TownNode)ThisGameEntity;
            game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
            LastGameEntity = ThisGameEntity;

            ThisGameEntity = EntityFactory.GetGrassNode(game, new Vector2(200f, 200f));
            game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
            game.PathEntityList.Add(EntityFactory.GetPathEntity(game, (NodeEntity)LastGameEntity, (NodeEntity)ThisGameEntity));
            LastGameEntity = ThisGameEntity;

            ThisGameEntity = EntityFactory.GetGrassNode(game, new Vector2(300f, 200f));
            game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
            game.PathEntityList.Add(EntityFactory.GetPathEntity(game, (NodeEntity)LastGameEntity, (NodeEntity)ThisGameEntity));
            LastGameEntity = ThisGameEntity;

            ThisGameEntity = EntityFactory.GetGrassNode(game, new Vector2(400f, 200f));
            game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
            game.PathEntityList.Add(EntityFactory.GetPathEntity(game, (NodeEntity)LastGameEntity, (NodeEntity)ThisGameEntity));
            LastGameEntity = ThisGameEntity;

            ThisGameEntity = EntityFactory.GetGrassNode(game, new Vector2(500f, 200f));
            game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
            game.PathEntityList.Add(EntityFactory.GetPathEntity(game, (NodeEntity)LastGameEntity, (NodeEntity)ThisGameEntity));
            LastGameEntity = ThisGameEntity;

            ThisGameEntity = EntityFactory.GetGrassNode(game, new Vector2(600f, 200f));
            game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
            game.PathEntityList.Add(EntityFactory.GetPathEntity(game, (NodeEntity)LastGameEntity, (NodeEntity)ThisGameEntity));
            LastGameEntity = ThisGameEntity;

            ThisGameEntity = EntityFactory.GetEnemyTown(game, new Vector2(700f, 200f));
            game.Enemy.Town = (TownNode)ThisGameEntity;
            game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
            game.PathEntityList.Add(EntityFactory.GetPathEntity(game, (NodeEntity)LastGameEntity, (NodeEntity)ThisGameEntity));
            LastGameEntity = ThisGameEntity;
        }
    }
}
