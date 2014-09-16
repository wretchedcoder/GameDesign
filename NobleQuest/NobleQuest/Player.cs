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
        public NobleQuestGame Game;
        public TownNode Town;
        public ResourceEntity Resources;        

        public float currentTime = 0.0f;

        public float farmLastPressed = 0.0f;
        public float sinceFarmPressed = 0.0f;
        public float farmCooldown = 10.0f;

        public void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            
            bool aIsPressed = Keyboard.GetState().IsKeyDown(Keys.A);
            sinceFarmPressed = currentTime - farmLastPressed;
            if (aIsPressed 
                && this.Resources.Gold >= 10
                && sinceFarmPressed >= farmCooldown)
            {
                this.Resources.Gold -= 10;
                sinceFarmPressed = 0.0f;
                Game.Player.Resources.PopulationLimit++;
                Game.Player.Resources.CurrentPopulation++;
                farmLastPressed = currentTime;
            }
        }
    }
}
