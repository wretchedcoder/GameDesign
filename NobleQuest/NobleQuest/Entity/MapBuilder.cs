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
        public List<GameEntity> BuildMap(Game game)
        {
            EntityFactory EntityFactory = new Entity.EntityFactory();
            

            List<GameEntity> GameEntityList = new List<GameEntity>();
            GameEntity ThisGameEntity = null;
            GameEntity LastGameEntity = null;

            ThisGameEntity = EntityFactory.GetPlayerTown(game, new Vector2(100f, 100f));
            GameEntityList.Add(ThisGameEntity);
            LastGameEntity = ThisGameEntity;

            ThisGameEntity = EntityFactory.GetEnemyTown(game, new Vector2(400f, 100f));
            GameEntityList.Add(ThisGameEntity);

            return GameEntityList;
        }
    }
}
