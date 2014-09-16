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

        public float farmLastPressed = 0.0f;
        public float sinceFarmPressed = 0.0f;
        public float farmCooldown = 10.0f;

        public float infantryLastPressed = 0.0f;
        public float sinceInfantryPressed = 0.0f;
        public float infantryCooldown = 10.0f;

        public float archerLastPressed = 0.0f;
        public float sinceArcherPressed = 0.0f;
        public float archerCooldown = 10.0f;

        public float knightLastPressed = 0.0f;
        public float sinceKnightPressed = 0.0f;
        public float knightCooldown = 10.0f;

        public float laborerLastPressed = 0.0f;
        public float sinceLaborerPressed = 0.0f;
        public float laborerCooldown = 10.0f;

        public Player()
        {
            EntityFactory = new EntityFactory();
        }

        public void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            #region Farm (A)
            bool aIsPressed = Keyboard.GetState().IsKeyDown(Keys.A);
            sinceFarmPressed = currentTime - farmLastPressed;
            if (aIsPressed 
                && this.Resources.Gold >= 10
                && sinceFarmPressed >= farmCooldown)
            {
                this.Resources.Gold -= 10;
                sinceFarmPressed = 0.0f;
                Game.Player.Resources.PopulationLimit++;
                farmLastPressed = currentTime;
            }
            #endregion Farm (A)

            #region Infantry (S)
            bool sIsPressed = Keyboard.GetState().IsKeyDown(Keys.S);
            sinceInfantryPressed = currentTime - infantryLastPressed;
            if (sIsPressed
                && sinceInfantryPressed >= infantryCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.Resources.HasBlacksmith)
                    {
                        if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                        {
                            this.Resources.Gold -= 50;
                            sinceInfantryPressed = 0.0f;
                            infantryLastPressed = currentTime;
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
                        sinceInfantryPressed = 0.0f;
                        infantryLastPressed = currentTime;
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
            sinceArcherPressed = currentTime - archerLastPressed;
            if (dIsPressed
                && sinceArcherPressed >= archerCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.Resources.HasFletchery)
                    {
                        if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                        {
                            this.Resources.Gold -= 50;
                            sinceArcherPressed = 0.0f;
                            archerLastPressed = currentTime;
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
                        sinceArcherPressed = 0.0f;
                        archerLastPressed = currentTime;
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
            sinceKnightPressed = currentTime - knightLastPressed;
            if (fIsPressed
                && sinceKnightPressed >= knightCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.Resources.HasArmory)
                    {
                        if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                        {
                            this.Resources.Gold -= 50;
                            sinceKnightPressed = 0.0f;
                            knightLastPressed = currentTime;
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
                        sinceKnightPressed = 0.0f;
                        knightLastPressed = currentTime;
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
            sinceLaborerPressed = currentTime - laborerLastPressed;
            if (rIsPressed
                && sinceLaborerPressed >= laborerCooldown)
            {
                if (this.Resources.Gold >= 50)
                {
                    if (this.SelectedNode != null)
                    {
                        if (this.Resources.CurrentPopulation < this.Resources.PopulationLimit)
                        {
                            if (this.SelectedNode.HasResourceStructure)
                            {
                                
                            }
                            else
                            { 
                                this.Resources.Gold -= 50;
                                sinceLaborerPressed = 0.0f;
                                laborerLastPressed = currentTime;
                                this.Resources.Laborers++;
                                this.Resources.CurrentPopulation++;
                                this.SelectedNode.HasResourceStructure = true;
                            }
                        }
                        else
                        {
                            // TODO: Put Message about population exceed
                        }
                    }
                    else
                    {
                        // Put message about no selected node
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
