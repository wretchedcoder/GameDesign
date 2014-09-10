using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NobleQuest.Entity
{
    class DynamicEntity : GameEntity
    {
        public int HitPoints { get; set; }
        public NodeEntity Location { get; set; }
        public bool Moving { get; set; }
        public NodeEntity Destination { get; set; }
    }
}
