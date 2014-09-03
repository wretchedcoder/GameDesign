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
    public class NodeEntity : GameEntity
    {
        public enum Resources {WOOD, ORE, HAY};
        public Boolean StructurePresent { get; set; }
        public int Resource { get; set; }
        /*
        public List<PathEntity> LeftNodes { get; set; }
        public List<PathEntity> RightNodes { get; set; }
        public PathEntity PreferredPathEntity { get; set; }
         * */
    }
}
