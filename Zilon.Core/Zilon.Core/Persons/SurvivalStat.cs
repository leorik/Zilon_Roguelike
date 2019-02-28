﻿using System;
using Zilon.Core.Common;

namespace Zilon.Core.Persons
{
    /// <summary>
    /// Текущие показатели характеристики модуля выживания.
    /// </summary>
    public class SurvivalStat
    {
        private float _rawValue;

        public SurvivalStat(int startValue, int min, int max)
        {
            Range = new Range<int>(min, max);

            Value = startValue;
        }

        /// <summary>
        /// Тип характеристики.
        /// </summary>
        public SurvivalStatType Type { get; set; }

        /// <summary>
        /// Текущее значение.
        /// </summary>
        public int Value
        {
            get
            {
                var result = Math.Round((Range.Max - Range.Min) * _rawValue);
                return (int)result;
            }
            set
            {
                var boundedValue = Range.GetBounded(value);
                _rawValue = boundedValue / (float)(Range.Max - Range.Min);
            }
        }

        /// <summary>
        /// Минимальное/максимальное значение.
        /// </summary>
        public Range<int> Range { get; private set; }

        /// <summary>
        /// Скорость снижения характеристики за ход.
        /// </summary>
        public int Rate { get; set; }

        /// <summary> Набор ключевых точек характеристики. </summary>
        public SurvivalStatKeyPoint[] KeyPoints { get; set; }

        /// <summary>
        /// Изменение текущего диапазона характеристики.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public void ChangeStatRange(int min, int max)
        {
            if (min >= max)
            {
                Range = new Range<int>(min, min + 1);
                Value = Range.Max;
                return;
            }

            Range = new Range<int>(min, max);
        }
    }
}
