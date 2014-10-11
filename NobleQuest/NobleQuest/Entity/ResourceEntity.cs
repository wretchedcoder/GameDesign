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
    public class ResourceEntity : GameEntity
    {
        public int Gold = 200;
        public int CurrentPopulation = 0;
        public int PopulationLimit = 0;
        public int Laborers = 0;
        public int Infantry = 0;
        public int Archer = 0;
        public int Knight = 0;

        public bool HasBlacksmith = false;
        public bool HasFletchery = false;
        public bool HasArmory = false;

        public float TimeElapsed = 0.0f;
        public float Delay = 5.0f;

        public SpriteFont SpriteFont;
        

        public override void Update(GameTime gameTime)
        {
            TimeElapsed += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (TimeElapsed > Delay)
            {
                TimeElapsed -= Delay;
                Gold += Laborers;
            }
        }

        public override void Draw(SpriteBatch SpriteBatch)
        {   
            SpriteBatch.Draw(
                this.Texture,
                this.Position,
                this.SrcRectangle,
                Color.White,
                this.Rotation,
                this.Midpoint,
                1.0f,
                SpriteEffects.None,
                1.0f);

            float nameOffset = 2.0f;
            float amtOffset = 102.0f;

            string resourceName = "";
            string resourceAmt = "";
            Vector2 namePosition;
            Vector2 amtPosition;
            string text = "";

            // Gold Resource 
            resourceName = "Gold";
            resourceAmt = Gold.ToString();
            namePosition = new Vector2(nameOffset, 34.0f + Position.Y);
            amtPosition = new Vector2(amtOffset, 34.0f + Position.Y);

            text = resourceName;
            SpriteBatch.DrawString(SpriteFont, text, namePosition, Color.Black);
            text = resourceAmt;
            SpriteBatch.DrawString(SpriteFont, text, amtPosition, Color.Black);
            
            // Population Resource
            resourceName = "Pop/Max";
            resourceAmt = CurrentPopulation + " / " + PopulationLimit;
            namePosition = new Vector2(nameOffset, 66.0f + Position.Y);
            amtPosition = new Vector2(amtOffset, 66.0f + Position.Y);

            text = resourceName;
            SpriteBatch.DrawString(SpriteFont, text, namePosition, Color.Black);
            text = resourceAmt;
            SpriteBatch.DrawString(SpriteFont, text, amtPosition, Color.Black);

            // Laborers Resource
            resourceName = "Lab";
            resourceAmt = Laborers.ToString();
            namePosition = new Vector2(nameOffset, 98.0f + Position.Y);
            amtPosition = new Vector2(amtOffset, 98.0f + Position.Y);

            text = resourceName;
            SpriteBatch.DrawString(SpriteFont, text, namePosition, Color.Black);
            text = resourceAmt;
            SpriteBatch.DrawString(SpriteFont, text, amtPosition, Color.Black);

            // Infantry Resource
            resourceName = "Infantry";
            resourceAmt = Infantry.ToString();
            namePosition = new Vector2(nameOffset, 130.0f + Position.Y);
            amtPosition = new Vector2(amtOffset, 130.0f + Position.Y);
            if (this.HasBlacksmith)
            {
                text = resourceName;
                SpriteBatch.DrawString(SpriteFont, text, namePosition, Color.Black);
                text = resourceAmt;
                SpriteBatch.DrawString(SpriteFont, text, amtPosition, Color.Black);
            }
            else
            {
                SpriteBatch.Draw(
                    NobleQuestGame.BlockBar,
                    namePosition,
                    null,
                    Color.White,
                    0.0f,
                    new Vector2(0f, 0f),
                    1.0f,
                    SpriteEffects.None,
                    1.0f);
                text = "Blacksmith (S)";
                namePosition.X += 5f;
                namePosition.Y += 5f;
                SpriteBatch.DrawString(SpriteFont, text, namePosition, Color.Black);
            }
            

            // Archery Resource
            resourceName = "Archers";
            resourceAmt = Archer.ToString();
            namePosition = new Vector2(nameOffset, 162.0f + Position.Y);
            amtPosition = new Vector2(amtOffset, 162.0f + Position.Y);

            text = resourceName;
            SpriteBatch.DrawString(SpriteFont, text, namePosition, Color.Black);
            text = resourceAmt;
            SpriteBatch.DrawString(SpriteFont, text, amtPosition, Color.Black);

            // Knight Resource
            resourceName = "Knights";
            resourceAmt = Knight.ToString();
            namePosition = new Vector2(nameOffset, 194.0f + Position.Y);
            amtPosition = new Vector2(amtOffset, 194.0f + Position.Y);

            text = resourceName;
            SpriteBatch.DrawString(SpriteFont, text, namePosition, Color.Black);
            text = resourceAmt;
            SpriteBatch.DrawString(SpriteFont, text, amtPosition, Color.Black);

        }
    }
}
