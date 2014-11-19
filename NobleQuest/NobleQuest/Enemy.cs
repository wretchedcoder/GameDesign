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
        public float DecisionDelay = 15.0f;

        public List<List<NodeEntity>> NewPaths;

        public Enemy()
        {
            EntityFactory = new EntityFactory();
            NewPaths = new List<List<NodeEntity>>();
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
            if (this.Resources.BuyUnit())
            {
                DynamicEntity NewEntity = new DynamicEntity();
                int unit = this.Game.Random.Next(3);
                switch(unit)
                {
                    case 0:
                        NewEntity = EntityFactory.GetInfantryEntity(
                            this.Game, Owners.ENEMY, this.Town);
                        break;
                    case 1:
                        NewEntity = EntityFactory.GetArcherEntity(
                            this.Game, Owners.ENEMY, this.Town);
                        break;
                    case 2:
                        NewEntity = EntityFactory.GetKnightEntity(
                            this.Game, Owners.ENEMY, this.Town);
                        break;
                    default:
                        break;
                }
                if (this.NewPaths.Count > 0)
                {
                    NewEntity.ToVisitPath = this.NewPaths[0];
                    this.NewPaths.Remove(this.NewPaths[0]);
                }                
            }
        }
    }
}
