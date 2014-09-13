using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NobleQuest.Entity
{
    public class DynamicEntity : GameEntity
    {
        public int HitPoints;
        public NodeEntity Location;
        public bool Moving;
        public NodeEntity Destination;
    }
}
