﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NobleQuest.Entity
{
    public class KnightEntity : InfantryEntity
    {
        public KnightEntity(NobleQuestGame Game)
            : base(Game)
        {
            HitPointMax = 100;
            HitPoint = 100;
            Damage = 20;
            this.Game = Game;
            this.unitType = UnitType.KNIGHT;
        }

        public override int GetModifier()
        {
            if (this.TargetEntity == null)
            {
                return 0;
            }
            switch (this.TargetEntity.unitType)
            {
                case UnitType.ARCHER:
                    return 0;
                case UnitType.INFANTRY:
                    return 20;
                case UnitType.KNIGHT:
                    return 0;
                default:
                    return 0;
            }
        }
    }
}
