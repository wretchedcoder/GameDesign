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
        public Resources Resource { get; set; }
        public HashSet<PathEntity> LeftPaths { get; set; }
        public HashSet<PathEntity> RightPaths { get; set; }
        public PathEntity PreferredPathEntity { get; set; }

        public float DistanceTo(NodeEntity node)
        {
            return Vector2.Distance(this.Position, node.Position);
        }
        
    }
}
