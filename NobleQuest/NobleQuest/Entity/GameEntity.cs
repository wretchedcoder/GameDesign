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
    public enum Owners { PLAYER, ENEMY, NEUTRAL };

    public enum Orders { NONE, HALT, GO };

    public class GameEntity
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Midpoint;
        public float Rotation;
        public Vector2 Offset;
        public Rectangle SrcRectangle;
        public Rectangle DestRectangle;
        public NobleQuestGame Game;
        public Boolean PlayerOwned;
        public Boolean EnemyOwned;
        public Random RandomGenerator;
        public bool IsVisible;
        public Owners Owner;
        public Orders Order;

        public DynamicEntity TargetEntity;
        public TownNode TargetTown;
        public int HitPointMax;
        public int HitPoint;
        public int Damage;      

        public static Vector2 ZERO_VELOCITY = new Vector2(0f, 0f);
        
        public GameEntity() 
        {
            RandomGenerator = new Random();
            IsVisible = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(
                this.Texture,
                this.Position,
                this.SrcRectangle,
                Color.White,
                this.Rotation,
                this.Midpoint,
                1.0f,
                SpriteEffects.None,
                1.0f);
            }            
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
