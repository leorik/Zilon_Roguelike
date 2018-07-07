﻿using System;
using Zilon.Core.Common;
using Zilon.Core.Schemes;

namespace Zilon.Core.Persons
{
    public class MonsterPerson: IPerson, ITacticalActCarrier
    {
        public int Id { get; set; }
        public float Hp { get; }
        public IEquipmentCarrier EquipmentCarrier => throw new NotSupportedException("Для монстров не поддерживается явная экипировка");

        public ITacticalAct[] Acts { get; }

        public MonsterPerson()
        {
            var scheme = new TacticalActScheme
            {
                Efficient = new Range<float>(1, 1),
                MinRange = 1,
                MaxRange = 1
            };

            Acts = new TacticalAct[] {
                new TacticalAct(1, scheme)
            };
        }
    }
}