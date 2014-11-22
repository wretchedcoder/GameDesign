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
    public class TownNode : NodeEntity
    {
        public HitPointBarEntity HitBar;
        public DynamicEntity LastAttackedBy = null;

        public TownNode(NobleQuestGame Game, Owners Owner) : base(Game)
        {
            this.HitPointMax = 1000;
            this.HitPoint = 1000;
            this.Damage = 5;

            this.Owner = Owner;

            this.HitBar = new HitPointBarEntity(this.Game);
            this.HitBar.AssociatedEntity = this;
            this.HitBar.Background = 
                this.Game.Content.Load<Texture2D>("TownHitBarBackground");
            this.HitBar.Foreground =
                this.Game.Content.Load<Texture2D>("TownHitBar");
            this.HitBar.UpdatePosition = false;
            if (this.Owner == Owners.PLAYER)
            {
                this.HitBar.Position.X = 10;
                this.HitBar.Position.Y = 10;
            }
            else
            {
                this.HitBar.Position.X = 
                    this.Game.Graphics.PreferredBackBufferWidth - this.HitBar.Background.Width - 10;
                this.HitBar.Position.Y = 10;
                this.HitBar.InvertDirection = true;
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.HitBar.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            this.HitBar.Draw(spriteBatch);
        }
    }
}
