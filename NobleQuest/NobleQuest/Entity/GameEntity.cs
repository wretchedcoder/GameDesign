using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace NobleQuest
{
    public class GameEntity
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Midpoint { get; set; }
        public float Rotation { get; set; }
        public Vector2 Offset { get; set; }
        public Rectangle Rectangle { get; set; }
        public Game Game { get; set; }
        public Boolean PlayerOwned { get; set; }
        public Boolean EnemyOwned { get; set; }

        public GameEntity() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.Texture,
                this.Position,
                null,
                Color.White,
                this.Rotation,
                this.Midpoint,
                1.0f,
                SpriteEffects.None,
                1.0f);
        }

        public void Update()
        {

        }
    }
}
