using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using NobleQuest.Entity;

namespace NobleQuest.Entity
{
    public class NodeEntity : GameEntity
    {
        public bool HasResourceStructure;
        public bool HasFort;
        public List<PathEntity> LeftPaths;
        public List<PathEntity> RightPaths;
        public PathEntity PreferredPathEntity;
        public Fort Fort;
        public bool isTown;
    }
}
