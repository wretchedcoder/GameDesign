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
    public class Player
    {
        public EntityFactory EntityFactory;

        public NobleQuestGame Game;
        public TownNode Town;
        public ResourceEntity Resources;
        public NodeEntity SelectedNode;

        public float currentTime = 0.0f;

        public float farmTime = 0.0f;
        public float farmCooldown = 10.0f;

        public float infantryTime = 0.0f;
        public float infantryCooldown = 10.0f;

        public float archerTime = 0.0f;
        public float archerCooldown = 10.0f;

        public float knightTime = 0.0f;
        public float knightCooldown = 10.0f;

        public float workerTime = 0.0f;
        public float workerCooldown = 0.0f;

        public Player()
        {
            EntityFactory = new EntityFactory();
        }

        public void Update(GameTime gameTime)
        {
            currentTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            #region Farm (A)
            bool aIsPressed = Keyboard.GetState().IsKeyDown(Keys.A);
            farmTime += currentTime;
            if (aIsPressed 
                && this.Resources.Gold >= 10
                && farmTime >= farmCooldown)
            {
                this.Resources.Gold -= 10;
                farmTime -= farmCooldown;
                Game.Player.Resources.PopulationLimit++;
            }
            #endregion Farm (A)

            #region Infantry (S)
            bool sIsPressed = Keyboard.GetState().IsKeyDown(Keys.S);
            infantryTime += currentTime;
            if (sIsPressed
                && infantryTime >= infantryCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.Resources.HasBlacksmith)
                    {
                        if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                        {
                            this.Resources.Gold -= 50;
                            infantryTime -= infantryCooldown;
                            this.Resources.Infantry++;
                            this.Resources.CurrentPopulation++;

                            EntityFactory.GetInfantryEntity(Game, true, this.Town);
                        }
                        else
                        {
                            // TODO: Put Message about population exceed
                        }                        
                    }
                    else
                    {
                        this.Resources.Gold -= 50;
                        infantryTime -= infantryCooldown;
                        this.Resources.HasBlacksmith = true;
                    }
                }
                else
                {
                    // TODO: Put Message About Insufficient Gold Here
                }
            }
            #endregion Infantry (S)

            #region Archer (D)
            bool dIsPressed = Keyboard.GetState().IsKeyDown(Keys.D);
            archerTime += currentTime;
            if (dIsPressed
                && archerTime >= archerCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.Resources.HasFletchery)
                    {
                        if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                        {
                            this.Resources.Gold -= 50;
                            archerTime -= archerCooldown;
                            this.Resources.Archer++;
                            this.Resources.CurrentPopulation++;

                            // TODO EntityFactory.GetArcherEntity(Game, true, this.Town);
                        }
                        else
                        {
                            // TODO: Put Message about population exceed
                        }
                    }
                    else
                    {
                        this.Resources.Gold -= 50;
                        archerTime -= archerCooldown;
                        this.Resources.HasFletchery = true;
                    }
                }
                else
                {
                    // TODO: Put Message About Insufficient Gold Here
                }
            }
            #endregion Archer (D)

            #region Knight (F)
            bool fIsPressed = Keyboard.GetState().IsKeyDown(Keys.F);
            knightTime += currentTime;
            if (fIsPressed
                && knightTime >= knightCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.Resources.HasArmory)
                    {
                        if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                        {
                            this.Resources.Gold -= 50;
                            knightTime -= knightCooldown;
                            this.Resources.Knight++;
                            this.Resources.CurrentPopulation++;

                            // TODO EntityFactory.GetKnightEntity(Game, true, this.Town);
                        }
                        else
                        {
                            // TODO: Put Message about population exceed
                        }
                    }
                    else
                    {
                        this.Resources.Gold -= 50;
                        knightTime -= knightCooldown;
                        this.Resources.HasArmory = true;
                    }
                }
                else
                {
                    // TODO: Put Message About Insufficient Gold Here
                }
            }
            #endregion Knight (F)

            #region Update Selected Node
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                foreach (NodeEntity node in Game.NodeEntityList)
                {
                    if (node.DestRectangle.Contains(Mouse.GetState().Position))
                    {
                        this.SelectedNode = node;
                        break;
                    }
                }
            }
            #endregion Update Selected Node

            #region Laborer (R)
            bool rIsPressed = Keyboard.GetState().IsKeyDown(Keys.R);
            workerTime += currentTime;
            if (rIsPressed
                && workerTime >= workerCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                    {
                        this.Resources.Gold -= 50;
                        workerTime -= workerCooldown;
                        this.Resources.Laborers++;
                        this.Resources.CurrentPopulation++;
                        this.Game.DynamicEntityList.Add(EntityFactory.GetWorkerEntity(this.Game, true, this.Town));
                    }
                    else
                    {
                        // TODO: Put Message about population exceed
                    }
                }                
                else
                {
                    // TODO: Put Message About Insufficient Gold Here
                }
            }
            #endregion Laborer (R)
        } // End of Update
    } // End of Player Class
} // End of Package
