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
    public class IntelligentList
    {
        public UnitType unitType;
        public List<NodeEntity> nodeList;

        public IntelligentList()
        {
            nodeList = new List<NodeEntity>();
        }

        public IntelligentList(UnitType unitType, NodeEntity node)
        {
            nodeList = new List<NodeEntity>();
            nodeList.Add(node);
            this.unitType = unitType;
        }
    }
}
