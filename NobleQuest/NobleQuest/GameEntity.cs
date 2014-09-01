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
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public Vector2 midpoint { get; set; }
        public float rotation { get; set; }
        public Vector2 offset { get; set; }
        public Rectangle rectangle { get; set; }
        public Game game { get; set; }

        public GameEntity() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.texture,
                this.position,
                null,
                Color.White,
                this.rotation,
                this.midpoint,
                1.0f,
                SpriteEffects.None,
                1.0f);
        }

        public void Update()
        {

        }
    }
}
