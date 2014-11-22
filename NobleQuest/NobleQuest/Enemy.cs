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
        public float DecisionDelay = 12.5f;

        public List<IntelligentList> NewPaths;

        public Enemy(NobleQuestGame Game)
        {
            EntityFactory = new EntityFactory();
            NewPaths = new List<IntelligentList>();
            this.Game = Game;
        }

        public void initPaths()
        {
            NewPaths.Add(new IntelligentList(UnitType.INFANTRY, this.Game.TopEnd));
            NewPaths.Add(new IntelligentList(UnitType.ARCHER, this.Game.MidEnd));
            NewPaths.Add(new IntelligentList(UnitType.KNIGHT, this.Game.BotEnd));
        }

        public void Update(GameTime gameTime)
        {
            DecisionTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (DecisionTime > DecisionDelay)
            {
                DecisionTime -= DecisionDelay;

                if (this.Town.Occupant == null)
                {
                    this.MakeDecision(gameTime);
                }
            }
            if (this.Town.HitPoint <= 0)
            {
                this.Game.GameWon = true;
            }
        }

        public void MakeDecision(GameTime gameTime)
        {
            IntelligentList intelligentList = null;
            if (this.Resources.BuyUnit())
            {
                if (this.NewPaths.Count > 0)
                {
                    intelligentList = this.NewPaths[0];
                    this.NewPaths.Remove(this.NewPaths[0]);
                } 
                DynamicEntity NewEntity = new DynamicEntity();
                if (intelligentList == null)
                {
                    int unit = this.Game.Random.Next(3);
                    switch (unit)
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
                }
                else
                {
                    switch (intelligentList.unitType)
                    {
                        case UnitType.ARCHER:
                            NewEntity = EntityFactory.GetInfantryEntity(
                                this.Game, Owners.ENEMY, this.Town);
                            break;
                        case UnitType.KNIGHT:
                            NewEntity = EntityFactory.GetArcherEntity(
                                this.Game, Owners.ENEMY, this.Town);
                            break;
                        case UnitType.INFANTRY:
                            NewEntity = EntityFactory.GetKnightEntity(
                                this.Game, Owners.ENEMY, this.Town);
                            break;
                        default:
                            break;
                    }
                    NewEntity.ToVisitPath.AddRange(intelligentList.nodeList);
                }                
            }
        } // MakeDecision

        public void AddPath(UnitType unitType, List<NodeEntity> newList)
        {
            IntelligentList intelligentList = new IntelligentList();
            intelligentList.unitType = unitType;
            intelligentList.nodeList.AddRange(newList);
            this.NewPaths.Add(intelligentList);
        }
    }
}
