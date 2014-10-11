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
    public class PathSelectionEntity : GameEntity
    {
        public PathSelectionEntity(NobleQuestGame Game)
        {
            this.Game = Game;
            this.Texture = this.Game.Content.Load<Texture2D>("PathArrow");
        }
    }
}
