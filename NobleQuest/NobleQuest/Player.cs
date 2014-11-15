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

        public bool isUpdating = false;

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

        private KeyboardState OldKeyState;
        private KeyboardState NewKeyState;

        public Player()
        {
            EntityFactory = new EntityFactory();
        }

        public void Update(GameTime gameTime)
        {
            currentTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            if (this.Town.HitPoint <= 0)
            {
                this.Game.GameLost = true;
            }

            CheckKeyState();

        } // End of Update

        private void CheckKeyState()
        {
            NewKeyState = Keyboard.GetState();
            if (OldKeyState != null)
            {
                if (OldKeyState.IsKeyDown(Keys.A)
                    && NewKeyState.IsKeyUp(Keys.A))
                {
                    this.Game.EntityFactory.GetWorkerEntity(this.Game, true, this.Town);
                }

                if (OldKeyState.IsKeyDown(Keys.S)
                    && NewKeyState.IsKeyUp(Keys.S))
                {
                    this.Game.EntityFactory.GetInfantryEntity(this.Game, Owners.PLAYER, this.Town);
                }
            }
            OldKeyState = NewKeyState;
        }
    } // End of Player Class
} // End of Package
