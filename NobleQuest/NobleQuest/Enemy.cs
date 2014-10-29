using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using NobleQuest.Entity;

namespace NobleQuest
{
    public class Enemy
    {
        public EntityFactory EntityFactory;

        public NobleQuestGame Game;
        public TownNode Town;
        public ResourceEntity Resources;
        public bool HasBlacksmith = false;
        public bool HasFletchery = false;
        public bool HasArmory = false;

        public float infantryTime = 0.0f;
        public float infantryDelay = 30.0f;

        public Enemy()
        {
            EntityFactory = new EntityFactory();
        }

        public void Update(GameTime gameTime)
        {
            infantryTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (infantryTime > infantryDelay)
            {
                this.Game.EntityFactory.GetInfantryEntity(Game, Owners.ENEMY, Town);
                infantryTime -= infantryDelay;
            }
        }
    }
}
