﻿using Zilon.Core.Graphs;
using Zilon.Core.Persons;

namespace Zilon.Core.Tactics
{
    /// <summary>
    /// Интерфейс объект, который возможно атаковать.
    /// </summary>
    public interface IAttackTarget
    {
        /// <summary>
        /// Текущий узел карты, в котором находится цель.
        /// </summary>
        IGraphNode Node { get; }

        PhysicalSize PhysicalSize { get; }

        /// <summary>
        /// Принятие урона.
        /// </summary>
        /// <param name="value"> Величина урона, полученного объектом. </param>
        /// <remarks>
        /// Это окончательный урон с учетом всех модифиаторов.
        /// </remarks>
        void TakeDamage(int value);

        /// <summary>
        /// Проверка, может ли цель быть атакована.
        /// </summary>
        bool CanBeDamaged();
    }
}
