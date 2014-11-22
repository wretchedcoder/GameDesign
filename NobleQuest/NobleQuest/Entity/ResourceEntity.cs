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
        public int Gold;
        public int Shops;

        public int UnitCost = 10;
        public int ShopCost = 10;

        public float ShopTime = 0.0f;
        public float ShopCooldown = 1.00f;

        public Vector2 FontGoldPosition = Vector2.Zero;
        public Vector2 FontShopsPosition = Vector2.Zero;
        public float FontScale = 1.0f;

        public bool BuyUnit()
        {
            if (this.Gold >= this.UnitCost)
            {
                this.Gold -= this.UnitCost;
                return true;
            }
            return false;
        }

        public bool BuyShop()
        {
            if (this.Gold >= this.ShopCost)
            {
                this.Gold -= this.ShopCost;
                this.Shops++;
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ShopCooldown = 1.00f - ((float)Shops * 0.01f);
            
            ShopTime += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (ShopTime > ShopCooldown)
            {
                ShopTime -= ShopCooldown;
                Gold++;
            }

            FontGoldPosition = this.Position;
            FontGoldPosition.X += 110;
            FontGoldPosition.Y -= 5;

            FontShopsPosition = FontGoldPosition;
            FontShopsPosition.Y += 40;

            this.FontScale = 1.4f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(
                this.Game.SpriteFont,
                this.Gold.ToString(),
                this.FontGoldPosition,
                Color.Black,
                this.Rotation,
                this.Midpoint,
                this.FontScale,
                SpriteEffects.None,
                1.0f);

            spriteBatch.DrawString(
                this.Game.SpriteFont,
                this.Shops.ToString(),
                this.FontShopsPosition,
                Color.Black,
                this.Rotation,
                this.Midpoint,
                this.FontScale,
                SpriteEffects.None,
                1.0f);
        }
    }
}
