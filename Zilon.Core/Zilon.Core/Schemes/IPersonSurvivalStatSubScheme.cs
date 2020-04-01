﻿namespace Zilon.Core.Schemes
{
    /// <summary>
    /// Подсхема характеристики выживания персонажа.
    /// </summary>
    public interface IPersonSurvivalStatSubScheme: ISubScheme
    {
        /// <summary>
        /// Тип характеристики выживания. Сытость/гидратация.
        /// На будущее будет усталость, уровень перегрева, переохлаждения,
        /// отравления, токсикации, облучения, коррапта, дыхание.
        /// </summary>
        PersonSurvivalStatType Type { get; }

        /// <summary>
        /// Ключевые точки характеристики.
        /// При прохождении ключевых точек будет выбрасываться событие.
        /// В результате прохождения ключевых точек обычно наступают или сбрасываются угрозы выживания.
        /// Например, добавляется эффект голода, снижающего характеристики.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays",
            Justification = "используется для десериализации с внешнего источника.")]
        IPersonSurvivalStatKeySegmentSubScheme[] KeyPoints { get; }

        /// <summary>
        /// Стартовое значение характеристики.
        /// Это опорное, начальное значение характеристики при создании персонажа.
        /// </summary>
        int StartValue { get; }

        /// <summary>
        /// Минимальное значение характеристики.
        /// Ниже этого значения характеристика не будет опускаться.
        /// </summary>
        int MinValue { get; }

        /// <summary>
        /// Максимальное значение характеристики.
        /// Выше этого значения характеристика не будет подниматься.
        /// </summary>
        int MaxValue { get; }

        /// <summary>
        /// Бросок кости, при котором характеристика снижается.
        /// При 0 - не будет снижаться.
        /// При 6 - будет снижаться каждый ход.
        /// </summary>
        //TODO переделать на int, т.к. это значение всегда должно быть задано. Убрать установку значения по умолчанию.
        int? DownPassRoll { get; }
    }
}
