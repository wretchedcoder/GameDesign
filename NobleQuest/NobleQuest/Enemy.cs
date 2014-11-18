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

        public float DecisionTime = 0.0f;
        public float DecisionDelay = 30.0f;

        public Dictionary<DynamicEntity, List<NodeEntity>> NewPaths;
        public List<List<NodeEntity>> PriorityPaths;

        public Enemy()
        {
            EntityFactory = new EntityFactory();
            NewPaths = new Dictionary<DynamicEntity, List<NodeEntity>>();
            PriorityPaths = new List<List<NodeEntity>>();
        }

        public void Update(GameTime gameTime)
        {
            DecisionTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (DecisionTime > DecisionDelay)
            {
                this.MakeDecision(gameTime);
                DecisionTime -= DecisionDelay;
            }
            if (this.Town.HitPoint <= 0)
            {
                this.Game.GameWon = true;
            }
        }

        public void MakeDecision(GameTime gameTime)
        {
            foreach(DynamicEntity entity in NewPaths.Keys)
            {

            }
        }
    }
}
