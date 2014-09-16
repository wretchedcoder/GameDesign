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
    public class Enemy
    {
        public TownNode Town;
        public ResourceEntity Resources;
        public bool HasBlacksmith = false;
        public bool HasFletchery = false;
        public bool HasArmory = false;
    }
}
