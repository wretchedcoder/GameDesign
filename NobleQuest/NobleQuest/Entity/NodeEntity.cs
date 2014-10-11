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
        public List<PathEntity> AllPaths;
        public PathEntity PreferredPathEntity;
        public Fort Fort;
        public bool isTown;
        public bool isSelected;
      
        public void IncrementPreferredPath()
        {
            if (AllPaths == null)
            {
                AllPaths = new List<PathEntity>();
                AllPaths.AddRange(RightPaths);
                AllPaths.AddRange(LeftPaths);
            }

            if (PreferredPathEntity == null)
            {
                PreferredPathEntity = AllPaths[0];
            }
            else
            {
                int index = AllPaths.IndexOf(PreferredPathEntity);
                if (index + 1 < AllPaths.Count)
                {
                    PreferredPathEntity.IsPreferredPath = false;
                    PreferredPathEntity = AllPaths[index + 1];
                    PreferredPathEntity.IsPreferredPath = true;
                }
                else
                {
                    PreferredPathEntity.IsPreferredPath = false;
                    PreferredPathEntity = AllPaths[0];
                    PreferredPathEntity.IsPreferredPath = true;
                }
            }
        }
    }
}
