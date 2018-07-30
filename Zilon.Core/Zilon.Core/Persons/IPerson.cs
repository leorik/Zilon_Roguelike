﻿namespace Zilon.Core.Persons
{
    /// <summary>
    /// Интерфейс персонажа.
    /// </summary>
    /// <remarks>
    /// Персонаж - это описание игрового объекта за пределами тактических боёв.
    /// </remarks>
    public interface IPerson
    {
        int Id { get; set; }

        /// <summary>
        /// Хитпоинты персонажа.
        /// </summary>
        float Hp { get; }

        /// <summary>
        /// Носитель экипировки.
        /// </summary>
        IEquipmentCarrier EquipmentCarrier { get; }

        /// <summary>
        /// Носитель тактических действий.
        /// </summary>
        ITacticalActCarrier TacticalActCarrier { get; }

        /// <summary>
        /// Данные о развитие персонажа.
        /// </summary>
        IEvolutionData EvolutionData { get; }

        /// <summary>
        /// Характеристики, используемые персонажем в бою.
        /// </summary>
        ICombatStats CombatStats { get; }
    }
}