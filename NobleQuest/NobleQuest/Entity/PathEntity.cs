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
    public class PathEntity : GameEntity
    {
        public NodeEntity LeftNode { get; set; }
        public NodeEntity RightNode { get; set; }

        public PathEntity(NodeEntity LeftNode, NodeEntity RightNode)
        {
            this.LeftNode = LeftNode;
            this.RightNode = RightNode;
        }
        
    }
}
